using redfish_nas.BLL;

namespace redfish_nas;

public partial class MainPage : ContentPage
{
	int count = 0;
	private readonly HttpClient _httpClient;
	private readonly IUpdateService _updateService;

	public MainPage(HttpClient httpClient, IUpdateService updateService)
    {
        InitializeComponent();
		_httpClient = httpClient;
		_updateService = updateService;
    }

    private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);

		var test = new RedfishHandler();
		test.LogIn();

		_updateService.SslCert(_httpClient);
	}
}

