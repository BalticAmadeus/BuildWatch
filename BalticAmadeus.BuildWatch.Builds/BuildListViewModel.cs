using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using BalticAmadeus.BuildWatch.Builds.ClientService;
using Prism.Mvvm;
using Prism.Regions;

namespace BalticAmadeus.BuildWatch.Builds
{
	public class BuildListViewModel : BindableBase, INavigationAware
	{
		private readonly IClientService _clientService;
		private readonly DispatcherTimer _dispatcherTimer;

		private BuildListModel _selectedBuild;

		public BuildListModel SelectedBuild
		{
			get { return _selectedBuild; }
			set { SetProperty(ref _selectedBuild, value); }
		}

		public ObservableCollection<BuildListModel> FinishedBuilds { get; set; }
		public ObservableCollection<BuildListModel> QueuedBuilds { get; set; }

		public BuildListViewModel(IClientService clientService)
		{
			_clientService = clientService;

			FinishedBuilds = new ObservableCollection<BuildListModel>();
			QueuedBuilds = new ObservableCollection<BuildListModel>();

			_dispatcherTimer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(5)};
			_dispatcherTimer.Tick += (sender, args) => ProcessBuilds();
		}

		private async void ProcessBuilds()
		{
			FinishedBuilds.Clear();
			QueuedBuilds.Clear();

			var pollBuildStatusResp = await _clientService.PollBuildStatusAsync(new PollBuildStatusReq
			{
				UpdateCounter = 0,
				ConfigurationId = 1
			});

			if (pollBuildStatusResp.PictureMaps == null)
				pollBuildStatusResp.PictureMaps = new PictureMap[0];

			var pictureMap = pollBuildStatusResp.PictureMaps.ToDictionary(
				x => x.UserName,
				x => GetPictureDataFromHash(x.PictureHash));

			if (pollBuildStatusResp.FinishedBuilds == null)
				pollBuildStatusResp.FinishedBuilds = new FinishedBuild[0];

			FinishedBuilds.AddRange(pollBuildStatusResp.FinishedBuilds.Select(x => new BuildListModel
			{
				Name = x.BuildName,
				Instance = x.BuildInstance,
				Result = x.Result,
				TimeStamp = x.TimeStamp.ToLocalTime(),
				User = x.UserName,
				PictureData = pictureMap[x.UserName]
			}));

			if (pollBuildStatusResp.QueuedBuilds == null)
				pollBuildStatusResp.QueuedBuilds = new QueuedBuild[0];

			QueuedBuilds.AddRange(pollBuildStatusResp.QueuedBuilds.Select(x => new BuildListModel
			{
				Name = x.BuildName,
				Instance = x.BuildName,
				TimeStamp = x.QueueTime.ToLocalTime()
			}));

			SelectedBuild = FinishedBuilds.FirstOrDefault();
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
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

		private static byte[] GetPictureDataFromHash(string hash)
		{
			return Convert.FromBase64String(hash);
		}
	}
}
