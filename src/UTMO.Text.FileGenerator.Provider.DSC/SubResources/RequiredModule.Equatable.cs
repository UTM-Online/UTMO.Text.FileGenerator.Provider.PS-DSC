// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 3:28 PM
// // ***********************************************************************
// // <copyright file="RequiredModule.Equatable.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.SubResources
{
    public abstract partial class RequiredModule : IEquatable<RequiredModule>
    {
        public bool Equals(RequiredModule? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.ModuleName == other.ModuleName && this.ModuleVersion == other.ModuleVersion;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((RequiredModule)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.ModuleName, this.ModuleVersion, this.IsPrivate, this.AllowClobber, this.RewriteModuleVersion, this.UseAlternateFormat);
        }
    }
}