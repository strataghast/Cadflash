using cadflash.Models;
using cadflash.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace cadflash.ViewModels
{
    [QueryProperty(nameof(CurrentStreak), "CurrentStreak")]
    public partial class StudyBasicsViewModel : ObservableObject
    {

        private LocalDbService localDbService;

        [ObservableProperty]
        public ObservableCollection<Flashcard> flashcards = new();

        [ObservableProperty]
        public Flashcard flashcard = new();

        private readonly Random random;

        private int _highestStreak;
        private int _currentStreak;

        HighScoreDbContext? highScoreDbContext;
        public StudyBasicsViewModel()
        {
            Flashcard = new Flashcard();
            Flashcards = new ObservableCollection<Flashcard>();
            this.localDbService = new LocalDbService();
            random = new Random();
            highScoreDbContext = new HighScoreDbContext();
            highScoreDbContext.Database.EnsureCreated();
            LoadHighestStreak();
            SeedDummyData().Wait();
        }

        [RelayCommand]
        private async Task Back()
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("CurrentStreak", CurrentStreak);
            await Shell.Current.GoToAsync("//difficulty", parameter);
        }

        [RelayCommand]
        public async Task LoadRandomFlashcardByDifficulty()
        {
            Flashcards = new ObservableCollection<Flashcard>(await localDbService.GetFlashcardsAsync());
            var filteredFlashcards = Flashcards.Where(f => f.Difficulty == "Basic").ToList();
            if (filteredFlashcards.Any())
            {
                int randomIndex = random.Next(0, filteredFlashcards.Count);
                var randomEasyFlashcard = filteredFlashcards[randomIndex];
                Flashcard = randomEasyFlashcard;
            }
            else
            {
                return;
            }

        }


        // CURRENT AND HIGHSCORE STREAK FUNCTIONALITY 

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

        public async Task<int> GetHighestStreak()
        {
            using (var db = new HighScoreDbContext())
            {
                var highScore = await db.HighScoreStreaks.FirstOrDefaultAsync();
                return highScore != null ? highScore.HighestStreak : 0;
            }
        }
        public async Task UpdateStreak(int newStreak)
        {
            CurrentStreak = newStreak;

            if (newStreak > HighestStreak)
            {
                HighestStreak = newStreak;
                await UpdateHighestStreak(newStreak);
            }
        }

        private async Task LoadHighestStreak()
        {
            HighestStreak = await GetHighestStreak();
        }

        private async Task UpdateHighestStreak(int newStreak)
        {
            using (var db = new HighScoreDbContext())
            {
                if(db != null)
                {
                    var highScore = await db.HighScoreStreaks.FirstOrDefaultAsync();
                    if (highScore == null)
                    {
                        highScore = new HighScoreStreak { HighestStreak = newStreak };
                        db.HighScoreStreaks.Add(highScore);
                    }
                    else if (newStreak > highScore.HighestStreak)
                    {
                        highScore.HighestStreak = newStreak;
                    }
                    await db.SaveChangesAsync();
                    HighestStreak = highScore.HighestStreak;
                }
            }
        }
        public async Task SeedDummyData()
        {
            using (var db = new HighScoreDbContext())
            {
                if (db != null)
                {
                    // Check if any data exists in the HighScoreStreaks table
                    if (!await db.HighScoreStreaks.AnyAsync())
                    {
                        // If no data exists, seed the database with dummy data
                        var dummyData = new HighScoreStreak { HighestStreak = 0 };
                        db.HighScoreStreaks.Add(dummyData);
                        await db.SaveChangesAsync();
                    }
                }
            }
        }

        [RelayCommand]
        public async Task IncrementStreak()
        {
            CurrentStreak++;
        }
        [RelayCommand]
        public async Task StopStreak()
        {
            // Update the high score if the current streak surpasses it
            await UpdateStreak(CurrentStreak);
            CurrentStreak = 0; // Reset current streak
        }
    }
}
