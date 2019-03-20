using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automoton.Common;

namespace Automoton.AssetManagement
{
    class AssetCache : Assets
    {
        public int CacheSize { get; }

        public AssetCache( int cacheSize )
            : base()
        {
            CacheSize = cacheSize;
        }

        public AssetCache( int cacheSize , IEnumerable<Asset> contents )
            : base( contents.Reduce(cacheSize) )
        {
            CacheSize = cacheSize;
        }

        public AssetCache IncreaseCacheSize( int value ) =>
            new AssetCache(
                CacheSize + value ,
                Contents
                );

        public AssetCache DecreaseCacheSize( int value ) =>
            new AssetCache(
                CacheSize - value ,
                Contents
                );

        public override Assets Remove( Asset asset ) =>
            asset is null
                ? this
                : new AssetCache(
                    CacheSize ,
                    Contents
                        .Remove( asset )
                    );

        public override Assets RemoveMany( IEnumerable<Asset> assets ) =>
            assets is null
                ? this
                : new AssetCache(
                    CacheSize ,
                    Contents
                        .RemoveRange( assets )
                    );

        public override Assets Add( Asset asset ) =>
            asset is null
                ? this
                : new AssetCache(
                    CacheSize ,
                    Contents
                        .Add( asset )
                        .Reduce( CacheSize )
                    );

        public override Assets AddMany( IEnumerable<Asset> assets ) =>
            assets is null 
                ? this
                : new AssetCache(
                    CacheSize ,
                    Contents
                        .AddRange( assets )
                        .Reduce( CacheSize )
                    );

        public static AssetCache operator +( AssetCache assetCache , int value ) =>
            assetCache is null
                ? throw new ArgumentNullException( "The assetCache is null." )
                : assetCache.IncreaseCacheSize( value );

        public static AssetCache operator +( AssetCache assetCache , Asset asset ) =>
            assetCache is null
                ? throw new ArgumentNullException( "The assetCache is null." )
                : assetCache.Add( asset ) as AssetCache;
        
        public static AssetCache operator +( AssetCache assetCache , IEnumerable<Asset> assets ) =>
            assetCache is null
                ? throw new ArgumentNullException( "The assetCache is null." )
                : assetCache.AddMany( assets ) as AssetCache;

        public static AssetCache operator -( AssetCache assetCache , int value) =>
            assetCache is null
                ? throw new ArgumentNullException( "The assetCache is null." )
                : assetCache.DecreaseCacheSize( value );

        public static AssetCache operator -( AssetCache assetCache , Asset asset ) =>
            assetCache is null
                ? throw new ArgumentNullException( "The assetCache is null." )
                : assetCache.Remove( asset ) as AssetCache;
        
        public static AssetCache operator -( AssetCache assetCache , IEnumerable<Asset> assets ) =>
            assetCache is null
                ? throw new ArgumentNullException( "The assetCache is null." )
                : assetCache.RemoveMany( assets ) as AssetCache;
    }
}
