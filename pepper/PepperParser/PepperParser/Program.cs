using Microsoft.Extensions.DependencyInjection;
using PepperParser.Helpers;
using PepperParser.InitialSetups;
using PepperParser.Parsers;
using PepperParser.Services;

IServiceCollection services = new ServiceCollection();
services.AddSingleton<TextProcessing>();
services.AddSingleton<SafeParse>();
services.AddSingleton<Cache>();
services.AddSingleton<RestSharpSetup>();
services.AddTransient<SeleniumSetup>();
services.AddTransient<RestSharpParser>();
services.AddTransient<SeleniumParser>();
services.AddSingleton<TelegramService>();
services.AddTransient<PepperService>();

var serviceProvider = services.BuildServiceProvider();

var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(10));
while (await periodicTimer.WaitForNextTickAsync())
{
    var pepperService = serviceProvider.GetRequiredService<PepperService>();
    await pepperService.NotifyNewProduct();
}