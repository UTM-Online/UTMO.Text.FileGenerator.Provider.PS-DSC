// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 3:07 PM
// // ***********************************************************************
// // <copyright file="ManagedServiceAccount.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.SubResources
{
    using Attributes;
    using Enums;
    using Resources;

    public abstract class ManagedServiceAccount : RelatedTemplateResourceBase
    {
        public sealed override bool GenerateManifest => true;
        
        public sealed override string ResourceTypeName => $"{base.ResourceTypeName}/ManagedServiceAccount";
        
        public abstract string AccountName { get; }

        public abstract string DscDisplayName { get; }

        public virtual DscEnsure Ensure { get; } = DscEnsure.Present;
        
        public string ManagingPrinciples => string.Join(',', this._managingPrinciples.Select(x => $"\"{x.NodeName}$\""));
        
        [IgnoreMember]
        private List<DscComputer> _managingPrinciples = new();
        
        public ManagedServiceAccount RegisterPrinciple<T>() where T : DscComputer, new()
        {
            this._managingPrinciples.Add(new T());
            return this;
        }

        public override object? ToManifest()
        {
            return new
            {
                Name = this.AccountName,
                DscDisplayName = this.DscDisplayName,
                Ensure = this.Ensure,
                ManagingPrinciples = this._managingPrinciples.Select(x => $"{x.NodeName}$")
            };
        }
    }
}