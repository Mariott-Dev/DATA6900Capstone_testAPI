using CapAppMauiClient.DataServices;
using CapAppMauiClient.Models;
using System.Diagnostics;

namespace CapAppMauiClient.Pages;


[QueryProperty(nameof(CapApp), "CapApp")]   //decorator allows main to access 'this' code behind page DB object data
public partial class ManageCapAppPage : ContentPage
{
	private readonly IRestDataService _dataService;
	CapApp _capApp;
	bool _isNew;

	public CapApp CapApp
	{
		get => _capApp;
		set 
		{ 
			_isNew = IsNew(value);
			_capApp = value;
			OnPropertyChanged();	
		}
	}

	public ManageCapAppPage(IRestDataService dataService)
	{
		InitializeComponent();

		_dataService = dataService;
		BindingContext = this;
	}

	bool IsNew(CapApp capApp)
	{
		if(capApp.Id == 0)
			return true;
		return false;
	}

	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		if (_isNew)
		{
			Debug.WriteLine("---> Add new item");
			await _dataService.AddCapAppAsync(CapApp);
		}
		else
		{
			Debug.WriteLine("---> Update existing item");
			await _dataService.UpdateCapAppAsync(CapApp);
		}
        await Shell.Current.GoToAsync("..");
    }

	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		await _dataService.DeleteCapAppAsync(CapApp.Id);
        await Shell.Current.GoToAsync("..");
    }

	async void OnCancelButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");  //navigates back (cmd line)
	}

}