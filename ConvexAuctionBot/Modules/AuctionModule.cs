using System.Timers;
using ConvexAuctionBot.Handlers;
using ConvexAuctionBot.Services.Interfaces;
using Discord.Interactions;
using Timer = System.Timers.Timer;

namespace ConvexAuctionBot.Modules;

[Group("auction", "commands for managing the auction")]
public class AuctionModule : InteractionModuleBase<SocketInteractionContext>
{
    public InteractionService Commands { get; set; } = null!;
    public CommandHandler _handler;
    private readonly IAuctionService _auctionService;
    private readonly ICaptainService _captainService;
    private readonly IPlayerService _playerService;
    private Timer _timer;

    public AuctionModule(CommandHandler handler, IAuctionService auctionService, ICaptainService captainService, IPlayerService playerService)
    {
        _handler = handler;
        _auctionService = auctionService;
        _captainService = captainService;
        _playerService = playerService;
    }

    [SlashCommand("start", "start the auction for a player")]
    public async Task StartAuction(string player)
    {
        _auctionService.SetStatus("true");

        _auctionService.SetCurrentPlayer(player);

        await RespondAsync("Starting auction for: **" + player + "**");

        _timer = new Timer(10000);
        _timer.AutoReset = false;
        _timer.Elapsed += OnTimedEvent;
        _timer.Enabled = true;
    }

    [SlashCommand("stop", "manually stop the auction")]
    public async Task StopAuction()
    {
        ResetAuction();

        await RespondAsync("Auction has been stopped.");
    }

    private void OnTimedEvent(object? source, ElapsedEventArgs e)
    {
        Console.WriteLine("timer is done");
        string highestBid = _auctionService.GetHighestBid();
        string highestBidder = _auctionService.GetHighestBidder();
        string player = _auctionService.GetCurrentPlayer();

        _playerService.UpdatePlayer(new KeyValuePair<string, int>(player, int.Parse(highestBid)));
        _captainService.UpdateCaptain(new KeyValuePair<string, int>());
        
        Context.Channel.SendMessageAsync($"**{player}** was bought for $**{highestBid}** by **{highestBidder}**!");

        ResetAuction();
    }

    private void ResetAuction()
    {
        _auctionService.SetStatus("false");
        _auctionService.SetCurrentPlayer("");
        _auctionService.SetHighestBid("");
        _auctionService.SetHighestBidder("");
    }
}