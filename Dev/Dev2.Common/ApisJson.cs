using System;
using System.Collections.Generic;

namespace Dev2.Common
{
    public class ApisJson : IEquatable<ApisJson>
    {
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(ApisJson other)
        {
            if(ReferenceEquals(null, other))
            {
                return false;
            }
            if(ReferenceEquals(this, other))
            {
                return true;
            }
            return string.Equals(Name, other.Name) && string.Equals(Description, other.Description) && string.Equals(Image, other.Image) && string.Equals(Url, other.Url) &&  Created.Equals(other.Created) && Modified.Equals(other.Modified) && string.Equals(SpecificationVersion, other.SpecificationVersion);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj))
            {
                return false;
            }
            if(ReferenceEquals(this, obj))
            {
                return true;
            }
            if(obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((ApisJson)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Image != null ? Image.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Url != null ? Url.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Tags != null ? Tags.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Created.GetHashCode();
                hashCode = (hashCode * 397) ^ Modified.GetHashCode();
                hashCode = (hashCode * 397) ^ (SpecificationVersion != null ? SpecificationVersion.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Apis != null ? Apis.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Include != null ? Include.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Maintainers != null ? Maintainers.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(ApisJson left, ApisJson right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ApisJson left, ApisJson right)
        {
            return !Equals(left, right);
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public List<string> Tags { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string SpecificationVersion { get; set; }
        public List<SingleApi> Apis { get; set; }
        public List<IncludeApi> Include { get; set; }
        public List<MaintainerApi> Maintainers { get; set; }
    }
}