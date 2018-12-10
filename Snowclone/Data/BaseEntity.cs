using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowclone.Data
{
    public class BaseEntity : IEquatable<BaseEntity>
    {
        //public int Id { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity);
        }

        bool IEquatable<BaseEntity>.Equals(BaseEntity other)
        {
            return Equals(other);
        }

        public virtual bool Equals(BaseEntity other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(BaseEntity A, BaseEntity B)
        {
            return Equals(A, B);
        }

        public static bool operator !=(BaseEntity A, BaseEntity B)
        {
            return !(A == B);
        }
    }
}
