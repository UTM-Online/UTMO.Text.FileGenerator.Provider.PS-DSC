﻿// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 3:11 PM
// // ***********************************************************************
// // <copyright file="NodeNetworkSettings.cs" company="Joshua S. Irwin">
// //     Copyright (c) 2026 Joshua S. Irwin. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes
{
    using System.Diagnostics.CodeAnalysis;
    using Models;
    using UTMO.Text.FileGenerator.Attributes;

    [SuppressMessage("ReSharper", "CollectionNeverQueried.Local")]
    public class NodeNetworkSettings : SubTemplateResourceBase
    {
        public sealed override bool GenerateManifest => false;
        
        [MemberName("primary_net_adapter")]
        public NetworkAdapter PrimaryNetworkAdapter { get; } = new NetworkAdapter()
        {
            Name = "Ethernet",
        };
        
        [MemberName("secondary_net_adapters")]
        private List<NetworkAdapter> SecondaryNetworkAdapters { get; } = new List<NetworkAdapter>();
        
        public NodeNetworkSettings AddSecondaryNetworkAdapter(NetworkAdapter networkAdapter)
        {
            this.SecondaryNetworkAdapters.Add(networkAdapter);
            return this;
        }
        
        public NodeNetworkSettings AddSecondaryNetworkAdapter(Action<NetworkAdapter> networkAdapter)
        {
            var adapter = new NetworkAdapter();
            networkAdapter(adapter);
            this.SecondaryNetworkAdapters.Add(adapter);
            return this;
        }
    }
}