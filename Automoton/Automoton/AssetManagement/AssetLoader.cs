using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Automoton.AssetManagement
{
    class AssetLoader
    {
        private Dictionary<string , AssetLoadFunction> LoadFuncMap { get; }

        public AssetLoader( Dictionary<string, AssetLoadFunction> loadFuncMap )
        {
            LoadFuncMap = loadFuncMap;
        }

        public Asset Load( XElement element , ContentManager contentManager ) =>
            LoadFuncMap[element.Attribute("Type").Value].Invoke( element , contentManager );

        public static Asset<T> Load<T>( XElement element , ContentManager contentManager )
        {
            var value = contentManager.Load<T>( element.Descendants( "Path" ).FirstOrDefault().Value );
            var id = Guid.Parse( element.Attribute( "Id" ).Value );

            return new Asset<T>( id , value );
        }

        public IEnumerable<Asset> Load( IEnumerable<XElement> elements , ContentManager contentManager ) =>
            elements
                .Where( e => e.Name == "Asset" )
                .Where( e => e.Attributes( "Id" ).Any() )
                .Where( e => e.Attributes( "Type" ).Any() )
                .Select( e => Load( e , contentManager ) );
    }
}
