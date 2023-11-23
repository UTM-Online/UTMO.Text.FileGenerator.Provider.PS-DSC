// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 2:43 PM
// // ***********************************************************************
// // <copyright file="EnvironmentExtensions.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC
{
    using System.Reflection;
    using Resources;
    using UTMO.Text.FileGenerator.Abstract;
    
    public static class EnvironmentExtensions
    {
        public static ITemplateGenerationEnvironment AddComputer<T>(this ITemplateGenerationEnvironment env, T computer)
            where T : DscComputer
        {
            env.AddResource(computer);
            return env;
        }
        
        public static ITemplateGenerationEnvironment AddConfiguration<T>(this ITemplateGenerationEnvironment env, T configuration)
            where T : DscConfiguration
        {
            env.AddResource(configuration);
            return env;
        }
        
        public static ITemplateGenerationEnvironment AddComputer<T>(this ITemplateGenerationEnvironment env)
            where T : DscComputer, new()
        {
            var computer = new T();
            env.AddResource(computer);
            return env;
        }
        
        public static ITemplateGenerationEnvironment AddConfiguration<T>(this ITemplateGenerationEnvironment env)
            where T : DscConfiguration, new()
        {
            var configuration = new T();
            env.AddResource(configuration);
            return env;
        }

        public static ITemplateGenerationEnvironment UseDiscovery(this ITemplateGenerationEnvironment env)
        {
            var discoveredTypes = Assembly.GetCallingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(DscComputer)) || t.IsSubclassOf(typeof(DscConfiguration)));
            
            foreach (var type in discoveredTypes)
            {
                var resource = Activator.CreateInstance(type) as ITemplateModel;

                if (resource == null)
                {
                    continue;
                }
                
                env.AddResource(resource);
            }
            
            return env;
        }
    }
}