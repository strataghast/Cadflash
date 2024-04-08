using cadflash.ViewModels;

namespace cadflash.Views;

public partial class DifficultyView : ContentPage
{
	public DifficultyView()
	{
		InitializeComponent();
		BindingContext = new DifficultyViewModel();
	}
}