using System.Runtime.InteropServices;
using UnityEngine;

public class NativeGPSPlugin : MonoBehaviour
{
    static NativeGPSPlugin instance = null;
    static GameObject go;

#if UNITY_ANDROID
	static AndroidJavaClass obj;

#endif

#region Dll imports for iOS
    #if UNITY_IOS
    [DllImport("__Internal")] private static extern void startLocation();
    [DllImport("__Internal")] private static extern double getLongitude();
    [DllImport("__Internal")] private static extern double getLatitude();
    [DllImport("__Internal")] private static extern double getAltitude();
    [DllImport("__Internal")] private static extern float getAccuracy();
    [DllImport("__Internal")] private static extern float getVerticalAccuracyMeters();
    [DllImport("__Internal")] private static extern float getSpeed();
    [DllImport("__Internal")] private static extern float getSpeedAccuracy();
    #endif
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

                #if UNITY_ANDROID

				if(Application.platform == RuntimePlatform.Android)
					obj = new AndroidJavaClass("com.marcinkulwicki.localetools.Main");

				#endif
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

        #elif UNITY_ANDROID
        
        if(Application.platform == RuntimePlatform.Android)
        {
            obj.CallStatic("startLocation");
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

        #elif UNITY_ANDROID
        
            return (double) Get(NativeAndroidFunction.GET_LONGITUDE);

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

        #elif UNITY_ANDROID
        
            return (double) Get(NativeAndroidFunction.GET_LATITUDE);
        
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

        #elif UNITY_ANDROID
        
            return (float) Get(NativeAndroidFunction.GET_ACCURACY);
        
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

        #elif UNITY_ANDROID
        
            return (double) Get(NativeAndroidFunction.GET_ALTITUDE);
        
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

        #elif UNITY_ANDROID
        
            return (float) Get(NativeAndroidFunction.GET_SPEED);
        
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

        #elif UNITY_ANDROID
        
            return (float) Get(NativeAndroidFunction.GET_SPEED_ACCURACY_METERS_PER_SECOND);
        
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

        #elif UNITY_ANDROID
        
            return (float) Get(NativeAndroidFunction.GET_VERTICAL_ACCURACY_METERS);
        
        #endif

        return 0;
    }

#endregion

#region Android functions
    #if UNITY_ANDROID
    
    private static object Get(NativeAndroidFunction functionName)
    {
        Instance.Awake();

        if(!Input.location.isEnabledByUser)
        {
            return 0;
        }
        
        if(Application.platform == RuntimePlatform.Android)
        {
            switch(functionName)
            {
                case NativeAndroidFunction.GET_LONGITUDE:
                    return obj.CallStatic<double>("getLongitude");

                case NativeAndroidFunction.GET_LATITUDE:
                    return obj.CallStatic<double>("getLatitude");
                
                case NativeAndroidFunction.GET_ACCURACY:
                    return obj.CallStatic<float>("getAccuracy");

                case NativeAndroidFunction.GET_ALTITUDE:
                    return obj.CallStatic<double>("getAltitude");

                case NativeAndroidFunction.GET_SPEED:
                    return obj.CallStatic<float>("getSpeed");

                case NativeAndroidFunction.GET_SPEED_ACCURACY_METERS_PER_SECOND:
                    return obj.CallStatic<float>("getSpeedAccuracyMetersPerSecond");

                case NativeAndroidFunction.GET_VERTICAL_ACCURACY_METERS:
                    return obj.CallStatic<float>("getVerticalAccuracyMeters");
            }
        }

        return null;
    }

    private enum NativeAndroidFunction
    {
        GET_LONGITUDE,
        GET_LATITUDE,
        GET_ACCURACY,
        GET_ALTITUDE,
        GET_SPEED,
        GET_SPEED_ACCURACY_METERS_PER_SECOND,
        GET_VERTICAL_ACCURACY_METERS,
    }
    #endif
#endregion
}
