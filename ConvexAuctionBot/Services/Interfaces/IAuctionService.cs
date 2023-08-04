namespace ConvexAuctionBot.Services.Interfaces;

public interface IAuctionService
{
    public string GetStatus();
    public bool SetStatus(string status);
    public void GenerateDbFile();
}