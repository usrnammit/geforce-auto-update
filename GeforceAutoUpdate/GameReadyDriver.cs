using System;
using Microsoft.Win32;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace GeforceAutoUpdate
{
	static partial class GameReadyDriver
	{
		public static readonly string LocalVersion;
		public static readonly string LatestVersion;
		public static readonly bool IsInstalled;
		public static readonly bool UpdateNeeded;

		private static string extractPath;

		static GameReadyDriver()
		{
			LocalVersion = RetrieveLocalVersion();
			LatestVersion = RetrieveLatestVersion();
			extractPath = null;

			if (LocalVersion == null)
			{
				IsInstalled = false;
				UpdateNeeded = false;
			}
			else if (Double.Parse(LatestVersion, CultureInfo.InvariantCulture) > Double.Parse(LocalVersion, CultureInfo.InvariantCulture))
			{
				IsInstalled = true;
				UpdateNeeded = true;
			}
			else
			{
				IsInstalled = true;
				UpdateNeeded = false;
			}
		}

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

			if (localKey != null)
			{
				string version = localKey.GetValue("DisplayVersion").ToString();
				return version;
			}
			else
			{
				return null;
			}
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
		// however it follows similar pattern do the download url
		public static string GetDownloadLink()
		{
			if (Environment.Is64BitOperatingSystem)
			{
				RegistryKey localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
				localKey = localKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
				string windowsVersion = localKey.GetValue("ProductName") as String;

				if (windowsVersion.StartsWith("Windows 10"))
				{
					extractPath = "C:\\NVIDIA\\DisplayDriver\\" + LatestVersion + "\\Win10_64\\International\\";
					string downloadLink = "http://us.download.nvidia.com/Windows/" + LatestVersion + "/" + LatestVersion + "-desktop-win10-64bit-international-whql.exe";
					return downloadLink;
				}
				else
				{
					extractPath = "C:\\NVIDIA\\DisplayDriver\\" + LatestVersion + "\\Win8_Win7_64\\International\\";
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
					extractPath = "C:\\NVIDIA\\DisplayDriver\\" + LatestVersion + "\\Win10\\International\\";
					string downloadLink = "http://us.download.nvidia.com/Windows/" + LocalVersion + "/" + LocalVersion + "-desktop-win10-32bit-international-whql.exe";
					return downloadLink;
				}
				else
				{
					extractPath = "C:\\NVIDIA\\DisplayDriver\\" + LatestVersion + "\\Win8_Win7\\International\\";
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
									"Manual: Opens direct link to the .exe in your default browser.\nPlease check that OS version and CPU architecture matches.\n\n";
			return updateDetails;
		}
	}
}