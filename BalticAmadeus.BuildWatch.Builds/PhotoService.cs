using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BalticAmadeus.BuildWatch.Builds
{
	public class PhotoService
	{
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
				return new byte[0];

			return _photosMap[key];
		} 
	}
}
