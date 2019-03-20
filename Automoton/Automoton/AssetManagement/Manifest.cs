using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Automoton.AssetManagement
{
    class Manifest : IEnumerable<ManifestRecord>
    {
        private IEnumerable<ManifestRecord> Contents { get; set; }

        public Manifest()
        {
            Clear();
        }

        public Manifest( IEnumerable<ManifestRecord> contents )
        {
            Contents = new List<ManifestRecord>( contents );
        }

        public void Load( string path , Func<XElement , bool> predicate ) =>
            Contents.ToList().AddRange(
                XElement
                    .Load( path )
                    .Descendants()
                    .Where( predicate )
                    .Select( x => new ManifestRecord( Guid.Parse( x.Attribute( "Id" ).Value ) , path ) )
                );

        public ManifestRecord GetRecord( Guid id ) =>
            Contents.Where( mr => mr.Id == id ).FirstOrDefault();

        public void Clear() =>
            Contents = new List<ManifestRecord>();

        public IEnumerator<ManifestRecord> GetEnumerator() =>
            Contents.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            Contents.GetEnumerator();
    }
}
