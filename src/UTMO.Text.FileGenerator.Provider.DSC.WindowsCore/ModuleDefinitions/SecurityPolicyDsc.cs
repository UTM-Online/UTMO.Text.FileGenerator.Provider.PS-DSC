// // ***********************************************************************
// // Assembly         : UTMO.DSC.Configurations
// // Author           : Josh Irwin (joirwi)
// // Created          : 09/08/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 09/08/2023 12:11 PM
// // ***********************************************************************
// // <copyright file="SecurityPolicyDsc.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.ModuleDefinitions
{
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

    public class SecurityPolicyDsc : RequiredModule
    {
        private SecurityPolicyDsc()
        {
        }
        
        public override string ModuleName => "SecurityPolicyDsc";

        public override string ModuleVersion => "2.10.0";

        public static RequiredModule Instance { get; } = new SecurityPolicyDsc();
    }
}