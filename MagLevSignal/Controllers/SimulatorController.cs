using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagLevSignal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MagLevSignal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimulatorController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IServiceProvider _serviceProvider;

        public SimulatorController(ILogger<WeatherForecastController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                StartSimulationIfStopped();
                return Ok(new { success = true });
            }
            catch (Exception e)
            {
                return Ok(new { success = false });
            }
        }

        public PhysicsSimulator GetSolSimulator()
        {
            var simulator = new PhysicsSimulator(_serviceProvider);

            simulator.SetUpSol();

            return simulator;
        }

        private void StartSimulationIfStopped()
        {
            if (Globals.Simulator != null)
            {
                return; //not stopped
            }

            //start it up
            Globals.Simulator = GetSolSimulator();
            Task.Run(() => Globals.Simulator.Simulate());
        }
    }
}
