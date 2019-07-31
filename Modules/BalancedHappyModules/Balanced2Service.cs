using Microsoft.AspNetCore.Mvc;
using ModuleType;
using System;

namespace BalancedHappyModules
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Balanced2Service : Balanced
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Balanced 2 Service";
        }
    }
}
