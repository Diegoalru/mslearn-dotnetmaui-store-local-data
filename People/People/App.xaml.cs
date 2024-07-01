namespace People;

public partial class App : Application
{
    public static PersonRepository PersonRepository { get; private set; }

    public App(PersonRepository personRepository)
	{
		InitializeComponent();

		MainPage = new AppShell();

		PersonRepository = personRepository;
	}
}
