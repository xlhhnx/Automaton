using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automoton.AssetManagement
{
    class ManifestRecord : IEquatable<ManifestRecord>
    {
        public Guid Id { get; private set; }
        public string Path { get; private set; }

        public ManifestRecord( Guid id , string path )
        {
            Id = id;
            Path = path;
        }

        public override bool Equals( object obj ) =>
            Equals( obj as ManifestRecord );

        public bool Equals( ManifestRecord other ) =>
            !ReferenceEquals( null , other ) &&
            Id == other.Id &&
            Path == other.Path;

        public override int GetHashCode()
        {
            var hashCode = 1763280214;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode( Id );
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode( Path );
            return hashCode;
        }

        public static bool operator ==( ManifestRecord a , ManifestRecord b ) =>
            !ReferenceEquals( null , a ) &&
            a.Equals( b );

        public static bool operator !=( ManifestRecord a , ManifestRecord b ) =>
            !(a == b);
    }
}
