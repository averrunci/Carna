using Carna.WinUIRunner;
using Microsoft.UI.Xaml;

namespace $safeprojectname$;

public partial class App
{
    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        CarnaWinUIRunner.Run();
    }
}