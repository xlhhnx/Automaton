using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automoton.AssetManagement
{
    interface IAssetManager
    {
        void LoadBatch( Guid id );
        void UnloadAll();
        Asset GetAsset( Guid Id );
    }
}
