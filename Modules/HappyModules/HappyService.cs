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
            var returnString = "Happy Service";
            if (!string.IsNullOrEmpty(base.CorrelationId))
            {
                returnString += $" Correlation Id :{base.CorrelationId}";
            }
            return returnString;
        }
    }
}
