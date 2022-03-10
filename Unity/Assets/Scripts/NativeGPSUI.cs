using System.Text;
using UnityEngine;
using UnityEngine.UI;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class NativeGPSUI : MonoBehaviour
{
    public Text text;
    bool locationIsReady = false;
    bool locationGrantedAndroid = false;
    GameObject dialog = null;

    private void Start() 
    {
        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            dialog = new GameObject();
        }
        else
        {
            locationGrantedAndroid = true;
            locationIsReady = NativeGPSPlugin.StartLocation();
        }

        #elif PLATFORM_IOS

        locationIsReady = NativeGPSPlugin.StartLocation();
    
        #endif
    }

    private void Update() 
    {
        if (locationIsReady)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Longitude: "+NativeGPSPlugin.GetLongitude());
            sb.AppendLine("Latitude: "+NativeGPSPlugin.GetLatitude());
            sb.AppendLine("Accuracy: "+NativeGPSPlugin.GetAccuracy());
            sb.AppendLine("Altitude: "+NativeGPSPlugin.GetAltitude());
            sb.AppendLine("Speed: "+NativeGPSPlugin.GetSpeed());
            sb.AppendLine("Speed Accuracy Meters Per Second: "+NativeGPSPlugin.GetSpeedAccuracyMetersPerSecond());
            sb.AppendLine("Vertical Accuracy Meters: "+NativeGPSPlugin.GetVerticalAccuracyMeters());

            text.text = sb.ToString();
        }
    }

    void OnGUI ()
    {
        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            // The user denied permission to use the fineLocation.
            // Display a message explaining why you need it with Yes/No buttons.
            // If the user says yes then present the request again
            // Display a dialog here.
            dialog.AddComponent<PermissionsRationaleDialog>();
            return;
        }
        else if (dialog != null)
        {
            if (!locationGrantedAndroid)
            {
                locationGrantedAndroid = true;
                locationIsReady = NativeGPSPlugin.StartLocation();
            }

            Destroy(dialog);
        }
        #endif
    }
}
