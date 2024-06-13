using CarDealer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class SoldCarController : ControllerBase
    {
        private readonly ISoldCarService? _soldCarService;
        
        public SoldCarController(ISoldCarService soldCarService) => _soldCarService = soldCarService;
    }
}
