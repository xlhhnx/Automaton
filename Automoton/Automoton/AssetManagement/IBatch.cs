using System.Collections.Generic;

namespace Automoton.AssetManagement
{
    interface IBatch : IEnumerable<Asset>
    {
        IBatch Reload();
        IBatch Unload();
    }
}