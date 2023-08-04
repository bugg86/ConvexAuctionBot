using ConvexAuctionBot.Handlers;
using Discord.Interactions;

namespace ConvexAuctionBot.Modules;

public class AuctionModule : InteractionModuleBase<SocketInteractionContext>
{
    public InteractionService Commands { get; set; } = null!;
    public CommandHandler _handler;

    public AuctionModule(CommandHandler handler)
    {
        _handler = handler;
    }

    public async Task StartAuction(string player)
    {
        
    }

    public async Task StopAuction()
    {
        
    }
}