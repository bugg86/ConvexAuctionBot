using ConvexAuctionBot.Handlers;
using Discord.Interactions;

namespace ConvexAuctionBot.Modules;

[Group("captain", "commands for managing captains")]
public class CaptainModule : InteractionModuleBase<SocketInteractionContext>
{
    public InteractionService Commands { get; set; } = null!;
    public CommandHandler _handler;
    
    public CaptainModule(CommandHandler handler)
    {
        _handler = handler;
    }

    [SlashCommand("add", "add captain to database with default balance of 500")]
    public async Task AddCaptain(string name, int balance = 500)
    {
        
    }
    
    [SlashCommand("delete", "delete captain from database")]

    public async Task DeleteCaptain(string name)
    {
        
    }
}