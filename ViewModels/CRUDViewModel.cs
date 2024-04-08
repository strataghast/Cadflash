using cadflash.Models;
using cadflash.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace cadflash.ViewModels
{
    public partial class CRUDViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<Flashcard> flashcards = new();

        [ObservableProperty]
        public Flashcard flashcard = new();

        private readonly LocalDbService localDbService;

        public CRUDViewModel()
        {
            Flashcard = new Flashcard();
            Flashcards = [];
            this.localDbService = new LocalDbService();
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("//difficulty");
        }
    }
}
