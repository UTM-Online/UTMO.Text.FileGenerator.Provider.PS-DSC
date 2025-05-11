// // ***********************************************************************
// // Assembly         : UTMO.DSC.Configuration.Common
// // Author           : Josh Irwin (joirwi)
// // Created          : 09/08/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 09/08/2023 12:41 PM
// // ***********************************************************************
// // <copyright file="ComputerManagementDsc.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.ModuleDefinitions
{
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

    public class ComputerManagementDsc : RequiredModule
    {
        private ComputerManagementDsc()
        {
        }
        
        public override string ModuleName => "ComputerManagementDsc";
        public override string ModuleVersion => "10.0";

        public override string RewriteModuleVersion => "10.0.0";
        
        public override bool UseAlternateFormat => true;
        
        public static RequiredModule Instance { get; } = new ComputerManagementDsc();
    }
}