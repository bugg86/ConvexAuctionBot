using ConvexAuctionBot.Handlers;
using Discord.Interactions;

namespace ConvexAuctionBot.Modules;

public class PlayerModule : InteractionModuleBase<SocketInteractionContext>
{
    public InteractionService Commands { get; set; } = null!;
    public CommandHandler _handler;

    public PlayerModule(CommandHandler handler)
    {
        _handler = handler;
    }
}