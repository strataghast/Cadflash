using cadflash.Models;
using cadflash.Services;
using cadflash.ViewModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace cadflash.Views;

public partial class CRUDView : ContentPage
{
	CRUDViewModel viewModel;
	private readonly LocalDbService localDbService; // This is the service that is used to interact with the local database
	private int _editFlashcardId; // This is the ID of the flashcard that is being edited
	public CRUDView(LocalDbService localDbService)
	{
		InitializeComponent();
		viewModel = new CRUDViewModel();
		this.BindingContext = viewModel;
		this.localDbService = localDbService;
		Task.Run(async () => await localDbService.GetFlashcardsAsync());
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		listView.ItemsSource = await localDbService.GetFlashcardsAsync();
	}

    private async void saveFlashcardButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(entryQuestion.Text) || string.IsNullOrEmpty(entryAnswer.Text) || string.IsNullOrEmpty(entryDifficulty.Text))
        {
            CreateToast("Please fill in all fields!");
            return;
        }

		if(entryDifficulty.Text != "Basic" && entryDifficulty.Text != "Intermediate" && entryDifficulty.Text != "Advanced")
		{
			CreateToast("Difficulty must be 'Basic', 'Intermediate', or 'Advanced'");
			return;
		}

        if (_editFlashcardId == 0)
		{
			// Create a new flashcard
			await localDbService.Create(new Flashcard
			{
                Question = entryQuestion.Text,
                Answer = entryAnswer.Text,
				Difficulty = entryDifficulty.Text
            });
			CreateToast("Flashcard created successfully!");
		}
		else
		{
			await localDbService.Update(new Flashcard
			{
                Id = _editFlashcardId,
                Question = entryQuestion.Text,
                Answer = entryAnswer.Text,
                Difficulty = entryDifficulty.Text
            });
			CreateToast("Flashcard updated successfully!");
		}

		entryQuestion.Text = string.Empty;
		entryAnswer.Text = string.Empty;
		entryDifficulty.Text = string.Empty;

		listView.ItemsSource = await localDbService.GetFlashcardsAsync();
    }

    private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		var flashcard = (Flashcard)e.Item;
		var action = await DisplayActionSheet("Options", "Cancel", null, "Edit", "Delete");

		switch(action)
		{
			case "Edit":
                _editFlashcardId = flashcard.Id;
                entryQuestion.Text = flashcard.Question;
                entryAnswer.Text = flashcard.Answer;
                entryDifficulty.Text = flashcard.Difficulty;
                break;

			case "Delete":
				await localDbService.Delete(flashcard);
                listView.ItemsSource = await localDbService.GetFlashcardsAsync();
				CreateToast("Flashcard deleted successfully!");
                break;
		}
    }

    private void backButton_Clicked(object sender, EventArgs e)
    {
		viewModel.BackCommand.Execute(null);
    }

	private async void CreateToast(string text)
	{
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        ToastDuration duration = ToastDuration.Short;
		double fontSize = 40;

        var toast = Toast.Make(text, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }
}