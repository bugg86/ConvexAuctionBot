using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace ConvexAuctionBot.Handlers;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _commands;
    private readonly IServiceProvider _services;

    public CommandHandler(DiscordSocketClient client, InteractionService commands, IServiceProvider services)
    {
        _client = client;
        _commands = commands;
        _services = services;
    }

    public async Task InitializeAsync()
    {
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        _client.InteractionCreated += HandleInteraction;

        _commands.SlashCommandExecuted += SlashCommandExecuted;
        _commands.ContextCommandExecuted += ContextCommandExecuted;
        _commands.ComponentCommandExecuted += ComponentCommandExecuted;
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