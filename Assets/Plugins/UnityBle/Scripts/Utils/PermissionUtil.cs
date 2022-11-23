using UnityEngine;
# if PLATFORM_ANDROID
using UnityEngine.Android;
# endif

namespace BlePlugin.Util {
    public class PermissionUtil 
    {
        public static void RequestLocation() {
            #if PLATFORM_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
            {
                Permission.RequestUserPermission(Permission.CoarseLocation);
            }

            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
            }
            #endif
        }
    }
}
