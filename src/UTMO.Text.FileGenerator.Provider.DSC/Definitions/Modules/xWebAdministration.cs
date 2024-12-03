// // ***********************************************************************
// // Assembly         : UTMO.DSC.Configuration.Common
// // Author           : Josh Irwin (joirwi)
// // Created          : 09/08/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 09/08/2023 12:42 PM
// // ***********************************************************************
// // <copyright file="xWebAdministration.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules
{
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

    public class xWebAdministration : RequiredModule
    {
        private xWebAdministration()
        {
        }
        
        public override string ModuleName => "xWebAdministration";
        public override string ModuleVersion => "3.0.0";
        
        public override string? RewriteModuleVersion => "3.0.0.0";
        
        public static RequiredModule Instance { get; } = new xWebAdministration();
    }
}