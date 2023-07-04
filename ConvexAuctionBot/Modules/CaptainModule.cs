using ConvexAuctionBot.Handlers;
using ConvexAuctionBot.Services.Interfaces;
using Discord.Interactions;

namespace ConvexAuctionBot.Modules;

[Group("captain", "commands for managing captains")]
public class CaptainModule : InteractionModuleBase<SocketInteractionContext>
{
    public InteractionService Commands { get; set; } = null!;
    public CommandHandler _handler;
    private readonly ICaptainService _captainService;
    
    public CaptainModule(CommandHandler handler, ICaptainService captainService)
    {
        _handler = handler;
        _captainService = captainService;
    }

    [SlashCommand("add", "add captain to database with default balance of 500")]
    public async Task AddCaptain(string name, int balance = 500)
    {
        //Maybe check for perms here eventually
        
        bool response = _captainService.AddCaptain(new KeyValuePair<string, int>(name, balance));

        if (response)
        {
            await RespondAsync("Captain was successfully added.");
        }
        else
        {
            await RespondAsync("Captain could not be added.");
        }
    }
    
    [SlashCommand("delete", "remove captain from database")]
    public async Task DeleteCaptain(string name)
    {
        //Maybe check for perms here eventually
        
        bool response = _captainService.DeleteCaptainByName(name);
        
        if (response)
        {
            await RespondAsync("Captain was successfully removed.");
        }
        else
        {
            await RespondAsync("Captain could not be removed.");
        }
    }

    [SlashCommand("update", "update captain's balance")]
    public async Task UpdateCaptain(string name, int balance)
    {
        //Maybe check for perms here eventually
        
        bool response = _captainService.UpdateCaptain(new KeyValuePair<string, int>(name, balance));
        
        if (response)
        {
            await RespondAsync("Captain was successfully updated.");
        }
        else
        {
            await RespondAsync("Captain could not be updated.");
        }
    }

    [SlashCommand("balances", "get captain's current balances")]
    public async Task GetBalances()
    {
        Dictionary<string, int>? captains = _captainService.GetCaptains();

        if (captains is null)
        {
            await RespondAsync("There are no captains.");
        }
        else if (captains.Count.Equals(0))
        {
            await RespondAsync("There are no captains.");
        }
        else
        {
            string response = captains.Aggregate("Captain Balances: \n", (current, captain) => current + $"{captain.Key} | {captain.Value}\n");

            await RespondAsync(response);
        }
    }
}