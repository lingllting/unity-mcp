using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace MCPForUnity.Editor.Helpers
{
    /// <summary>
    /// Compatibility helper for PackageInfo APIs across Unity versions.
    /// GetAllRegisteredPackages() was added in Unity 2021.1.
    /// </summary>
    internal static class PackageCompatHelper
    {
        internal static PackageInfo[] GetAllPackages()
        {
#if UNITY_2021_1_OR_NEWER
            return PackageInfo.GetAllRegisteredPackages();
#else
            // Fallback for Unity 2020: use Client.List() synchronously
            var request = Client.List(true, false);
            while (!request.IsCompleted)
                System.Threading.Thread.Sleep(10);

            if (request.Status == StatusCode.Success)
                return request.Result.ToArray();

            return new PackageInfo[0];
#endif
        }
    }
}
