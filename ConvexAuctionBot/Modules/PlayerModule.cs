using ConvexAuctionBot.Handlers;
using ConvexAuctionBot.Services.Interfaces;
using Discord.Interactions;

namespace ConvexAuctionBot.Modules;

[Group("player", "commands for managing players")]
public class PlayerModule : InteractionModuleBase<SocketInteractionContext>
{
    public InteractionService Commands { get; set; } = null!;
    public CommandHandler _handler;
    private readonly IPlayerService _playerService;

    public PlayerModule(CommandHandler handler, IPlayerService playerService)
    {
        _handler = handler;
        _playerService = playerService;
    }

    [SlashCommand("add", "add player to database with default price of 0")]
    public async Task AddPlayer(string name, int price = 0)
    {
        //Maybe check for perms here eventually
        
        bool response = _playerService.AddPlayer(new KeyValuePair<string, int>(name, price));

        if (response)
        {
            await RespondAsync("Captain was successfully added.");
        }
        else
        {
            await RespondAsync("Captain could not be added.");
        }
    }

    [SlashCommand("delete", "remove player from database")]
    public async Task DeletePlayer(string name)
    {
        //Maybe check for perms here eventually
        
        bool response = _playerService.DeletePlayerByName(name);
        
        if (response)
        {
            await RespondAsync("Player was successfully removed.");
        }
        else
        {
            await RespondAsync("Player could not be removed.");
        }
    }
    
    [SlashCommand("update", "update player's price")]
    public async Task UpdateCaptain(string name, int price)
    {
        //Maybe check for perms here eventually
        
        bool response = _playerService.UpdatePlayer(new KeyValuePair<string, int>(name, price));
        
        if (response)
        {
            await RespondAsync("Player was successfully updated.");
        }
        else
        {
            await RespondAsync("Player could not be updated.");
        }
    }
}