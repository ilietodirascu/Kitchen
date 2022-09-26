using Kitchen.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StartSimulationController : ControllerBase
    {
        [HttpGet]
        public IActionResult StartSimulation()
        {
            Simulation simulation = new Simulation();
            simulation.RunSimulation();
            return Ok();
        }
    }
}
