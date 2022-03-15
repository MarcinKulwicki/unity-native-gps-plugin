#import "LocaleTools.h"

double latitude, longitude, altitude;
float accuracy, verticalAccuracy, speed, speedAccuracy;

@implementation LocaleTools

CLLocationManager *locationManager;

- (LocaleTools *)init
{
    locationManager = [[CLLocationManager alloc] init];
    locationManager.delegate = self;
    locationManager.distanceFilter = kCLDistanceFilterNone;
    locationManager.desiredAccuracy = kCLLocationAccuracyBest;

    if([[[UIDevice currentDevice] systemVersion] floatValue] >= 8.0)
        [locationManager requestWhenInUseAuthorization];

    [locationManager startUpdatingLocation];

    return self;
}

- (void)locationManager:(CLLocationManager *)manager didUpdateLocations:(NSArray *)locations;
{
    CLLocation *location = [locations lastObject];
    
    latitude = location.coordinate.latitude;
    longitude = location.coordinate.longitude;
    accuracy = location.horizontalAccuracy;
    
    altitude = location.altitude;
    verticalAccuracy = location.verticalAccuracy;
    
    speed = location.speed;
    speedAccuracy = location.speedAccuracy;
}

@end

static LocaleTools* localeDelegate = NULL;

extern "C"
{
    void startLocation()
    {
        if(localeDelegate == NULL) localeDelegate = [[LocaleTools alloc] init];
    }

    double getLongitude()
    {
        return longitude;
    }

    double getLatitude()
    {
        return latitude;
    }

    double getAltitude()
    {
        return altitude;
    }

    float getAccuracy()
    {
        return accuracy;
    }

    float getVerticalAccuracyMeters()
    {
        return verticalAccuracy;
    }

    float getSpeed()
    {
        return speed;
    }

    float getSpeedAccuracy()
    {
        return speedAccuracy;
    }
}
