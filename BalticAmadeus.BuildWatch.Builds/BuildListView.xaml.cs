namespace BalticAmadeus.BuildWatch.Builds
{
	public partial class BuildListView
	{
		public BuildListView(BuildListViewModel viewModel)
		{
			InitializeComponent();

			DataContext = viewModel;
		}
	}
}
