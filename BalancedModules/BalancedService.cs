using Microsoft.AspNetCore.Mvc;
using ModuleType;
using System;

namespace PlugIns
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BalancedService : Balanced
    {
        public ActionResult<string> Get()
        {
            return "Balanced Service";
        }
    }
}
