using ConvexAuctionBot.Handlers;
using ConvexAuctionBot.Services.Interfaces;
using Discord.Interactions;

namespace ConvexAuctionBot.Modules;

public class AuctionModule : InteractionModuleBase<SocketInteractionContext>
{
    public InteractionService Commands { get; set; } = null!;
    public CommandHandler _handler;
    private readonly IAuctionService _auctionService;

    public AuctionModule(CommandHandler handler, IAuctionService auctionService)
    {
        _handler = handler;
        _auctionService = auctionService;
    }

    public async Task StartAuction(string player)
    {
        _auctionService.SetStatus("true");
    }

    public async Task StopAuction()
    {
        
    }
}