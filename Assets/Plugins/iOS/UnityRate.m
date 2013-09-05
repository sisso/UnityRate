void UnityRate_sendToRate(unsigned char* id)
{
	NSString *idStr = [NSString stringWithUTF8String:id];
	NSLog(@"UnityRate_sendToRate id '%@'", idStr);
	
	// http://gamesfromwithin.com/increase-your-app-ratings-on-the-app-store
	// http://creativealgorithms.com/blog/content/review-app-links-sorted-out
	// http://bjango.com/articles/ituneslinks/
	// NSString* url = [NSString stringWithFormat:  @"http://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?id=%@&pageNumber=0&sortOrdering=1&type=Purple+Software&mt=8", appid];

	NSString *templateUrl = @"itms-apps://ax.itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=%@";
	NSString* iTunesLink = [NSString stringWithFormat: templateUrl, idStr];

	[[UIApplication sharedApplication] openURL:[NSURL URLWithString:iTunesLink]];
}
