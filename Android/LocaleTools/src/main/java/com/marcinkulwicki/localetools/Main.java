package com.marcinkulwicki.localetools;

import android.app.Activity;
import android.util.Log;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class Main extends UnityPlayerActivity
{
    public static void startLocation()
    {
        Log.w("Native Toolkit", "Start Location");

        Location location = new Location();
        location.init(getUnityActivity());
    }

    public static double getLatitude()
    {
        if (Location.LastLocation == null)
            return 0;

        return Location.LastLocation.getLatitude();
    }

    public static double getLongitude()
    {
        if (Location.LastLocation == null)
            return 0;

        return Location.LastLocation.getLongitude();
    }

    public static float getAccuracy()
    {
        if (Location.LastLocation == null)
            return 0;

        if (Location.LastLocation.hasAccuracy())
            return Location.LastLocation.getAccuracy();
        return -1;
    }

    public static double getAltitude()
    {
        if (Location.LastLocation == null)
            return 0;

        if (Location.LastLocation.hasAltitude())
            return Location.LastLocation.getAltitude();
        return -1;
    }

    public static float getSpeed()
    {
        if (Location.LastLocation == null)
            return 0;

        if (Location.LastLocation.hasSpeed())
            return Location.LastLocation.getSpeed();
        return -1;
    }

    public static float getSpeedAccuracyMetersPerSecond()
    {
        if (Location.LastLocation == null)
            return 0;

        if (Location.LastLocation.hasSpeedAccuracy())
            return Location.LastLocation.getSpeedAccuracyMetersPerSecond();
        return -1;
    }

    public static float getVerticalAccuracyMeters()
    {
        if (Location.LastLocation == null)
            return 0;

        if (Location.LastLocation.hasVerticalAccuracy())
            return Location.LastLocation.getVerticalAccuracyMeters();
        return -1;
    }

    public static Activity getUnityActivity()
    {
        return UnityPlayer.currentActivity;
    }
}