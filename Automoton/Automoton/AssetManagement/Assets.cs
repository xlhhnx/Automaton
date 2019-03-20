using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automoton.Common;

namespace Automoton.AssetManagement
{
    class Assets : IEnumerable<Asset>
    {
        protected IEnumerable<Asset> Contents { get; set; }

        public Assets()
        {
            Contents = new List<Asset>();
        }

        public Assets( IEnumerable<Asset> contents )
        {
            Contents = new List<Asset>( contents );
        }

        public IEnumerator<Asset> GetEnumerator() =>
            Contents.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            Contents.GetEnumerator();

        public virtual Assets Remove( Asset asset ) =>
            asset is null 
                ? this
                : new Assets(
                    Contents
                        .Remove( asset )
                    );

        public virtual Assets RemoveMany( IEnumerable<Asset> assets ) =>
            assets is null 
                ? this
                : new Assets(
                    Contents
                        .RemoveRange( assets )
                    );

        public virtual Assets Add( Asset asset ) =>
                asset is null 
                ? this
                : new Assets(
                    Contents
                        .Add( asset )
                    );

        public virtual Assets AddMany( IEnumerable<Asset> assets ) =>
                assets is null 
                ? this
                : new Assets(
                    Contents
                        .AddRange( assets )
                    );

        public static Assets operator +( Assets assets , Asset other ) =>
            assets is null
                ? throw new ArgumentNullException( "The assetCache is null." )
                : assets.Add( other );
        
        public static Assets operator +( Assets assets , IEnumerable<Asset> other ) =>
            assets is null
                ? throw new ArgumentNullException( "The assetCache is null." )
                : assets.AddMany( other );

        public static Assets operator -( Assets assets , Asset other ) =>
            assets is null
                ? throw new ArgumentNullException( "The assetCache is null." )
                : assets.Remove( other );
        
        public static Assets operator -( Assets assets , IEnumerable<Asset> other ) =>
            assets is null
                ? throw new ArgumentNullException( "The assetCache is null." )
                : assets.RemoveMany( other );
    }
}
