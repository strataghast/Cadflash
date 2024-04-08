using cadflash.Models;
using cadflash.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace cadflash.ViewModels
{
    public partial class StudyAdvancedViewModel : ObservableObject
    {
        private LocalDbService localDbService;

        [ObservableProperty]
        public ObservableCollection<Flashcard> flashcards = new();

        [ObservableProperty]
        public Flashcard flashcard = new();

        private readonly Random random;

        public StudyAdvancedViewModel()
        {
            Flashcard = new Flashcard();
            Flashcards = new ObservableCollection<Flashcard>();
            this.localDbService = new LocalDbService();
            random = new Random();
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("//difficulty");
        }

        [RelayCommand]
        public async Task LoadRandomFlashcardByDifficulty()
        {
            Flashcards = new ObservableCollection<Flashcard>(await localDbService.GetFlashcardsAsync());
            var filteredFlashcards = Flashcards.Where(f => f.Difficulty == "Advanced").ToList();
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
    }
}
