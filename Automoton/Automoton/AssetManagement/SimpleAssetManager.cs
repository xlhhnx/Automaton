using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automoton.AssetManagement
{
    class SimpleAssetManager : IAssetManager
    {
        private Manifest BatchManifest { get; set; }
        private Batches Batches { get; set; }
        private AssetCache AssetCache { get; set; }
        private IServiceProvider ServiceProvider { get; }

        public SimpleAssetManager(string path , IServiceProvider serviceProvider)
        {
            BatchManifest = new Manifest();
            Batches = new Batches();
            AssetCache = new AssetCache(200);
            ServiceProvider = serviceProvider;

            BatchManifest.Load( path , e => e.Name == "Batch" );
        }

        public Asset GetAsset( Guid Id )
        {
            var asset = AssetCache.Where( a => a.Id == Id && a.Loaded ).FirstOrDefault();

            if ( !ReferenceEquals( null , asset ) )
                return asset;

            asset = Batches.AssetsWhere( a => a.Id == Id ).FirstOrDefault();

            if ( !ReferenceEquals( null , asset ) )
                throw new ArgumentException( $"Asset {Id} is not loaded." );

            AssetCache += asset;
            return asset;
        }

        public void UnloadAll()
        {
            Batches = new Batches(
                Batches.Select( b => b.Unload() )
                );
            AssetCache = new AssetCache(200);
        }

        public void LoadBatch( Guid id ) =>
            Batches.Add( 
                new SimpleBatch( 
                    BatchManifest
                        .Where( mr => mr.Id == id )
                        .FirstOrDefault() , 
                    ServiceProvider 
                    ) 
                );
    }
}
