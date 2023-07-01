namespace ConvexAuctionBot.Services.Interfaces;

public interface IPlayerService
{
    public Dictionary<string, int>? GetPlayers();
    public KeyValuePair<string, int>? GetSinglePlayer(string name);
    public KeyValuePair<string, int>? AddPlayer(KeyValuePair<string, int> player);
    public Dictionary<string, int>? AddPlayers(Dictionary<string, int> players);
    public void DeletePlayerByName(string name);
    public void DeletePlayerByObject(KeyValuePair<string, int> player);
    public KeyValuePair<string, int>? UpdatePlayer(KeyValuePair<string, int> player);
    public void GenerateDbFiles();
}