// // ***********************************************************************
// // Assembly         : UTMO.DSC.Configuration.Common
// // Author           : Josh Irwin (joirwi)
// // Created          : 09/08/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 09/08/2023 12:43 PM
// // ***********************************************************************
// // <copyright file="cChoco.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules
{
    using UTMO.Text.FileGenerator.Provider.DSC.SubResources;

    public class cChoco : RequiredModule
    {
        private cChoco()
        {
        }
        
        public override string ModuleName => "cChoco";
        public override string ModuleVersion => "2.5.0";

        public override string RewriteModuleVersion => "2.5.0.0";
        
        public static RequiredModule Instance { get; } = new cChoco();
    }
}