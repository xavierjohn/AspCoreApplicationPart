using Microsoft.AspNetCore.Mvc;
using ModuleType;
using System;

namespace HappyModules
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HappyService : Happy
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Happy Service";
        }
    }
}
