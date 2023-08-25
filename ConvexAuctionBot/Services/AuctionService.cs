using ConvexAuctionBot.Services.Interfaces;
using Newtonsoft.Json;

namespace ConvexAuctionBot.Services;

public class AuctionService : IAuctionService
{
    private readonly string auctionFile = "../../../DB/auctionData.json";
    
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
                Console.Write("auctionData.json does not exist.");
                return false;
            }
            
            auctionData["status"] = status;
            File.WriteAllText(auctionFile, JsonConvert.SerializeObject(auctionData, Formatting.Indented));
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    public string GetCurrentPlayer()
    {
        try
        {
            Dictionary<string, string>? auctionData =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(auctionFile));

            if (auctionData?.TryGetValue("player", out string? temp) ?? false)
            {
                return temp;
            }

            return "";
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return "";
        }
    }

    public bool SetCurrentPlayer(string player)
    {
        try
        {
            Dictionary<string, string>? auctionData =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(auctionFile));

            if (auctionData is null)
            {
                Console.Write("auctionData.json does not exist.");
                return false;
            }
            
            auctionData["player"] = player;
            File.WriteAllText(auctionFile, JsonConvert.SerializeObject(auctionData, Formatting.Indented));
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    public string GetHighestBid()
    {
        try
        {
            Dictionary<string, string>? auctionData =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(auctionFile));

            if (auctionData?.TryGetValue("highestBid", out string? temp) ?? false)
            {
                return temp;
            }

            return "";
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return "";
        }
    }

    public bool SetHighestBid(string bid)
    {
        try
        {
            Dictionary<string, string>? auctionData =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(auctionFile));

            if (auctionData is null)
            {
                Console.Write("auctionData.json does not exist.");
                return false;
            }
            
            auctionData["highestBid"] = bid;
            File.WriteAllText(auctionFile, JsonConvert.SerializeObject(auctionData, Formatting.Indented));
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    public string GetHighestBidder()
    {
        try
        {
            Dictionary<string, string>? auctionData =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(auctionFile));

            if (auctionData?.TryGetValue("highestBidder", out string? temp) ?? false)
            {
                return temp;
            }

            return "";
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return "";
        }
    }

    public bool SetHighestBidder(string captain)
    {
        try
        {
            Dictionary<string, string>? auctionData =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(auctionFile));

            if (auctionData is null)
            {
                Console.Write("auctionData.json does not exist.");
                return false;
            }
            
            auctionData["highestBidder"] = captain;
            File.WriteAllText(auctionFile, JsonConvert.SerializeObject(auctionData, Formatting.Indented));
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    public int GetSeconds()
    {
        try
        {
            Dictionary<string, string>? auctionData =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(auctionFile));

            if (auctionData?.TryGetValue("seconds", out string? temp) ?? false)
            {
                return int.Parse(temp);
            }

            return -1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return -1;
        }
    }

    public bool SetSeconds(int seconds)
    {
        try
        {
            Dictionary<string, string>? auctionData =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(auctionFile));

            if (auctionData is null)
            {
                Console.Write("auctionData.json does not exist.");
                return false;
            }
            
            auctionData["seconds"] = seconds.ToString();
            File.WriteAllText(auctionFile, JsonConvert.SerializeObject(auctionData, Formatting.Indented));
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }
    
    public bool AddOneSecond()
    {
        try
        {
            Dictionary<string, string>? auctionData =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(auctionFile));

            if (auctionData is null)
            {
                Console.Write("auctionData.json does not exist.");
                return false;
            }
            
            auctionData["seconds"] = (int.Parse(auctionData["seconds"]) + 1).ToString();
            File.WriteAllText(auctionFile, JsonConvert.SerializeObject(auctionData, Formatting.Indented));
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