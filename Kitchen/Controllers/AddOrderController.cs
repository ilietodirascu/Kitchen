using Kitchen.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddOrderController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            Utility.AddOrder(order);
            return Ok();
        }
    }
}
