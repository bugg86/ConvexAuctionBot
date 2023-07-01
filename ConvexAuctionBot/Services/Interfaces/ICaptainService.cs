namespace ConvexAuctionBot.Services.Interfaces;

public interface ICaptainService
{
    public Dictionary<string, int>? GetCaptains();
    public KeyValuePair<string, int>? GetSingleCaptain(string name);
    public KeyValuePair<string, int>? AddCaptain(KeyValuePair<string, int> captain);
    public Dictionary<string, int>? AddCaptains(Dictionary<string, int> captains);
    public void DeleteCaptainByName(string name);
    public void DeleteCaptainByObject(KeyValuePair<string, int> captain);
    public KeyValuePair<string, int>? UpdateCaptain(KeyValuePair<string, int> captain);
    public void GenerateDbFiles();
}