﻿// // ***********************************************************************
// // Assembly         : UTMO.DSC.Configuration.Common
// // Author           : Josh Irwin (joirwi)
// // Created          : 09/08/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 09/08/2023 12:41 PM
// // ***********************************************************************
// // <copyright file="ComputerManagementDsc.cs" company="Joshua S. Irwin">
// //     Copyright (c) 2026 Joshua S. Irwin. All rights reserved.
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
        public override string ModuleVersion => "10.0.0";
        
        public static RequiredModule Instance { get; } = new ComputerManagementDsc();
    }
}