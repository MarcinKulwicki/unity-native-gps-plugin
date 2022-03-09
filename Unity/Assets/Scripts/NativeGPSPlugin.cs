using System.Runtime.InteropServices;
using UnityEngine;

public class NativeGPSPlugin : MonoBehaviour
{
    static NativeGPSPlugin instance = null;
    static GameObject go;

#region Dll imports for iOS
    [DllImport("__Internal")] private static extern void startLocation();
    [DllImport("__Internal")] private static extern double getLongitude();
    [DllImport("__Internal")] private static extern double getLatitude();
    [DllImport("__Internal")] private static extern double getAltitude();
    [DllImport("__Internal")] private static extern float getAccuracy();
    [DllImport("__Internal")] private static extern float getVerticalAccuracyMeters();
    [DllImport("__Internal")] private static extern float getSpeed();
    [DllImport("__Internal")] private static extern float getSpeedAccuracy();
#endregion

#region Init Singleton
    public static NativeGPSPlugin Instance 
	{
		get 
        {
			if(instance == null)
			{
				go = new GameObject();
				go.name = "NativeGPSPlugin";
				instance = go.AddComponent<NativeGPSPlugin>();
			}
		
			return instance; 
		}
	}
    void Awake() 
	{
		if (instance != null && instance != this) 
		{
			Destroy(this.gameObject);
		}
	}
#endregion

    public static bool StartLocation()
	{
        Instance.Awake();

        if(!Input.location.isEnabledByUser)
		{
			Debug.Log ("Location service disabled");
			return false;
		}

        #if UNITY_IOS

        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            startLocation();
        }
        
        #endif

        return true;
	}

#region Getting GPS properties

    public static double GetLongitude()
	{
        #if UNITY_IOS

        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return getLongitude();
        }

        #endif

        return 0;
	}

    public static double GetLatitude()
	{
        #if UNITY_IOS

        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return getLatitude();
        }
        
        #endif

        return 0; 
    }

    public static float GetAccuracy()
    {
        #if UNITY_IOS

        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return getAccuracy();
        }
        
        #endif

        return 0;
    }

    public static double GetAltitude()
    {
        #if UNITY_IOS

        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return getAltitude();
        }
        
        #endif

        return 0;
    }

    public static float GetSpeed()
    {
        #if UNITY_IOS

        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return getSpeed();
        }
        
        #endif

        return 0;
    }

    public static float GetSpeedAccuracyMetersPerSecond()
    {
        #if UNITY_IOS

        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return getSpeedAccuracy();
        }
        
        #endif

        return 0;
    }

    public static float GetVerticalAccuracyMeters()
    {
        #if UNITY_IOS

        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return getVerticalAccuracyMeters();
        }
        
        #endif

        return 0;
    }

#endregion

}
