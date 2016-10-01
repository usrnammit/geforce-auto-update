using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeforceAutoUpdate
{
	static class GameReadyDriver
	{
		private static float localVersion;
		private static float latestVersion;
		public static readonly bool UpdateNeeded; // TODO: rewrite as property

		static GameReadyDriver()
		{
			localVersion = GetLocalVersion();
			latestVersion = GetLatestVersion();

			if (latestVersion > localVersion)
			{
				UpdateNeeded = true;
			}
			else
			{
				UpdateNeeded = false;
			}
		}

		private static float GetLocalVersion()
		{
			// TODO: logic for retrieving local driver version
			float version = 0.0f;
			return version;
		}
		private static float GetLatestVersion()
		{
			// TODO: logic for retrieving latest version
			float version = 0.0f;
			return version;
		}
	}
}
