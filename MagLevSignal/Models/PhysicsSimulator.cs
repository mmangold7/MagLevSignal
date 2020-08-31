using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using MagLevSignal.Hubs;

namespace MagLevSignal.Models
{
    public class PhysicsSimulator
    {
        #region ///  Constants  ///

        private const double BigG = .0002;
        private const int Fps = 90;

        #endregion

        #region ///  Fields  ///

        public List<Body> Bodies;

        private readonly IHubContext<MagLevHub, IMagLevHub> _hubContext;

        #endregion

        #region ///  Constructors  ///

        public PhysicsSimulator(IServiceProvider serviceProvider)
        {
            _hubContext = serviceProvider.GetService<IHubContext<MagLevHub, IMagLevHub>>();

            Bodies = new List<Body>();
        }

        //public PhysicsSimulator(IHubContext<MagLevHub, IMagLevHub> context, IEnumerable<Body> bodies)
        //{
        //    _hubContext = context;

        //    Bodies = bodies.ToList();

        //    Task.Run(() => Simulate());
        //}

        #endregion

        #region ///  Properties  ///

        public bool IsPaused { get; private set; }

        #endregion

        #region ///  Methods  ///

        public Body CreateCircularOrbiterOf(Body parentBody, float orbitRadius, float mass, float radius, string color, string name)
        {
            var orbiter = new Body
                          {
                              Name = name,
                              Mass = mass,
                              Radius = radius,
                              Color = color,
                              Position = new Vector2(parentBody.Position.X, parentBody.Position.Y + orbitRadius),
                              Velocity = new Vector2(0, 0)
                          };

            AssignCircularOrbitVelocity(orbiter, parentBody);

            orbiter.ParentBody = parentBody;

            Bodies.Add(orbiter);

            return orbiter;
        }

        public void SetUpSol()
        {
            //make the sun
            var sun = new Body
                      {
                          Name = "sun",
                          Mass = 400000,
                          Radius = 40,
                          Position = new Vector2(0, 0),
                          Velocity = new Vector2(0, 0),
                          Color = "Yellow"
                      };
            this.Bodies.Add(sun);

            //make the earth
            var earth = this.CreateCircularOrbiterOf(sun, 300, 1000, 6, "blue", "earth");
        }

        public void ResetSimulation()
        {
            Bodies.RemoveAll(b => true);

            SetUpSol();
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
            Simulate();
        }

        public async void Simulate()
        {
            while (!IsPaused)
            {
                if (Bodies == null)
                {
                    break;
                }

                foreach (var body in Bodies)
                {
                    UpdateBodyPosition(body);
                }

                await _hubContext.Clients.All.GameState(Bodies);

                await Task.Delay(1000 / Fps);
            }
        }

        private void AssignCircularOrbitVelocity(Body orbiter, Body parentBody)
        {
            var orbitRadius = Vector2.Distance(orbiter.Position, parentBody.Position);

            var accelerationOfGravity = BigG * parentBody.Mass / Math.Pow(orbitRadius, 2);

            var parentReferenceFrameOrbitingXVelocity = Convert.ToSingle(Math.Sqrt(orbitRadius * accelerationOfGravity));

            orbiter.Velocity += new Vector2(parentReferenceFrameOrbitingXVelocity, 0);

            if (parentBody.ParentBody == null)
            {
                return;
            }

            AssignCircularOrbitVelocity(orbiter, parentBody.ParentBody);
        }

        private void GravitateBody(Body body)
        {
            foreach (var otherBody in Bodies.Where(b => b != body))
            {
                var displacement = otherBody.Position - body.Position;
                var rSquared = displacement.LengthSquared();
                var acceleration = Vector2.Normalize(displacement) * Convert.ToSingle(BigG * otherBody.Mass / rSquared);
                body.Velocity += acceleration;
            }
        }

        private void MoveBody(Body body)
        {
            body.Position += body.Velocity;
        }

        private void UpdateBodyPosition(Body body)
        {
            MoveBody(body);
            GravitateBody(body);
        }

        #endregion
    }
}