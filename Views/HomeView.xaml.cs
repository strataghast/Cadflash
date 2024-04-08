using cadflash.ViewModels;

namespace cadflash.Views;

public partial class HomeView : ContentPage
{
	public HomeView()
	{
		InitializeComponent();
		BindingContext = new HomeViewModel();
	}
}