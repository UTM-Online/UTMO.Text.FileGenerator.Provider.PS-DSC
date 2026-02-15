﻿// // ***********************************************************************
// // Assembly         : UTMO.DSC.Configurations
// // Author           : Josh Irwin (joirwi)
// // Created          : 09/08/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 09/08/2023 12:09 PM
// // ***********************************************************************
// // <copyright file="PackageManagement.cs" company="Joshua S. Irwin">
// //     Copyright (c) 2026 Joshua S. Irwin. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.ModuleDefinitions
{
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

    public class PackageManagement : RequiredModule
    {
        private PackageManagement()
        {
        }
        
        public override string ModuleName => "PackageManagement";
        public override string ModuleVersion => "1.4.8.1";

        public override bool AllowClobber => true;
        
        public static RequiredModule Instance { get; } = new PackageManagement();
    }
}