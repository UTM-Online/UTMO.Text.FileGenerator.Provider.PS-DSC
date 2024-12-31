// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 3:10 PM
// // ***********************************************************************
// // <copyright file="NetworkAdapter.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes
{
    using Models;
    using UTMO.Text.FileGenerator.Attributes;

    public class NetworkAdapter : SubTemplateResourceBase
    {
        public sealed override bool GenerateManifest => false;
        
        [MemberName("name")]
        public string Name { get; set; } = null!;

        [MemberName("primary_ip")]
        public PrimaryIpAddress PrimaryIpAddress { get; } = new PrimaryIpAddress();
        
        [MemberName("alternate_ips")]
        public AlternateIpAddresses AlternateIpAddresses { get; } = new AlternateIpAddresses();
    }
}