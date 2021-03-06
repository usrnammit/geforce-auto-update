﻿using System;
using Microsoft.Win32;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Security;

namespace GeforceAutoUpdate
{
	static partial class GameReadyDriver
	{
		public static readonly string LocalVersion;
		public static readonly string LatestVersion;
		public static readonly bool UpdateNeeded;
		//TODO: rewrite as properties

		static GameReadyDriver()
		{
			try
			{
				LocalVersion = RetrieveLocalVersion();
			}
			catch (SecurityException)
			{
				MessageBox.Show("Application doesn't have rights to access registry - cannot check local version.\nYou can try restarting as Administrator");
				Environment.Exit(1);
			}
			
			if (LocalVersion == null)
			{
				MessageBox.Show("Unable to check local version of nVidia driver. Are you sure it is installed?");
				Environment.Exit(1);
			}

			try
			{
				LatestVersion = RetrieveLatestVersion();
			}
			catch (Exception e) // mostly likely Chocolatey changed page layout or URL or there is now internet access
			{
				MessageBox.Show("Error: Unable to retrieve latest version from Chocolatey website.\n\n" + e.Message);
				Environment.Exit(1);
			}

			// unable to parse decimal point in some regions without InvariantCulture
			if (Double.Parse(LatestVersion, CultureInfo.InvariantCulture) > Double.Parse(LocalVersion, CultureInfo.InvariantCulture))
			{
				UpdateNeeded = true;
			}
			else
			{
				UpdateNeeded = false;
			}
		}

		// returns string representation of the version following 123.45 pattern if driver is installed
		private static string RetrieveLocalVersion()
		{
			RegistryKey localKey = null;
			if (Environment.Is64BitOperatingSystem)
			{
				localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
			}
			else
			{
				localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
			}

			localKey = localKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}_Display.Driver");
			string version = localKey.GetValue("DisplayVersion").ToString();
			return version;
		}
		
		// nVidia doesn't provide any reasonable way of checking current driver version beside GeForce Experience so we check latest version fo Chocolatey package.
		// Chocolatey package page is fairly small and version is in headline making regex search fast enough.
		// Note: The Chocolatey package page layout is supposed to get updated soon.
		private static string RetrieveLatestVersion()
		{
			WebClient client = new WebClient();
			string chocoPackagePage = client.DownloadString("https://chocolatey.org/packages/geforce-game-ready-driver-win10");

			string pattern = @"Geforce Game Ready Driver for Windows 10 \d\d\d.\d\d";
			Regex r = new Regex(pattern);
			Match m = r.Match(chocoPackagePage);

			string version = m.Value.Substring(41);
			return version;
		}

		// Drivers come in 4 versions total with download link following the same pattern.
		// The actual driver installer does it's own checks to verify propper version.
		// extractPath is used by GameReadyDriver.Update.Unpack() - the dafault path cannot be easily changed
		// however it follows similar pattern do the download url therefore they're generated together
		public static string GetDownloadLink()
		{
			if (Environment.Is64BitOperatingSystem)
			{
				RegistryKey localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
				localKey = localKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
				string windowsVersion = localKey.GetValue("ProductName") as String;

				if (windowsVersion.StartsWith("Windows 10"))
				{
					string downloadLink = "http://us.download.nvidia.com/Windows/" + LatestVersion + "/" + LatestVersion + "-desktop-win10-64bit-international-whql.exe";
					return downloadLink;
				}
				else
				{
					string downloadLink = "http://us.download.nvidia.com/Windows/" + LatestVersion + "/" + LatestVersion + "-desktop-win8-win7-64bit-international-whql.exe";
					return downloadLink;
				}
			}
			else
			{
				RegistryKey localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
				localKey = localKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
				string windowsVersion = localKey.GetValue("ProductName") as String;

				if (windowsVersion.StartsWith("Windows 10"))
				{
					string downloadLink = "http://us.download.nvidia.com/Windows/" + LocalVersion + "/" + LocalVersion + "-desktop-win10-32bit-international-whql.exe";
					return downloadLink;
				}
				else
				{
					string downloadLink = "http://us.download.nvidia.com/Windows/" + LocalVersion + "/" + LocalVersion + "-desktop-win8-win7-32bit-international-whql.exe";
					return downloadLink;
				}
			}
		}

		// Displayed in the DriverUpdatePromt GUI
		// TODO: add more information (changelog, link to official site, license,...)
		public static string GetUpdateDetails()
		{
			string updateDetails = "New version of GeForce Game Ready Drive is aviable.\n\n" +
									"Installed version: " + LocalVersion + "\n" +
									"Latest version: " + LatestVersion + "\n\n\n" +
									"Automatic: Downloads the update and performs silent install in the background.\nNot implemented yet\n\n" +
									"Manual: Opens direct link to the .exe in your default browser.\nPlease check that OS version and CPU architecture matches.\n\n\n";
			return updateDetails;
		}
	}
}