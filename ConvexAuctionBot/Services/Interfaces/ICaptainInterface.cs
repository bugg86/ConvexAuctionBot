namespace ConvexAuctionBot.Services.Interfaces;

public interface ICaptainInterface
{
    public Dictionary<string, decimal>? GetCaptains();
    public KeyValuePair<string, decimal>? GetSingleCaptain(string name);
    public KeyValuePair<string, decimal>? AddCaptain(KeyValuePair<string, decimal> captain);
    public Dictionary<string, decimal>? AddCaptains(Dictionary<string, decimal> captains);
    public void DeleteCaptainByName(string name);
    public void DeleteCaptainByObject(KeyValuePair<string, decimal> captain);
    public KeyValuePair<string, decimal>? UpdateCaptain(KeyValuePair<string, decimal> captain);
}