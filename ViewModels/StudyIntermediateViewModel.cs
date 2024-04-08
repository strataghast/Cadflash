using cadflash.Models;
using cadflash.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cadflash.ViewModels
{
    

    public partial class StudyIntermediateViewModel : ObservableObject
    {
        private LocalDbService localDbService;

        [ObservableProperty]
        public ObservableCollection<Flashcard> flashcards = new();

        [ObservableProperty]
        public Flashcard flashcard = new();

        private readonly Random random;

        public StudyIntermediateViewModel()
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
            var filteredFlashcards = Flashcards.Where(f => f.Difficulty == "Intermediate").ToList();
            if (filteredFlashcards.Any())
            {
                int randomIndex = random.Next(0, filteredFlashcards.Count);
                var randomIntermediateFlashcard = filteredFlashcards[randomIndex];
                Flashcard = randomIntermediateFlashcard;
            }
            else
            {
                return;
            }
        }
    }
}
