using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;
using ModuleType;

namespace AspCoreApplicationPart
{
    public class MyCustomControllerFactory : DefaultControllerFactory
    {
        public MyCustomControllerFactory(IControllerActivator controllerActivator, IEnumerable<IControllerPropertyActivator> propertyActivators) : base(controllerActivator, propertyActivators)
        {
        }

        public override object CreateController(ControllerContext context)
        {
            var controller = base.CreateController(context);

            if (controller is Happy happyController)
            {
                if (context.HttpContext.Request.Headers.TryGetValue("CorrelationId", out var correlationId))
                {
                    happyController.CorrelationId = correlationId;
                }
            }

            return controller;
        }
    }
}