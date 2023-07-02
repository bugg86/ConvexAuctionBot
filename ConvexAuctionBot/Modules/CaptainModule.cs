using ConvexAuctionBot.Handlers;
using Discord.Interactions;

namespace ConvexAuctionBot.Modules;

public class CaptainModule : InteractionModuleBase<SocketInteractionContext>
{
    public InteractionService Commands { get; set; } = null!;
    public CommandHandler _handler;

    public CaptainModule(CommandHandler handler)
    {
        _handler = handler;
    }
}