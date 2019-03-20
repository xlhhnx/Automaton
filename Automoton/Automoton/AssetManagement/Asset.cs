using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automoton.AssetManagement
{
    public abstract class Asset
    {
        public abstract Type ValueType { get; }
        public Guid Id { get; }
        public bool Loaded { get; private set; }

        public Asset( Guid id )
        {
            Id = id;
            Loaded = true;
        }

        public void Unload() =>
            Loaded = false;
    }

    class Asset<T> : Asset
    {
        public override Type ValueType { get => typeof( T ); }
        public T Value { get; }

        public Asset( Guid id , T value )
            : base( id )
        {
            Value = value;
        }
    }
}
