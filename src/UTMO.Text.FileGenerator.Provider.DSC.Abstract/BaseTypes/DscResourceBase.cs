﻿// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 2:34 PM
// // ***********************************************************************
// // <copyright file="DscResourceBase.cs" company="Joshua S. Irwin">
// //     Copyright (c) 2026 Joshua S. Irwin. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes
{
    using Models;

    public abstract class DscResourceBase : TemplateResourceBase
    {
        public sealed override string OutputExtension => ".ps1";

        public override bool GenerateManifest => true;
    }
}