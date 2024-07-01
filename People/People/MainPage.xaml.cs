namespace People;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

    private void OnNewButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = string.Empty;

        App.PersonRepository.AddNewPerson(newPerson.Text);
        statusMessage.Text = App.PersonRepository.StatusMessage;
    }

    private void OnGetButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = string.Empty;

        var people = App.PersonRepository.GetAllPeople();
        peopleList.ItemsSource = people;
    }

}

