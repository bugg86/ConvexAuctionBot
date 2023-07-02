﻿using ConvexAuctionBot.Handlers;
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
}