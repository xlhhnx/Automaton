using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automoton.AssetManagement
{
    class Batches : IEnumerable<IBatch>
    {
        private IEnumerable<IBatch> Contents { get; set; }

        public Batches()
        {
            Contents = new List<IBatch>();
        }

        public Batches( IEnumerable<IBatch> contents )
        {
            Contents = new List<IBatch>( contents );
        }

        public IEnumerator<IBatch> GetEnumerator() =>
            Contents.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            Contents.GetEnumerator();

        public IEnumerable<Asset> AssetsWhere( Func<Asset, bool> predicate ) =>
            Contents.SelectMany( c => c.Where( predicate ) );

        public void Add( IBatch batch ) =>
            Contents.ToList().Add( batch );
    }
}
