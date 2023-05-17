using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPCSharp.Models
{
    public class Entity
    {
        private Guid _id = Guid.NewGuid();
        protected Entity() { }

        protected Entity(Guid id)
        {
            this._id = id;
        }
        public virtual Guid id { get { return _id; }
            set { _id = value; } }

        public virtual Guid GetGuid() { return _id; }

        public virtual void setGuid(Guid id)
        {
            this._id = id;
        } 
        public override string ToString()
        {
            return "Entity{" +
                "id=" + _id +
                '}';
        }
    }
}
