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
            await RespondAsync("Player was successfully added.");
        }
        else
        {
            await RespondAsync("Player could not be added.");
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
    public async Task UpdatePlayer(string name, int price)
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

    [SlashCommand("reset", "reset player's prices to 0")]
    public async Task ResetPlayers()
    {
        bool response = _playerService.ResetSellPrices();
        
        if (response)
        {
            await RespondAsync("Player prices successfully set to 0.");
        }
        else
        {
            await RespondAsync("There was an issue resetting player prices.");
        }
    }

    [SlashCommand("remaining", "lists remaining players (players with price of 0)")]
    public async Task RemainingPlayers()
    {
        Dictionary<string, int>? players = _playerService.GetRemainingPlayers();

        if (players is null)
        {
            await RespondAsync("There are no remaining players.");
        }
        else if (players.Count.Equals(0))
        {
            await RespondAsync("There are no remaining players.");
        }
        else
        {
            string response = players.Aggregate("Remaining Players: \n", (current, player) => current + $"{player.Key} | {player.Value}\n");

            await RespondAsync(response);
        }
    }
}