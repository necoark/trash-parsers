using Microsoft.Extensions.DependencyInjection;
using ProbnickRuParser.Helpers;
using ProbnickRuParser.Parsers;
using ProbnickRuParser.Services;

IServiceCollection services = new ServiceCollection();
services.AddSingleton<Cache>();
services.AddSingleton<TextProcessing>();
services.AddSingleton<TelegramService>();
services.AddSingleton<AngleSharpParser>();
services.AddSingleton<ProbnickRuService>();
var serviceProvider = services.BuildServiceProvider();

var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(60*30));
while (await periodicTimer.WaitForNextTickAsync())
{
    var probnickRuService = serviceProvider.GetRequiredService<ProbnickRuService>();
    await probnickRuService.NotifyNewOffer();
}