using System;

namespace Dev2.Common
{
    public class MaintainerApi : IEquatable<MaintainerApi>
    {
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(MaintainerApi other)
        {
            if(ReferenceEquals(null, other))
            {
                return false;
            }
            if(ReferenceEquals(this, other))
            {
                return true;
            }
            return string.Equals(Fn, other.Fn) && string.Equals(Email, other.Email) && string.Equals(Url, other.Url) && string.Equals(Org, other.Org) && string.Equals(Adr, other.Adr) && string.Equals(Tel, other.Tel) && string.Equals(XTwitter, other.XTwitter) && string.Equals(XGithub, other.XGithub) && string.Equals(Photo, other.Photo) && string.Equals(VCard, other.VCard);
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
            return Equals((MaintainerApi)obj);
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
                var hashCode = (Fn != null ? Fn.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Email != null ? Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Url != null ? Url.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Org != null ? Org.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Adr != null ? Adr.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Tel != null ? Tel.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (XTwitter != null ? XTwitter.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (XGithub != null ? XGithub.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Photo != null ? Photo.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (VCard != null ? VCard.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(MaintainerApi left, MaintainerApi right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MaintainerApi left, MaintainerApi right)
        {
            return !Equals(left, right);
        }
        [Newtonsoft.Json.JsonProperty("FN")]
        public string Fn { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        [Newtonsoft.Json.JsonProperty("org")]
        public string Org { get; set; }
        public string Adr { get; set; }
        public string Tel { get; set; }
        [Newtonsoft.Json.JsonProperty("X-Twitter")]
        public string XTwitter { get; set; }
        [Newtonsoft.Json.JsonProperty("X-Github")]
        public string XGithub { get; set; }
        [Newtonsoft.Json.JsonProperty("photo")]
        public string Photo { get; set; }
        [Newtonsoft.Json.JsonProperty("vCard")]
        public string VCard { get; set; }
    }
}