using CapAppMauiClient.Pages;

namespace CapAppMauiClient;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ManageCapAppPage), typeof(ManageCapAppPage));
	}
}
