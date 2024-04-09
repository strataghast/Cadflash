using cadflash.Models;
using cadflash.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace cadflash.ViewModels
{
    [QueryProperty(nameof(CurrentStreak), "CurrentStreak")]
    public partial class CRUDViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<Flashcard> flashcards = new();

        [ObservableProperty]
        public Flashcard flashcard = new();

        private readonly LocalDbService localDbService;

        private int _highestStreak;
        private int _currentStreak;
        public int CurrentStreak
        {
            get => _currentStreak;
            set => SetProperty(ref _currentStreak, value);
        }

        public int HighestStreak
        {
            get => _highestStreak;
            set => SetProperty(ref _highestStreak, value);
        }

        public CRUDViewModel()
        {
            Flashcard = new Flashcard();
            Flashcards = [];
            this.localDbService = new LocalDbService();
            LoadHighestStreak();
        }

        [RelayCommand]
        private async Task Back()
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("CurrentStreak", CurrentStreak);
            await Shell.Current.GoToAsync("//difficulty", parameter);
        }

        public async Task<int> GetHighestStreak()
        {
            using (var db = new HighScoreDbContext())
            {
                var highScore = await db.HighScoreStreaks.FirstOrDefaultAsync();
                return highScore != null ? highScore.HighestStreak : 0;
            }
        }

        private async Task LoadHighestStreak()
        {
            HighestStreak = await GetHighestStreak();
        }
    }
}
