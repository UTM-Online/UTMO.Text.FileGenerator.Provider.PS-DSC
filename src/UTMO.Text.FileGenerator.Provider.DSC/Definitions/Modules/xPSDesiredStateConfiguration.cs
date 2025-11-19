// // ***********************************************************************
// // Assembly         : UTMO.DSC.Configurations
// // Author           : Josh Irwin (joirwi)
// // Created          : 09/08/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 09/08/2023 12:07 PM
// // ***********************************************************************
// // <copyright file="xPSDesiredStateConfiguration.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules
{
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

    public class xPSDesiredStateConfiguration : RequiredModule
    {
        private xPSDesiredStateConfiguration()
        {
        }
        
        public override string ModuleName => "xPSDesiredStateConfiguration";

        public override string ModuleVersion => "9.1.0";

        public override string? RewriteModuleVersion => this.ModuleVersion;

        public override bool UseAlternateFormat => true;

        public static RequiredModule Instance { get; } = new xPSDesiredStateConfiguration();
    }
}