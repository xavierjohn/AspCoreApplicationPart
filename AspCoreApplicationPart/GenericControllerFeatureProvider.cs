using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AspCoreApplicationPart
{
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly Type moduleType;

        public GenericControllerFeatureProvider(Type moduleType)
        {
            this.moduleType = moduleType;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            // This is designed to run after the default ControllerTypeProvider, 
            // so the list of 'real' controllers has already been populated.
            var dllFiles = Directory.EnumerateFiles($"{AppDomain.CurrentDomain.BaseDirectory}Plugins", "*.dll");
            foreach (var dllFile in dllFiles)
            {
                var dll = Assembly.LoadFrom(dllFile);
                var typeControllers = dll.GetExportedTypes().Where(t => t != moduleType && moduleType.IsAssignableFrom(t));
                foreach (var cType in typeControllers)
                {
                    feature.Controllers.Add(cType.GetTypeInfo());
                }
            }
        }
    }
}
