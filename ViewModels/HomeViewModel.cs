using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace cadflash.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task Continue()
        {
            await Shell.Current.GoToAsync("//difficulty");
        }
    }
}
