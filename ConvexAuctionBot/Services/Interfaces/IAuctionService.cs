namespace ConvexAuctionBot.Services.Interfaces;

public interface IAuctionService
{
    public string? GetStatus();
    public bool SetStatus(string status);
    public string GetCurrentPlayer();
    public bool SetCurrentPlayer(string player);
    public string GetHighestBid();
    public bool SetHighestBid(string captain);
    public void GenerateDbFile();
}