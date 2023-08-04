using ConvexAuctionBot.Services.Interfaces;
using Newtonsoft.Json;

namespace ConvexAuctionBot.Services;

public class AuctionService : IAuctionService
{
    private readonly string auctionFile = "../../../DB/captains.json";
    
    public string? GetStatus()
    {
        try
        {
            Dictionary<string, string>? auctionData =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(auctionFile));

            if (auctionData?.TryGetValue("status", out string? temp) ?? false)
            {
                return temp;
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public bool SetStatus(string status)
    {
        try
        {
            Dictionary<string, string>? auctionData =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(auctionFile));

            if (auctionData is null)
            {
                Console.Write("status does not exist.");
                return false;
            }
            
            auctionData["status"] = status;
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    public void GenerateDbFile()
    {
        if (!File.Exists(auctionFile))
        {
            File.WriteAllText(auctionFile, "{\"status\": \"false\"}");
        }
        else
        {
            Console.WriteLine("auctionData.json already exists!");
        }
    }
}