using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog;

namespace BalticAmadeus.BuildWatch.Builds
{
	public class PhotoService
	{
		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

		private readonly IDictionary<string, byte[]> _photosMap; 

		public PhotoService(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
				throw new ArgumentNullException(nameof(path));

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			_photosMap = Directory.GetFiles(path)
				.ToDictionary(Path.GetFileNameWithoutExtension, File.ReadAllBytes);
		}

		public byte[] GetPhoto(string key)
		{
			if (!_photosMap.ContainsKey(key))
			{
				Logger.Warn("No {0} photo found", key);
				
                return new byte[0];
			}

			return _photosMap[key];
		} 
	}
}
