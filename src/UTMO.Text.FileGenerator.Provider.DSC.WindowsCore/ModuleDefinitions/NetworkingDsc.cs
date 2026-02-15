﻿// // ***********************************************************************
// // Assembly         : UTMO.DSC.Configurations
// // Author           : Josh Irwin (joirwi)
// // Created          : 09/08/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 09/08/2023 12:02 PM
// // ***********************************************************************
// // <copyright file="NetworkingDsc.cs" company="Joshua S. Irwin">
// //     Copyright (c) 2026 Joshua S. Irwin. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.ModuleDefinitions
{
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

    public class NetworkingDsc : RequiredModule
    {
        private NetworkingDsc()
        {
        }
        
        public override string ModuleName => "NetworkingDsc";
        public override string ModuleVersion => "9.1.0";
        
        public static RequiredModule Instance { get; } = new NetworkingDsc();
    }
}