using redfish_nas.BLL;
using System.Net.Security;

namespace redfish_nas;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
            
		builder.Services.AddSingleton<IUpdateService>(new UpdateService());
		builder.Services.AddSingleton<HttpClient>(x =>
        {
            var baseUri = new Uri("https://192.168.1.10/redfish/v1/");

            using var handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromSeconds(120)
            };
            handler.SslOptions.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
            {
                if (sslPolicyErrors == SslPolicyErrors.None)
                    return true;

                if (certificate.GetCertHashString().ToLower() == "5598bab7f83ff5a9e2f9bb4e518d8848f2ceeefc")
                    return true;

                return false;
            };

            var client = new HttpClient(handler)
            {
                BaseAddress = baseUri, 
            };

            return client;
        });

		return builder.Build();
	}
}
