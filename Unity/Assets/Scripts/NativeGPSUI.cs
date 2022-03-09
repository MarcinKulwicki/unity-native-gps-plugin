using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class NativeGPSUI : MonoBehaviour
{
    public Text text;
    bool locationIsReady = false;

    private void Start() 
    {
        #if PLATFORM_IOS

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
}
