using ConvexAuctionBot.Services.Interfaces;

namespace ConvexAuctionBot.Services;

public class CaptainService : ICaptainInterface
{
    public string DbDirectory = "../../../DB";
    
    public Dictionary<string, decimal>? GetCaptains()
    {
        throw new NotImplementedException();
    }
    
    public KeyValuePair<string, decimal>? GetSingleCaptain(string name)
    {
        throw new NotImplementedException();
    }
    
    public KeyValuePair<string, decimal>? AddCaptain(KeyValuePair<string, decimal> captain)
    {
        throw new NotImplementedException();
    }
    
    public Dictionary<string, decimal>? AddCaptains(Dictionary<string, decimal> captains)
    {
        throw new NotImplementedException();
    }
    
    public void DeleteCaptainByName(string name)
    {
        throw new NotImplementedException();
    }
    
    public void DeleteCaptainByObject(KeyValuePair<string, decimal> captain)
    {
        throw new NotImplementedException();
    }
    
    public KeyValuePair<string, decimal>? UpdateCaptain(KeyValuePair<string, decimal> captain)
    {
        throw new NotImplementedException();
    }
}