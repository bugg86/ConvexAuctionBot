using ConvexAuctionBot.Models;
using ConvexAuctionBot.Services.Interfaces;

namespace ConvexAuctionBot.Services;

public class CaptainService : ICaptainInterface
{
    public string DbDirectory = "../../../DB";
    
    public List<Captain> GetCaptains()
    {
        throw new NotImplementedException();
    }
    
    public Captain GetSingleCaptain(string name)
    {
        throw new NotImplementedException();
    }
    
    public Captain? AddCaptain(Captain captain)
    {
        throw new NotImplementedException();
    }
    
    public List<Captain>? AddCaptains(List<Captain> captains)
    {
        throw new NotImplementedException();
    }
    
    public void DeleteCaptainByName(string name)
    {
        throw new NotImplementedException();
    }
    
    public void DeleteCaptainByObject(Captain captain)
    {
        throw new NotImplementedException();
    }
    
    public Captain? UpdateCaptain(Captain captain)
    {
        throw new NotImplementedException();
    }
}