using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using BalticAmadeus.BuildWatch.Builds.ClientService;
using BalticAmadeus.BuildWatch.Infrastructure;
using NLog;
using Prism.Mvvm;
using Prism.Regions;

namespace BalticAmadeus.BuildWatch.Builds
{
	public class BuildListViewModel : BindableBase, INavigationAware
	{
		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

		private readonly Func<IClientService> _clientServiceFactory;
		private IClientService _clientService;
		private readonly PhotoService _photoService;
		private readonly SoundService _soundService;
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

		public BuildListViewModel(Func<IClientService> clientServiceFactory, PhotoService photoService, SoundService soundService)
		{
			_clientServiceFactory = clientServiceFactory;
			_clientService = _clientServiceFactory();
			_photoService = photoService;
			_soundService = soundService;

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

				var pollBuildStatusResp = await _clientService.PollBuildStatusAsync(new PollBuildStatusReq
				{
					UpdateCounter = 0,
					ConfigurationId = 1
				});

				if (pollBuildStatusResp.FinishedBuilds == null)
					pollBuildStatusResp.FinishedBuilds = new FinishedBuild[0];
				
				Logger.Info("Server returned {0} finished builds", pollBuildStatusResp.FinishedBuilds.Count());

				var newBuilds = pollBuildStatusResp.FinishedBuilds.Select(x => new BuildListModel
				{
					Name = x.BuildName.ToUpper(),
					Instance = x.BuildInstance,
					Status = x.Result == "OK" ? BuildStatus.Success : BuildStatus.Fail,
					TimeStamp = x.TimeStamp.ToLocalTime(),
					User = x.UserName,
					PictureData = _photoService.GetPhoto(x.UserName)
				}).OrderByDescending(x => x.Status);

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

				_clientService = _clientServiceFactory();
				return Enumerable.Empty<BuildListModel>();
			}	
		}

		private async Task<IEnumerable<BuildListModel>> OnRefreshQueuedBuilds()
		{
			try
			{
				Logger.Info($"Pulling queued builds");

				var pollBuildStatusResp = await _clientService.PollBuildStatusAsync(new PollBuildStatusReq
				{
					UpdateCounter = 0,
					ConfigurationId = 1
				});

				if (pollBuildStatusResp.QueuedBuilds == null)
					pollBuildStatusResp.QueuedBuilds = new QueuedBuild[0];

				Logger.Info("Server returned {0} queued builds", pollBuildStatusResp.QueuedBuilds.Count());

				return pollBuildStatusResp.QueuedBuilds.Select(x => new BuildListModel
				{
					Name = x.BuildName.ToUpper(),
					Instance = x.BuildName,
					TimeStamp = x.QueueTime.ToLocalTime()
				});
			}
			catch (Exception e)
			{
				Logger.Error(e, "An error occured when pulling queued builds");

				_clientService = _clientServiceFactory();
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
