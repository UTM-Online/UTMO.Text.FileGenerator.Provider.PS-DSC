// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 2:40 PM
// // ***********************************************************************
// // <copyright file="DscManagedAdServiceAccounts.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Resources
{
    using Attributes;
    using BaseTypes;
    using Enums;
    using Instances.Module;
    using SubResources;

    public abstract class DscManagedAdServiceAccounts : DscResourceBase
    {
        // ReSharper disable once PublicConstructorInAbstractClass
        public DscManagedAdServiceAccounts()
        {
            this.RequiredModules.Add(new ActiveDirectoryDsc());
        }

        public sealed override string ResourceTypeName => "/DSC/Configurations/AdServiceAccounts";
        
        public string Description => "This configuration is used to manage Active Directory Service Accounts.";

        protected DscMode Mode => DscMode.Push;
        
        public string TemplateName => "AdServiceAccount";
        
        public List<RequiredModule> RequiredModules { get; } = new List<RequiredModule>();
        
        [MemberName("managed_service_accounts")]
        // ReSharper disable once CollectionNeverQueried.Local
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private List<ManagedServiceAccount> _managedServiceAccounts = new();
        
        public DscManagedAdServiceAccounts AddManagedServiceAccount<T>() where T : ManagedServiceAccount, new()
        {
            this._managedServiceAccounts.Add(new T());
            return this;
        }

        public sealed override dynamic? ToManifest()
        {
            return new
            {
                Accounts = this._managedServiceAccounts.Select(x => x.ToManifest())
            };
        }
    }
}