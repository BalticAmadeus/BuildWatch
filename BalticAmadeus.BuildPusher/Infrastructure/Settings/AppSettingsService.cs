using System;
using System.Collections.Generic;
using BalticAmadeus.BuildPusher.Infrastructure.Http;
using BalticAmadeus.BuildServer.Interfaces.Settings;

namespace BalticAmadeus.BuildPusher.Infrastructure.Settings
{
	public class AppSettingsService : IAppSettingsService
	{
		private readonly ILocalSettingsService _localSettingsService;
		private readonly IHttpClientWrapper _httpClientWrapper;
		private readonly IDictionary<string, AppSettingItem> _settingsCache;
		private DateTime _lastRefreshTimestamp;
		
		public AppSettingsService(ILocalSettingsService localSettingsService, IHttpClientWrapper httpClientWrapper)
		{
			_localSettingsService = localSettingsService;
			_httpClientWrapper = httpClientWrapper;
			_settingsCache = new Dictionary<string, AppSettingItem>();
		}

		public void ReloadSettings()
		{
			if (string.IsNullOrWhiteSpace(_localSettingsService.AppKey))
				RegisterApp();

			LoadAndSaveSettings();
		}

		private void LoadAndSaveSettings()
		{
			string url = $"{_localSettingsService.ApiUrlBase}/appSettings/{_localSettingsService.AppKey}";

			var appSettingItems = _httpClientWrapper.Get<AppSettingItem[]>(url);

			foreach (var appSettingItem in appSettingItems)
				_settingsCache.Add(appSettingItem.Key, appSettingItem);

			_lastRefreshTimestamp = DateTime.Now;
		}

		private void RegisterApp()
		{
			string key = Guid.NewGuid().ToString();
			string url = $"{_localSettingsService.ApiUrlBase}/appSettings";
			
			var data = new RegisterAppSettingsData(key);
			_httpClientWrapper.Post(url, data);
		}

		private object Get(string key, string type)
		{
			if ((DateTime.Now - _lastRefreshTimestamp) > TimeSpan.FromMinutes(_localSettingsService.SettingsCacheTimeoutInMiliseconds))
				ReloadSettings();

			if (!_settingsCache.ContainsKey(key))
				return null;

			var setting = _settingsCache[key];

			if (setting.Type.Equals(type) && type.Equals("string"))
				return setting.Value;

			if (setting.Type.Equals(type) && type.Equals("int"))
				return int.Parse(setting.Value);

			return null;
		}

		public string GetString(string key)
		{
			var obj = Get(key, "string");
			if (obj == null)
				return default(string);

			return (string) obj;
		}

		public int GetInt(string key)
		{
			var obj = Get(key, "int");
			if (obj == null)
				return default(int);

			return (int) obj;
		}
	}
}