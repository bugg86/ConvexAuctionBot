namespace ConvexAuctionBot.Services.Interfaces;

public interface IAuctionService
{
    public string? GetStatus();
    public bool SetStatus(string status);
    public string GetCurrentPlayer();
    public bool SetCurrentPlayer(string player);
    public void GenerateDbFile();
}