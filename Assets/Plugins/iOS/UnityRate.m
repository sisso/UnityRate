void UnityRate_sendToRate(unsigned char* id)
{
	NSString *idStr = [NSString stringWithUTF8String:id];
	
	// http://stackoverflow.com/questions/18905686/itunes-review-url-and-ios-7-ask-user-to-rate-our-app-appstore-show-a-blank-pag
	NSString* const iOSAppStoreURLFormat = @"itms-apps://itunes.apple.com/app/id%@";
	NSString* const iOS7AppStoreURLFormat = @"itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=%@"

	BOOL isIos7 = [[UIDevice currentDevice].systemVersion floatValue] >= 7.0f;	
	NSString* urlFormat = isIos7 ? iOS7AppStoreURLFormat : iOSAppStoreURLFormat;
	NSString* iTunesLink = [NSString stringWithFormat: urlFormat, idStr]; 
	NSLog(@"UnityRate_sendToRate open url '%@'", iTunesLink);
	[[UIApplication sharedApplication] openURL:[NSURL URLWithString:iTunesLink]];
}
