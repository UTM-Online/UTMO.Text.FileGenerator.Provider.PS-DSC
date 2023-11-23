// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 3:11 PM
// // ***********************************************************************
// // <copyright file="NodeNetworkSettings.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.SubResources
{
    using Attributes;

    public class NodeNetworkSettings : RelatedTemplateResourceBase
    {
        public sealed override bool GenerateManifest => false;
        
        [MemberName("primary_net_adapter")]
        public NetworkAdapter PrimaryNetworkAdapter { get; } = new NetworkAdapter()
        {
            Name = "Ethernet"
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