// // ***********************************************************************
// // Assembly         : UTMO.DSC.Configuration.Common
// // Author           : Josh Irwin (joirwi)
// // Created          : 09/08/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 09/08/2023 12:51 PM
// // ***********************************************************************
// // <copyright file="WmiNamespaceSecurity.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.ModuleDefinitions
{
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

    public class WmiNamespaceSecurity : RequiredModule
    {
        private WmiNamespaceSecurity()
        {
        }
        
        public override string ModuleName => "WmiNamespaceSecurity";
        public override string ModuleVersion => "0.3.0";
        
        public static RequiredModule Instance { get; } = new WmiNamespaceSecurity();
    }
}