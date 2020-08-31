using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MagLevSignal.Models;

namespace MagLevSignal.Hubs
{
    public static class UserHandler
    {
        #region ///  Fields  ///

        public static HashSet<string> ConnectedIds = new HashSet<string>();

        #endregion
    }

    public class MagLevHub : Hub<IMagLevHub>
    {
        public MagLevHub(PhysicsSimulator physicsSimulator)
        {
            _physicsSimulator = physicsSimulator;
        }

        #region ///  Fields  ///

        private PhysicsSimulator _physicsSimulator = Globals.Simulator;

        #endregion

        #region ///  Methods  ///

        public async Task ResetSimulation()
        {
            _physicsSimulator.ResetSimulation();
        }

        public async Task GameState(List<Body> bodies)
        {
            await Clients.All.GameState(bodies);
        }

        public string GetConnectionId() => Context.ConnectionId;

        public override async Task OnConnectedAsync()
        {
            UserHandler.ConnectedIds.Add(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            if (UserHandler.ConnectedIds.Count == 0 && _physicsSimulator != null)
            {
                _physicsSimulator = null;
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task Pause() => _physicsSimulator.Pause();

        public async Task Resume() => _physicsSimulator.Resume();

        public async Task TogglePaused()
        {
            if (_physicsSimulator.IsPaused)
            {
                _physicsSimulator.Resume();
            }
            else
            {
                _physicsSimulator.Pause();
            }
        }

        #endregion
    }
}