using System.Windows;

namespace BuildWatch
{
	public partial class App
	{
		private readonly Bootstrapper _bootstrapper;

		public App()
		{
			_bootstrapper = new Bootstrapper();
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			_bootstrapper.Run();
		}
	}
}
