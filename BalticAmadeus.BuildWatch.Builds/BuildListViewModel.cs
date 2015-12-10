using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Threading;
using BalticAmadeus.BuildServer.Interfaces.Builds;
using BalticAmadeus.BuildWatch.Infrastructure;
using BalticAmadeus.BuildWatch.Infrastructure.Settings;
using NLog;
using Prism.Mvvm;
using Prism.Regions;

namespace BalticAmadeus.BuildWatch.Builds
{
	public class BuildListViewModel : BindableBase, INavigationAware
	{
		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
		
		private readonly PhotoService _photoService;
		private readonly SoundService _soundService;
		private readonly ILocalSettingsService _localSettingsService;
		private readonly DispatcherTimer _dispatcherTimer;
		private bool _isLastBuildFailed; 

		private BuildListModel _selectedBuild;
		public BuildListModel SelectedBuild
		{
			get { return _selectedBuild; }
			set { SetProperty(ref _selectedBuild, value); }
		}

		public AsyncCollection<BuildListModel> FinishedBuilds { get; set; }
		public AsyncCollection<BuildListModel> QueuedBuilds { get; set; }

		public BuildListViewModel(PhotoService photoService, SoundService soundService, ILocalSettingsService localSettingsService)
		{
			_photoService = photoService;
			_soundService = soundService;
			_localSettingsService = localSettingsService;

			QueuedBuilds = new AsyncCollection<BuildListModel>(OnRefreshQueuedBuilds);
			FinishedBuilds = new AsyncCollection<BuildListModel>(OnRefreshFinishedBuilds);
			FinishedBuilds.IsExecutingChanged += (sender, args) =>
			{
				SelectedBuild = FinishedBuilds.FirstOrDefault();
			};
			
			_dispatcherTimer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(10)};
			_dispatcherTimer.Tick += (sender, args) =>
			{
				QueuedBuilds.Refresh();
				FinishedBuilds.Refresh();
			};
        }

		private async Task<IEnumerable<BuildListModel>> OnRefreshFinishedBuilds()
		{
			try
			{
				Logger.Info("Pulling finished builds");

				FinishedBuildItem[] finishedBuildItems;

				using (var httpClient = new HttpClient())
				{
					var response = await httpClient.GetAsync($"{_localSettingsService.ApiUrlBase}/finishedBuilds");
					finishedBuildItems = response.Content.As<FinishedBuildItem[]>();
				}

				Logger.Info("Server returned {0} finished builds", finishedBuildItems.Count());

				var newBuilds = finishedBuildItems.Select(x => new BuildListModel
				{
					Name = x.Title.ToUpper(),
					Status = x.Status == 0 ? BuildStatus.Success : BuildStatus.Fail,
					TimeStamp = x.TimeStamp.ToLocalTime(),
					User = x.Username,
					PictureData = _photoService.GetPhoto(x.Username)
				}).OrderByDescending(x => x.Status).ThenByDescending(x => x.TimeStamp);

				bool isBuildFailed = newBuilds.Any(x => x.Status == BuildStatus.Fail);
				if (!_isLastBuildFailed && isBuildFailed)
					await _soundService.PlaySound("BuildFailed");
				else if (_isLastBuildFailed && !isBuildFailed)
					await _soundService.PlaySound("BuildSucceeded");

				_isLastBuildFailed = isBuildFailed;

				return newBuilds;
			}
			catch(Exception e)
			{
				Logger.Error(e, "An error occured when pulling finished builds");
				
				return Enumerable.Empty<BuildListModel>();
			}	
		}

		private async Task<IEnumerable<BuildListModel>> OnRefreshQueuedBuilds()
		{
			try
			{
				Logger.Info($"Pulling queued builds");

				QueuedBuildItem[] queuedBuildItems;

				using (var httpClient = new HttpClient())
				{
					var response = await httpClient.GetAsync($"{_localSettingsService.ApiUrlBase}/queuedBuilds");
					queuedBuildItems = response.Content.As<QueuedBuildItem[]>();
				}
				
				Logger.Info($"Server returned {queuedBuildItems.Count()} queued builds");
				
				return queuedBuildItems.Select(x => new BuildListModel
				{
					Name = x.Title.ToUpper(),
					TimeStamp = x.TimeStamp.ToLocalTime(),
					User = x.Username
				}).OrderByDescending(x => x.TimeStamp);
			}
			catch (Exception e)
			{
				Logger.Error(e, "An error occured when pulling queued builds");
				
				return Enumerable.Empty<BuildListModel>();
			}
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			FinishedBuilds.Refresh();
			QueuedBuilds.Refresh();

			_isLastBuildFailed = FinishedBuilds.Any(x => x.Status == BuildStatus.Fail);

			_dispatcherTimer.Start();
		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			_dispatcherTimer.Stop();
		}
	}
}
