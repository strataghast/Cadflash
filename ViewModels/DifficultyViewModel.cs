using cadflash.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace cadflash.ViewModels
{
    [QueryProperty(nameof(CurrentStreak), "CurrentStreak")]
    public partial class DifficultyViewModel : ObservableObject
    {
        private int _currentStreak;
        public int CurrentStreak
        {
            get => _currentStreak;
            set => SetProperty(ref _currentStreak, value);
        }

        [RelayCommand]
        private async Task Modify()
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("CurrentStreak", CurrentStreak);
            await Shell.Current.GoToAsync("//crud", parameter);
        }

        [RelayCommand]
        private async Task StudyEasy()
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("CurrentStreak", CurrentStreak);
            await Shell.Current.GoToAsync("//studybasics", parameter);
        }

        [RelayCommand]
        private async Task StudyIntermediate()
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("CurrentStreak", CurrentStreak);
            await Shell.Current.GoToAsync("//studyintermediate", parameter);
        }

        [RelayCommand]
        private async Task StudyAdvanced()
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("CurrentStreak", CurrentStreak);
            await Shell.Current.GoToAsync("//studyadvanced", parameter);
        }
    }
}
