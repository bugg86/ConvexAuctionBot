namespace ConvexAuctionBot.Services.Interfaces;

public interface IPlayerService
{
    public Dictionary<string, int>? GetPlayers();
    public KeyValuePair<string, int>? GetSinglePlayer(string name);
    public Dictionary<string, int>? GetRemainingPlayers();
    public bool AddPlayer(KeyValuePair<string, int> player);
    public bool AddPlayers(Dictionary<string, int> players);
    public bool DeletePlayerByName(string name);
    public bool DeletePlayerByObject(KeyValuePair<string, int> player);
    public bool UpdatePlayer(KeyValuePair<string, int> player);
    public bool UpdatePlayer(string player, int price);
    public bool ResetSellPrices();
    public void GenerateDbFile();
}