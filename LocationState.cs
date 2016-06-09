/* PlayStation(R)Mobile SDK 1.21.01
 * Copyright (C) 2013 Sony Computer Entertainment Inc.
 * All Rights Reserved.
 */
using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Device;
using Sce.PlayStation.Core.Imaging;

namespace EXTRA
{

/**
 * LocationStateSample
**/
public class userLocation
{
	private bool isStarted = false;
	private bool isEnable = false;

	private int loopCount = 0;

	private const string latitudeNoDataStr = "--.--------";
	private const string longitudeNoDataStr = "---.-------";
	private const string altitudeNoDataStr = "---- m";
	private const string speedNoDataStr = "--- km/h";
	private const string datetimeNoDataStr = "--- --, ---- --:--:--";
	private const string deviceNoDataStr = "---";
	private const string accuracyNoDataStr = "----- m";
	private const string bearingNoDataStr = "--- °";
	private string latitudeStr = latitudeNoDataStr;
	public string datetimeStr = datetimeNoDataStr;
	public string deviceStr = deviceNoDataStr;
	
	private const string utcString = "(UTC)";

	public userLocation ()
	{
		isStarted = Location.Start();
	}

	public void Update ()
	{
		if (Location.GetEnableDevices() == LocationDeviceType.None)
		{
			isEnable = false;
		}
		else
		{
			isEnable = true;
			if ((loopCount++ % 30) == 0)
			{
				GetLocationData();
			}
		}
	}

	

	public void GetLocationData()
	{
		var locationData = Location.GetData();

		// Latitude
		if (locationData.HasLatitude == true)
		{
			double latitude = locationData.Latitude;
			string latitudeDirectionStr = "N";

			if(latitude < 0)
			{
				latitude *= -1.0;
				latitudeDirectionStr = "S";
			}
			latitudeStr = latitude.ToString() + " " + latitudeDirectionStr;
		}else{
			latitudeStr = latitudeNoDataStr;
		}

		// Time
		if (locationData.HasTime == true)
		{
			long locationDataTimeUtc = locationData.Time;
			DateTime locationDataUpdateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			locationDataUpdateTime = locationDataUpdateTime.AddMilliseconds(locationDataTimeUtc);

			datetimeStr = locationDataUpdateTime.ToString("MMM. dd, yyyy") + " " + locationDataUpdateTime.ToString("HH:mm:ss") + " " + utcString;
		}else{
			datetimeStr = datetimeNoDataStr;
		}

		// enable devices
		deviceStr = locationData.DeviceType.ToString();
			
	}

	public void Terminate ()
	{
		Location.Stop();
	}
}

} // Sample
