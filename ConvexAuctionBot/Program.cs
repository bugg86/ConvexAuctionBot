using ConvexAuctionBot.Handlers;
using ConvexAuctionBot.Services;
using ConvexAuctionBot.Services.Interfaces;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConvexAuctionBot;

public class Program
{
    private DiscordSocketClient? _client;
    private InteractionService _commands = null!;
    
    public static Task Main(string[] args) => new Program().MainAsync();

    private async Task MainAsync()
    {
        async void ConfigureDelegate(IServiceCollection services)
        {
            string? token;
            
            if (!File.Exists("../../../token"))
            {
                Console.Write("Please enter your discord bot token: ");
                token = Console.ReadLine();
                await File.WriteAllTextAsync("../../../token", token);
            }
            else
            {
                token = await File.ReadAllTextAsync("../../../token");
            }
            
            ConfigureServices(services);
            
            var serviceProvider = services.BuildServiceProvider();
            _client = serviceProvider.GetRequiredService<DiscordSocketClient>();
            _commands = serviceProvider.GetRequiredService<InteractionService>();

            _client.Log += Log;
            _commands.Log += Log;
            _client.Ready += ReadyAsync;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            
            await serviceProvider.GetRequiredService<CommandHandler>().InitializeAsync();
        }
        
        var host = new HostBuilder()
            .ConfigureServices(ConfigureDelegate);

        await host.RunConsoleAsync();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        DiscordSocketConfig config = new()
        {
            UseInteractionSnowflakeDate = false,
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent,
            AlwaysDownloadUsers = true
        };
        services.AddSingleton(config);
        services.AddSingleton<DiscordSocketClient>();
        services.AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()));
        services.AddSingleton<CommandHandler>();

        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<ICaptainService, CaptainService>();
        services.AddScoped<IAuctionService, AuctionService>();
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