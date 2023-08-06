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
            // _client.MessageReceived += HandleMessage(services);

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
    // private Task HandleMessage(SocketMessage arg, IServiceProvider _services)
    // {
    //     // there has to be a better way to do this but i can't be asked finding a better way
    //     if (arg.Channel.Id == auctionChannelId)
    //     {
    //         if (arg.Author.IsBot)
    //         {
    //             
    //         }
    //         
    //         string auctionStatus = _services.GetRequiredService<IAuctionService>().GetStatus() ?? "";
    //         
    //         if (!auctionStatus.Equals("true"))
    //         {
    //             return;
    //         }
    //
    //         if (!arg.Content.Contains("bid"))
    //         {
    //             return;
    //         }
    //         
    //         int bid = int.Parse(Regex.Match(arg.Content, @"\d+").Value);
    //         
    //         if (bid == 475)
    //         {
    //             return;
    //         }
    //         //25 is the bid increment
    //         if (bid % 25 != 0)
    //         {
    //             return;
    //         }
    //
    //         KeyValuePair<string, int> captain = _captainService.GetSingleCaptain(arg.Author.Username)!.Value;
    //         if (captain.Value - bid < 0)
    //         {
    //             return;
    //         }
    //             
    //         string currentPlayer = _auctionService.GetCurrentPlayer();
    //         int currentPrice = _playerService.GetSinglePlayer(currentPlayer).Value.Value;
    //
    //         if (bid <= currentPrice)
    //         {
    //             return;
    //         }
    //         
    //         _playerService.UpdatePlayer(new KeyValuePair<string, int>(currentPlayer, bid));
    //
    //         _captainService.UpdateCaptain(new KeyValuePair<string, int>(captain.Key, captain.Value - bid));
    //         
    //         await arg.Channel.SendMessageAsync("New highest bid is: **" + bid + "**");
    //     }
    // }
}