using CapAppMauiClient.DataServices;
using CapAppMauiClient.Models;
using CapAppMauiClient.Pages;
using System.Diagnostics;

namespace CapAppMauiClient;

public partial class MainPage : ContentPage
{
	private readonly IRestDataService _dataService;

	public MainPage(IRestDataService dataService)
	{
		InitializeComponent();

		_dataService = dataService;
	}

	protected async override void OnAppearing()  //uses data service interface to perform/display all DB content
	{
		base.OnAppearing();

		collectionView.ItemsSource = await _dataService.GetAllCapAppsAsync();
	}

	async void OnAddCapAppClicked(object sender, EventArgs e)  //onclick redirects to content creation page
	{
		Debug.WriteLine("---> Add button clicked");

		var navigationParameter = new Dictionary<string, object>
		{
			{ nameof(CapApp), new CapApp() }
		};

		await Shell.Current.GoToAsync(nameof(ManageCapAppPage), navigationParameter);
	}

	async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)  //onclick signal content was updated and redirects to main
	{
		Debug.WriteLine("---> Item changed clicked!");

        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(CapApp), e.CurrentSelection.FirstOrDefault() as CapApp }
        };

        await Shell.Current.GoToAsync(nameof(ManageCapAppPage), navigationParameter);
    }

}

