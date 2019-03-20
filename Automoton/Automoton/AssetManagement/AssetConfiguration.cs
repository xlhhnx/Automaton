using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automoton.AssetManagement
{
    public class AssetConfiguration
    {
        public string BatchManifestPath { get; private set; }
        public Guid SplashBatch { get; private set; }

        public static void Load( string path )
        {
            // TODO : AssetConfigration.Load
        }
    }
}
