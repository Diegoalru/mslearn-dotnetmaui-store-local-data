namespace People;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnNewButtonClicked(object sender, EventArgs args)
    {
        if (StatusMessage.Text != "Adding new person...")
        {
            StatusMessage.Text = "Adding new person...";
        }

        try
        {
            await App.PersonRepository.AddNewPersonAsync(NewPerson.Text).ConfigureAwait(true);
            StatusMessage.Text = App.PersonRepository.StatusMessage;
            NewPerson.Text = string.Empty; // Clear the input field after adding.
        }
        catch (Exception ex)
        {
            StatusMessage.Text = $"Failed to add new person. Error: {ex.Message}";
        }
    }

    private async void OnGetButtonClicked(object sender, EventArgs args)
    {
        if (StatusMessage.Text != "Loading people...")
        {
            StatusMessage.Text = "Loading people...";
        }
        PeopleList.ItemsSource = null; // Clear the list before loading.

        try
        {
            var people = await App.PersonRepository.GetAllPeopleAsync().ConfigureAwait(true);
            if (people.Count == 0)
            {
                StatusMessage.Text = "No people found.";
            }
            else
            {
                StatusMessage.Text = string.Empty; // Clear status message on successful load.
                PeopleList.ItemsSource = people;
            }
        }
        catch (Exception ex)
        {
            StatusMessage.Text = $"Failed to load people. Error: {ex.Message}";
        }
    }
}