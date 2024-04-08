using cadflash.Services;
using cadflash.ViewModels;

namespace cadflash.Views;

public partial class StudyBasicsView : ContentPage
{
    private readonly StudyBasicsViewModel viewModel = new();
    private readonly LocalDbService localDbService;
    public StudyBasicsView()
	{
		InitializeComponent();
        viewModel = new StudyBasicsViewModel();
        BindingContext = viewModel;
        this.localDbService = new LocalDbService();
        LoadFlashcardQuestion();
    }
    private async void LoadFlashcardQuestion()
    {
        await viewModel.LoadRandomFlashcardByDifficulty();
        flashcardLabel.Text = viewModel.Flashcard.Question;
    }

    private void nextButton_Clicked(object sender, EventArgs e)
    {
        LoadFlashcardQuestion();
    }

    private void backButton_Clicked(object sender, EventArgs e)
    {
        LoadFlashcardQuestion();
    }

    private void revealButton_Clicked(object sender, EventArgs e)
    {
        if (flashcardLabel.Text == viewModel.Flashcard.Question)
        {
            flashcardLabel.Text = viewModel.Flashcard.Answer;
        }
        else
        {
            flashcardLabel.Text = viewModel.Flashcard.Question;
        }
    }
}