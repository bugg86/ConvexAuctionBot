using ConvexAuctionBot.Handlers;
using ConvexAuctionBot.Services.Interfaces;
using Discord.Interactions;

namespace ConvexAuctionBot.Modules;

[Group("auction", "commands for managing the auction")]
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

    [SlashCommand("start", "start the auction for a player")]
    public async Task StartAuction(string player)
    {
        _auctionService.SetStatus("true");

        _auctionService.SetCurrentPlayer(player);

        await RespondAsync("Starting auction for: **" + player + "**");
    }

    [SlashCommand("stop", "manually stop the auction")]
    public async Task StopAuction()
    {
        _auctionService.SetStatus("false");
        _auctionService.SetCurrentPlayer("");

        await RespondAsync("Auction has been stopped.");
    }
}