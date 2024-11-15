// // ***********************************************************************
// // Assembly         : UTMO.DSC.Configuration.Common
// // Author           : Josh Irwin (joirwi)
// // Created          : 09/08/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 09/08/2023 12:45 PM
// // ***********************************************************************
// // <copyright file="cAzureKeyVault.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules
{
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

    public class cAzureKeyVault : RequiredModule
    {
        private cAzureKeyVault()
        {
        }
        
        public override string ModuleName => "cAzureKeyVault";
        public override string ModuleVersion => "1.0.4";
        
        public static RequiredModule Instance { get; } = new cAzureKeyVault();
    }
}