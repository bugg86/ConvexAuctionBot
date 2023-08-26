using System.Reflection;
using System.Text.RegularExpressions;
using ConvexAuctionBot.Services.Interfaces;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.DependencyInjection;

namespace ConvexAuctionBot.Handlers;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _commands;
    private readonly IServiceProvider _services;
    private readonly ulong auctionChannelId;
    private readonly ICaptainService _captainService;
    private readonly IAuctionService _auctionService;
    private readonly IPlayerService _playerService;

    public CommandHandler(DiscordSocketClient client, InteractionService commands, IServiceProvider services)
    {
        _client = client;
        _commands = commands;
        _services = services;
        auctionChannelId = 1124063514001154098;
        //I'm pretty sure this is not how I'm supposed to do this but i dont care
        _auctionService = _services.GetRequiredService<IAuctionService>();
        _captainService = _services.GetRequiredService<ICaptainService>();
        _playerService = _services.GetRequiredService<IPlayerService>();
    }

    public async Task InitializeAsync()
    {
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        _client.InteractionCreated += HandleInteraction;
        _client.MessageReceived += HandleMessage;
        
        _commands.SlashCommandExecuted += SlashCommandExecuted;
        _commands.ContextCommandExecuted += ContextCommandExecuted;
        _commands.ComponentCommandExecuted += ComponentCommandExecuted;
    }

    private async Task HandleMessage(SocketMessage arg)
    {
        // there has to be a better way to do this but i can't be asked finding a better way
        if (arg.Channel.Id != auctionChannelId)
        {
            return;
        }

        if (arg.Author.IsBot)
        {
            return;
        }
        
        Console.WriteLine("message is being processed");
        
        string auctionStatus = _services.GetRequiredService<IAuctionService>().GetStatus() ?? "";
            
        if (!auctionStatus.Equals("true"))
        {
            return;
        }

        if (!arg.Content.Contains("bid"))
        {
            return;
        }
            
        int bid = int.Parse(Regex.Match(arg.Content, @"\d+").Value);
            
        if (bid == 475)
        {
            return;
        }
        //25 is the bid increment
        if (bid % 25 != 0)
        {
            return;
        }

        KeyValuePair<string, int> captain = _captainService.GetSingleCaptain(arg.Author.Username)!.Value;
        if (captain.Value - bid < 0)
        {
            return;
        }
                
        string currentPlayer = _auctionService.GetCurrentPlayer();
        int? currentPrice = _playerService.GetSinglePlayer(currentPlayer)?.Value ?? 0;

        if (bid <= currentPrice)
        {
            return;
        }
        
        _auctionService.SetHighestBid(bid.ToString());
        _auctionService.SetHighestBidder(captain.Key);
        _auctionService.SetSeconds(0);

        await arg.Channel.SendMessageAsync(embed: new EmbedBuilder()
        {
            Title = "New highest bid!",
            Description = $"New highest bid is **{captain.Key}**: $**{bid}**",
            Color = Color.Orange
        }.Build());
        // await arg.Channel.SendMessageAsync($"New highest bid is **{captain.Key}**: $**{bid}**");
    }
    
    private async Task HandleInteraction(SocketInteraction arg)
    {
        try
        {
            var context = new SocketInteractionContext(_client, arg);
            await _commands.ExecuteCommandAsync(context, _services);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            
            if (arg.Type == InteractionType.ApplicationCommand)
            {
                await arg.GetOriginalResponseAsync().ContinueWith(async (msg) => await
                    msg.Result.DeleteAsync());
            }
        }
    }
    
    private Task ComponentCommandExecuted(ComponentCommandInfo _info, IInteractionContext _context, IResult _result)
    {
        if (!_result.IsSuccess)
        {
            switch (_result.Error)
            {
                case InteractionCommandError.UnmetPrecondition:
                    break;
                case InteractionCommandError.UnknownCommand:
                    break;
                case InteractionCommandError.BadArgs:
                    break;
                case InteractionCommandError.Exception:
                    break;
                case InteractionCommandError.Unsuccessful:
                    break;
                default:
                    break;
            }
        }

        return Task.CompletedTask;
    }
    
    private Task ContextCommandExecuted(ContextCommandInfo _info, IInteractionContext _context, IResult _result)
    {
        if (!_result.IsSuccess)
        {
            switch (_result.Error)
            {
                case InteractionCommandError.UnmetPrecondition:
                    break;
                case InteractionCommandError.UnknownCommand:
                    break;
                case InteractionCommandError.BadArgs:
                    break;
                case InteractionCommandError.Exception:
                    break;
                case InteractionCommandError.Unsuccessful:
                    break;
                default:
                    break;
            }
        }

        return Task.CompletedTask;
    }
    
    private Task SlashCommandExecuted(SlashCommandInfo arg1, IInteractionContext arg2, IResult arg3)
    {
        if (!arg3.IsSuccess)
        {
            switch (arg3.Error)
            {
                case InteractionCommandError.UnmetPrecondition:
                    break;
                case InteractionCommandError.UnknownCommand:
                    break;
                case InteractionCommandError.BadArgs:
                    break;
                case InteractionCommandError.Exception:
                    break;
                case InteractionCommandError.Unsuccessful:
                    break;
                default:
                    break;
            }
        }

        return Task.CompletedTask;
    }
}