using Microsoft.AspNetCore.Mvc;
using ModuleType;
using System;

namespace BalancedHappyModules
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Happy2Service : Happy
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Happy 2 Service";
        }
    }
}
