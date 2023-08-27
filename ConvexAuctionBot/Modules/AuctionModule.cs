using System.Timers;
using System.Xml.Schema;
using ConvexAuctionBot.Handlers;
using ConvexAuctionBot.Services.Interfaces;
using Discord;
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
    private int timerTracker = 0;

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
        if (Context.User.Id != 119581243342979074 && Context.User.Id != 318813777619320832 && Context.User.Id != 402263124809351178 && Context.User.Id != 336928732277243905)
        {
            await RespondAsync("you can't use that command stop it!");
            return;
        }
        _auctionService.SetStatus("true");

        _auctionService.SetCurrentPlayer(player);

        await RespondAsync(embed: new EmbedBuilder()
        {
            Title = "Starting auction for: **" + player + "**",
            Color = Color.Green
        }.Build());

        _timer = new Timer(1000);
        _timer.AutoReset = true;
        _timer.Elapsed += OnTimedEvent;
        _timer.Enabled = true;
    }

    [SlashCommand("stop", "manually stop the auction")]
    public async Task StopAuction()
    {
        await RespondAsync(embed: new EmbedBuilder()
        {
            Title = "Auction has been stopped.",
            Color = Color.Red
        }.Build());
        ResetAuction();
    }

    private void OnTimedEvent(object? source, ElapsedEventArgs e)
    {
        int secondsPassed = _auctionService.GetSeconds();
        if (secondsPassed != timerTracker)
        {
            timerTracker = 0;
            Console.WriteLine("timer was reset, fixing timerTracker");
        }
        else
        {
            Console.WriteLine("adding one second to timer");
            timerTracker++;
            _auctionService.AddOneSecond();
        }
        Console.WriteLine(timerTracker - 1 + " | " + secondsPassed);

        if (timerTracker - 1 == 10 && secondsPassed == 10)
        {
            Console.WriteLine("timer is done");
            int highestBid = int.Parse(_auctionService.GetHighestBid());
            string highestBidder = _auctionService.GetHighestBidder();
            string player = _auctionService.GetCurrentPlayer();
            int captainBalance = _captainService.GetSingleCaptain(highestBidder)!.Value.Value;

            _playerService.UpdatePlayer(new KeyValuePair<string, int>(player, highestBid));
            _captainService.UpdateCaptain(new KeyValuePair<string, int>(highestBidder, captainBalance - highestBid));

            Context.Channel.SendMessageAsync(embed: new EmbedBuilder()
            {
                Title = $"**{player}** was bought for $**{highestBid}** by **{highestBidder}**!",
                Color = Color.Green
            }.Build());
            // Context.Channel.SendMessageAsync($"**{player}** was bought for $**{highestBid}** by **{highestBidder}**!");
            ResetAuction();
        }
    }

    private void ResetAuction()
    {
        _auctionService.SetStatus("false");
        _auctionService.SetCurrentPlayer("");
        _auctionService.SetHighestBid("0");
        _auctionService.SetSeconds(0);
        _auctionService.SetHighestBidder("");
        _timer.Enabled = false;
    }
}