// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 3:45 PM
// // ***********************************************************************
// // <copyright file="PSDesiredStateConfiguration.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Models.Instances.Module
{
    using UTMO.Text.FileGenerator.Provider.DSC.SubResources;

    public class PSDesiredStateConfiguration : RequiredModule
    {
        public override bool GenerateManifest => false;

        public override string ModuleName => "PSDesiredStateConfiguration";

        public override string ModuleVersion => "";
    }
}