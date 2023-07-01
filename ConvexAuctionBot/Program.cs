using ConvexAuctionBot.Services;
using ConvexAuctionBot.Services.Interfaces;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    private DiscordSocketClient? _client;
    private InteractionService _commands = null!;
    
    public static Task Main(string[] args) => new Program().MainAsync();

    private async Task MainAsync()
    {
        async void ConfigureDelegate(IServiceCollection services)
        {
            ConfigureServices(services);
            
            var serviceProvider = services.BuildServiceProvider();
            _client = serviceProvider.GetRequiredService<DiscordSocketClient>();
            _commands = serviceProvider.GetRequiredService<InteractionService>();

            _client.Log += Log;
            _commands.Log += Log;
            _client.Ready += ReadyAsync;

            var token = await File.ReadAllTextAsync("../../../token");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // await serviceProvider.GetRequiredService<SelectionMenuHandler>().InitializeAsync();
            // await serviceProvider.GetRequiredService<CommandHandler>().InitializeAsync();
        }
        
        var host = new HostBuilder()
            .ConfigureServices(ConfigureDelegate);

        await host.RunConsoleAsync();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        DiscordSocketConfig config = new()
        {
            UseInteractionSnowflakeDate = false
        };
        services.AddSingleton(config);
        services.AddSingleton<DiscordSocketClient>();
        services.AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()));
        // services.AddSingleton<CommandHandler>();
        // services.AddSingleton<SelectionMenuHandler>();
        
        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<ICaptainService, CaptainService>();
    }
    
    private async Task ReadyAsync()
    {
        await _commands.RegisterCommandsGloballyAsync(true);
    }
    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}