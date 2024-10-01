// // ***********************************************************************
// // Assembly         : UTMO.DSC.Configuration.Common
// // Author           : Josh Irwin (joirwi)
// // Created          : 09/08/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 09/08/2023 12:52 PM
// // ***********************************************************************
// // <copyright file="SqlServerDsc.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules
{
    using UTMO.Text.FileGenerator.Provider.DSC.SubResources;

    public class SqlServerDsc : RequiredModule
    {
        private SqlServerDsc()
        {
        }
        
        public override string ModuleName => "SqlServerDsc";
        public override string ModuleVersion => "16.3.0";
        
        public static RequiredModule Instance { get; } = new SqlServerDsc();
    }
}