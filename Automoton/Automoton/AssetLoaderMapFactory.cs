using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Automoton.AssetManagement;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Automoton
{
    public static class AssetLoaderMapFactory
    {
        public static Dictionary<string , AssetLoadFunction> GetBasicContentsMap() =>
            new Dictionary<string , AssetLoadFunction>()
            {
                { "Texture" , new AssetLoadFunction (AssetLoader.Load<Texture>) },
                { "Font" , new AssetLoadFunction (AssetLoader.Load<SpriteFont>) },
                { "Effect" , new AssetLoadFunction (AssetLoader.Load<Effect>) },
                { "SoundEffect" , new AssetLoadFunction (AssetLoader.Load<SoundEffect>) },
                { "Song" , new AssetLoadFunction (AssetLoader.Load<Song>) }
            };
    }
}