using cadflash.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace cadflash.ViewModels
{
    public partial class DifficultyViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task Modify()
        {
            await Shell.Current.GoToAsync("//crud");
        }

        [RelayCommand]
        private async Task StudyEasy()
        {
            await Shell.Current.GoToAsync("//studybasics");
        }

        [RelayCommand]
        private async Task StudyIntermediate()
        {
            await Shell.Current.GoToAsync("//studyintermediate");
        }

        [RelayCommand]
        private async Task StudyAdvanced()
        {
            await Shell.Current.GoToAsync("//studyadvanced");
        }
    }
}
