﻿// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 3:13 PM
// // ***********************************************************************
// // <copyright file="PrimaryIpAddress.cs" company="Joshua S. Irwin">
// //     Copyright (c) 2026 Joshua S. Irwin. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes
{
    using Models;

    public class PrimaryIpAddress : SubTemplateResourceBase
    {
        public override bool GenerateManifest => false;
        
        public string? IPv4Address { get; private set; }
        
        public string? IPv6Address { get; private set; }
        
        public PrimaryIpAddress SetIPv4Address(string address)
        {
            this.IPv4Address = address;
            return this;
        }
        
        public PrimaryIpAddress SetIPv6Address(string address)
        {
            this.IPv6Address = address;
            return this;
        }
    }
}