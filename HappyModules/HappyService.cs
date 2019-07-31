using Microsoft.AspNetCore.Mvc;
using ModuleType;
using System;

namespace HappyModules
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HappyService : Happy
    {
        public ActionResult<string> Get()
        {
            return "Happy Service";
        }
    }
}
