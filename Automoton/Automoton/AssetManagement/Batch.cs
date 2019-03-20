using Microsoft.Xna.Framework.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Automoton.AssetManagement
{
    class SimpleBatch : IBatch
    {
        private Assets Assets { get; }
        private ContentManager ContentManager { get; }
        private ManifestRecord Record { get; }

        public SimpleBatch( ManifestRecord record , IServiceProvider serviceProvider)
        {
            Record = record;
            ContentManager = new ContentManager(
                serviceProvider ,
                GetRootDirectory( Record.Id , record.Path )
                );
            
            Assets = new Assets();
            Load( record.Id , record.Path );
        }

        private string GetRootDirectory( Guid id , string path ) =>
            XElement
                .Load( path )
                .Descendants( "Batch" )
                .Where( d => Guid.Parse( d.Attribute( "Id" ).Value ) == id )
                .FirstOrDefault()
                .Attribute( "RootDirectory" )
                .Value;

        public  SimpleBatch( ManifestRecord record , ContentManager contentManager )
        {
            Record = record;
            ContentManager = contentManager;
            
            Assets = new Assets();
            Load( record.Id , record.Path );
        }

        public virtual IBatch Unload()
        {
            Assets.ToList().ForEach( a => a.Unload() );
            ContentManager.Unload();
            return new UnloadedBatch( Record , ContentManager );
        }

        public IBatch Reload() =>
            this
                .Unload()
                .Reload();

        private void Load( Guid id , string path ) =>
            Assets.AddMany(
                Automaton.Instance.AssetLoader.Load(
                    XElement
                        .Load( path )
                        .Descendants( "Batch" )
                        .Where( d => Guid.Parse( d.Attribute( "Id" ).Value ) == id )
                        .FirstOrDefault()
                        .Descendants( "Asset" ) ,
                    ContentManager
                    )
                );

        public IEnumerator<Asset> GetEnumerator() =>
            Assets.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            Assets.GetEnumerator();
    }
}
