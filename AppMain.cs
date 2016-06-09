/*	Virtual Terminal - Main Operations
 * This software voids most OOP and Encapsulation, however is a working product complicated 
 * of cracking because it is sub-methods to be called from the launch. 
 * 
 * 	App info:
 * Release: Jun 4, 2014 01:41 am UTC
 * Publisher: Self, Nathan Pillman aka "GTAWWEKID"
 * Platform: PlayStation®Mobile SDK 1.21.01
 * Runtime: 1.21
 * 
 * Included Libraries owned by respective owners:
 * -Microsoft CORPORATION
 * -Sony Entertainment America Inc.
 * 
 * */
using System;
using System.IO;

namespace AppData3088261ATSI
{
	public class AppMain
	{
		const string SAVE_PASS = "/Documents/sudopass.dat";
		public static void Main (string[] args)
		{	
			
			// TO Whom it concerns:
			// This is a simulated terminal to fool player's friend and pretend to hack somebody 
			// This does not really harm anyone but the player's RAM usage.
			// All the code is in the original Terminal.Composer.cs file if you decrypt it!
			// Please allow this! My OOP is horrible. I know!
			bool pass = System.IO.File.Exists(@SAVE_PASS);
			OS app = new OS(pass);
		}
	}
}

