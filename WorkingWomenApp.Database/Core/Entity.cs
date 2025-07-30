using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWomenApp.Database.Core
{
    public abstract class Entity 
    {
        public Guid Id { get;  set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get;  set; }
        public DateTime? DeletedOn { get;  set; }
        public Boolean? IsDeleted { get;  set; } = false;

        private int? _hashCode;
    



        public Entity Create()
        {
            this.CreatedOn = DateTime.UtcNow;
            return this;
        }

        public Entity Update()
        {
            if (CreatedOn == default)
                throw new Exception("Entity State is not valid for updating");

            this.ModifiedOn = DateTime.UtcNow;
            return this;
        }

        public Entity Delete()
        {
            if (CreatedOn == default)
                throw new Exception("Entity State is not valid for deleting");

            this.DeletedOn = DateTime.UtcNow;
            this.IsDeleted = true;
            return this;
        }

        public bool IsTransient()
        {
            return this.Id == default;
        }

        public bool Equals(Entity? other)
        {
            if (other is null)
                return false;

            if (this.GetType() != other.GetType())
                return false;

            if (Object.ReferenceEquals(this, other))
                return true;

            Entity item = other;

            if (item.IsTransient() || this.IsTransient())
                return false;

            return item.Id == this.Id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Entity)
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (IsTransient())
                return base.GetHashCode();

            if (!_hashCode.HasValue)
                _hashCode = this.Id.GetHashCode() ^ 31;

            return _hashCode.Value;
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null));

            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
