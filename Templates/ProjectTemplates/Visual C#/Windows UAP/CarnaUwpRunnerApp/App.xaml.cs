using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

using Carna.UwpRunner;

namespace $safeprojectname$
{
    sealed partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Window.Current.Activate();
            CarnaUwpRunner.Run();
        }
    }
}
