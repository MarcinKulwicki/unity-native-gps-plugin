#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>
#import <UIKit/UIKit.h>

@interface LocaleTools : NSObject <CLLocationManagerDelegate>

- (LocaleTools *)init;
- (void)locationManager:(CLLocationManager *)manager didUpdateLocations:(NSArray *)locations;

@end
