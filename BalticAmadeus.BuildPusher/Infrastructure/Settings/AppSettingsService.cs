using System;
using System.Collections.Generic;
using System.Net.Http;
using BalticAmadeus.BuildServer.Interfaces.Settings;
using Newtonsoft.Json;

namespace BalticAmadeus.BuildPusher.Infrastructure.Settings
{
	public class AppSettingsService : IAppSettingsService
	{
		private readonly ILocalSettingsService _localSettingsService;
		private readonly IDictionary<string, AppSettingItem> _settingsCache;
		private DateTime _lastRefreshTimestamp;
		
		public AppSettingsService(ILocalSettingsService localSettingsService)
		{
			_localSettingsService = localSettingsService;
			_settingsCache = new Dictionary<string, AppSettingItem>();
		}

		private void ReloadSettings()
		{
			if (string.IsNullOrWhiteSpace(_localSettingsService.AppKey))
				RegisterApp();

			LoadAndSaveSettings();
		}

		private void LoadAndSaveSettings()
		{
			using (var httpClient = new HttpClient())
			{
				var response = httpClient.GetAsync($"{_localSettingsService.ApiUrlBase}/appSettings/{_localSettingsService.AppKey}").Result;
				var appSettingItems = JsonConvert.DeserializeObject<AppSettingItem[]>(response.Content.ReadAsStringAsync().Result);

				if (!response.IsSuccessStatusCode)
					throw new NotImplementedException("Stop the application carefully");

				foreach (var appSettingItem in appSettingItems)
					_settingsCache.Add(appSettingItem.Key, appSettingItem);

				_lastRefreshTimestamp = DateTime.Now;
			}
		}

		private void RegisterApp()
		{
			string key = Guid.NewGuid().ToString();

			using (var httpClient = new HttpClient())
			{
				var data = new RegisterAppSettingsData(key).AsJsonStringContent();

				var response = httpClient.PostAsync($"{_localSettingsService.ApiUrlBase}/appSettings", data).Result;
				if (!response.IsSuccessStatusCode)
					throw new NotImplementedException("Stop the application carefully");
				
				_localSettingsService.AppKey = key;
			}
		}

		private object Get(string key, string type)
		{
			if ((DateTime.Now - _lastRefreshTimestamp) > TimeSpan.FromMinutes(_localSettingsService.SettingsCacheTimeoutInMiliseconds))
				ReloadSettings();

			if (!_settingsCache.ContainsKey(key))
				throw new NotImplementedException("Handle this");

			var setting = _settingsCache[key];

			if (setting.Type.Equals(type) && type.Equals("string"))
				return setting.Value;

			if (setting.Type.Equals(type) && type.Equals("int"))
				return int.Parse(setting.Value);

			return null;
		}

		public string GetString(string key)
		{
			return (string) Get(key, "string");
		}

		public int GetInt(string key)
		{
			return (int) Get(key, "int");
		}
	}
}