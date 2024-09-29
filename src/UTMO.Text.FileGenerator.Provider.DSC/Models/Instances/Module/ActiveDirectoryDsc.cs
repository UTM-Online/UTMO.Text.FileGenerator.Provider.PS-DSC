// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 3:44 PM
// // ***********************************************************************
// // <copyright file="ActiveDirectoryDsc.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Resources.Instances.Module
{
    using SubResources;

    public class ActiveDirectoryDsc : RequiredModule
    {
        public sealed override string ModuleName => "ActiveDirectoryDsc";
        
        public override string ModuleVersion => "6.2.0";
    }
}