using Microsoft.Xna.Framework.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automoton.AssetManagement
{
    class UnloadedBatch : IBatch
    {
        private ManifestRecord Record { get; }
        private ContentManager ContentManager { get; }

        public UnloadedBatch( ManifestRecord record , ContentManager contentManager )
        {
            Record = record;
            ContentManager = contentManager;
        }

        public IEnumerator<Asset> GetEnumerator() =>
            new List<Asset>().GetEnumerator();

        public IBatch Reload() =>
            new SimpleBatch( Record , ContentManager );

        public IBatch Unload() =>
            this;

        IEnumerator IEnumerable.GetEnumerator()=>
            new List<Asset>().GetEnumerator();
    }
}
