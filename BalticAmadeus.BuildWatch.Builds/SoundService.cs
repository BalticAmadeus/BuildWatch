﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;

namespace BalticAmadeus.BuildWatch.Builds
{
	public class SoundService
	{
		private readonly IDictionary<string, string> _soundsMap; 
		
		public SoundService(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
				throw new ArgumentNullException(nameof(path));

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			_soundsMap = Directory.GetFiles(path)
				.ToDictionary(Path.GetFileNameWithoutExtension, Path.GetFullPath);
		}

		public async Task PlaySound(string key)
		{
			await Task.Yield();

			if (!_soundsMap.ContainsKey(key))
				return;

			var soundPlayer = new SoundPlayer(_soundsMap[key]);
			soundPlayer.Play();
		}
	}
}
