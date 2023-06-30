using ConvexAuctionBot.Models;

namespace ConvexAuctionBot.Services.Interfaces;

public interface ICaptainInterface
{
    public List<Captain> GetCaptains();
    public Captain GetSingleCaptain(string name);
    public Captain? AddCaptain(Captain captain);
    public List<Captain>? AddCaptains(List<Captain> captains);
    public void DeleteCaptainByName(string name);
    public void DeleteCaptainByObject(Captain captain);
    public Captain? UpdateCaptain(Captain captain);
}