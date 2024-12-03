// // ***********************************************************************
// // Assembly         : UTMO.DSC.Configuration.Common
// // Author           : Josh Irwin (joirwi)
// // Created          : 09/08/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 09/08/2023 12:48 PM
// // ***********************************************************************
// // <copyright file="UTMO_Infra_DSC_DotNetCore.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules
{
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

    public class UTMO_Infra_DSC_DotNetCore : RequiredModule
    {
        private UTMO_Infra_DSC_DotNetCore()
        {
        }
        
        public override string ModuleName => "UTMO.Infra.DSC.DotNetCore";
        public override string ModuleVersion => "1.0.10";

        public override bool IsPrivate => true;
        
        public static RequiredModule Instance { get; } = new UTMO_Infra_DSC_DotNetCore();
    }
}