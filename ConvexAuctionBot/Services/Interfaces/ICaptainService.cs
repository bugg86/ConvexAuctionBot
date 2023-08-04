namespace ConvexAuctionBot.Services.Interfaces;

public interface ICaptainService
{
    public Dictionary<string, int>? GetCaptains();
    public KeyValuePair<string, int>? GetSingleCaptain(string name);
    public bool AddCaptain(KeyValuePair<string, int> captain);
    public bool AddCaptains(Dictionary<string, int> captains);
    public bool DeleteCaptainByName(string name);
    public bool DeleteCaptainByObject(KeyValuePair<string, int> captain);
    public bool UpdateCaptain(KeyValuePair<string, int> captain);
    public bool ResetBalances();
    public void GenerateDbFile();
}