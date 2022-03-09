package com.marcinkulwicki.localetools;

import android.content.Context;
import android.location.LocationManager;
import android.location.LocationListener;

public class Location implements LocationListener
{
    public static android.location.Location LastLocation;

    private LocationManager locationManager;

    public void init(Context context)
    {
        locationManager = (LocationManager) context.getSystemService(Context.LOCATION_SERVICE);
        locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 1000, 0, this);
        locationManager.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, 1000, 0, this);
        locationManager.requestLocationUpdates(LocationManager.PASSIVE_PROVIDER, 1000, 0, this);
    }

    @Override
    public void onLocationChanged(android.location.Location location)
    {
        LastLocation = location;
    }
}
