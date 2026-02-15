﻿// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 2:43 PM
// // ***********************************************************************
// // <copyright file="EnvironmentExtensions.cs" company="Joshua S. Irwin">
// //     Copyright (c) 2026 Joshua S. Irwin. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC
{
    using System.Reflection;
    using Text.FileGenerator.Abstract.Contracts;
    using UTMO.Text.FileGenerator.Abstract;
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

    public static class EnvironmentExtensions
    {
        public static ITemplateGenerationEnvironment AddComputer<T>(this ITemplateGenerationEnvironment env, T computer)
            where T : DscLcmConfiguration
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
            where T : DscLcmConfiguration, new()
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
    }
}