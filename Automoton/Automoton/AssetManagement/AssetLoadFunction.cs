using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Automoton.AssetManagement
{
    public class AssetLoadFunction
    {
        private Func<XElement , ContentManager , Asset> Func { get; }

        public AssetLoadFunction( Func<XElement , ContentManager , Asset> func )
        {
            Func = func;
        }

        public Asset Invoke( XElement element , ContentManager contentManager ) =>
            Func( element , contentManager );
    }
}
