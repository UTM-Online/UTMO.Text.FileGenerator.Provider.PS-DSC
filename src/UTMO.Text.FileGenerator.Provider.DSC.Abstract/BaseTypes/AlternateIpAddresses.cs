﻿// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 3:06 PM
// // ***********************************************************************
// // <copyright file="AlternateIpAddresses.cs" company="Joshua S. Irwin">
// //     Copyright (c) 2026 Joshua S. Irwin. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes
{
    using Models;

    public class AlternateIpAddresses : SubTemplateResourceBase
    {
        public sealed override bool GenerateManifest => false;
        
        public IReadOnlyList<string> IPv4Addresses => this._ipv4;
        
        public IReadOnlyList<string> IPv6Addresses => this._ipv6;
        
        public AlternateIpAddresses AddIPv4Address(string address)
        {
            this._ipv4.Add(address);
            return this;
        }
        
        public AlternateIpAddresses AddIPv6Address(string address)
        {
            this._ipv6.Add(address);
            return this;
        }
        
        private List<string> _ipv4 { get; } = new List<string>();
        
        private List<string> _ipv6 { get; } = new List<string>();
    }
}