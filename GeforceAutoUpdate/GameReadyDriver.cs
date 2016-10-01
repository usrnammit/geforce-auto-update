using System;
using Microsoft.Win32;
using System.Globalization;

namespace GeforceAutoUpdate
{
	static class GameReadyDriver
	{
		public static double localVersion;
		private static double latestVersion;
		public static readonly bool UpdateNeeded; // TODO: rewrite as property

		static GameReadyDriver()
		{
			localVersion = RetrieveLocalVersion();
			latestVersion = RetrieveLatestVersion();

			if (latestVersion > localVersion)
			{
				UpdateNeeded = true;
			}
			else
			{
				UpdateNeeded = false;
			}
		}

		private static double RetrieveLocalVersion()
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
			string s = localKey.GetValue("DisplayVersion").ToString();

			double version = Double.Parse(s, CultureInfo.InvariantCulture);
			return version;
		}

		private static double RetrieveLatestVersion()
		{
			// TODO: logic for retrieving latest version
			double version = 0.0f;
			return version;
		}
	}
}
