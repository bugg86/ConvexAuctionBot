using ConvexAuctionBot.Services.Interfaces;
using Newtonsoft.Json;

namespace ConvexAuctionBot.Services;

public class CaptainService
{
    private string captainFile = "../../../DB/captains.json";
    
    public Dictionary<string, int>? GetCaptains()
    {
        try
        {
            Dictionary<string, int>? captains =
                JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(captainFile));

            return captains;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public KeyValuePair<string, int>? GetSingleCaptain(string name)
    {
        Dictionary<string, int>? captains = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(captainFile));
        
        if (captains is null)
        {
            Console.WriteLine("captains.json does not exist");
            return null;
        }

        try
        {
            return new KeyValuePair<string, int>(name, captains[name]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public KeyValuePair<string, int>? AddCaptain(KeyValuePair<string, int> captain)
    {
        Dictionary<string, int>? captains = GetCaptains();

        if (captains is null)
        {
            Console.WriteLine("captains.json does not exist");
            return null;
        }

        try
        {
            captains.Add(captain.Key, captain.Value);
            
            if (captains.TryGetValue(captain.Key, out int temp))
            {
                File.WriteAllText(captainFile, JsonConvert.SerializeObject(captains, Formatting.Indented));

                Console.WriteLine(captain.Key + " successfully added");
                return captain;
            }
            else
            {
                Console.WriteLine(captain.Key + " failed to be added");
                return null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public Dictionary<string, int>? AddCaptains(Dictionary<string, int> captainsToAdd)
    {
        Dictionary<string, int>? captains = GetCaptains();

        if (captains is null)
        {
            Console.WriteLine("captains.json does not exist");
            return null;
        }

        try
        {
            foreach (KeyValuePair<string, int> captain in captainsToAdd)
            {
                captains.Add(captain.Key, captain.Value);
            }
            
            //Check if captains added here eventually
            
            File.WriteAllText(captainFile, JsonConvert.SerializeObject(captains, Formatting.Indented));
            
            return captainsToAdd; 
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public void DeleteCaptainByName(string name)
    {
        Dictionary<string, int>? captains = GetCaptains();

        if (captains is null)
        {
            Console.WriteLine("captains.json does not exist");
            return;
        }

        try
        {
            captains.Remove(name);
            
            if (!captains.TryGetValue(name, out int temp))
            {
                File.WriteAllText(captainFile, JsonConvert.SerializeObject(captains, Formatting.Indented));
                Console.WriteLine(name + " successfully removed");
            }
            else
            {
                Console.WriteLine(name + " failed to be removed");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    public void DeleteCaptainByObject(KeyValuePair<string, int> captain)    
    {
        Dictionary<string, int>? captains = GetCaptains();

        if (captains is null)
        {
            Console.WriteLine("captains.json does not exist");
            return;
        }

        try
        {
            captains.Remove(captain.Key);
            
            if (!captains.TryGetValue(captain.Key, out int temp))
            {
                File.WriteAllText(captainFile, JsonConvert.SerializeObject(captains, Formatting.Indented));
                Console.WriteLine(captain.Key + " successfully removed");
            }
            else
            {
                Console.WriteLine(captain.Key + " failed to be removed");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    public KeyValuePair<string, int>? UpdateCaptain(KeyValuePair<string, int> oldCaptain)
    {
        Dictionary<string, int>? captains = GetCaptains();
        
        if (captains is null)
        {
            Console.WriteLine("captains.json does not exist");
            return null;
        }

        try
        {
            int oldBalance = captains[oldCaptain.Key];
            captains[oldCaptain.Key] = oldCaptain.Value;
            
            if (!oldBalance.Equals(captains[oldCaptain.Key]))
            {
                File.WriteAllText(captainFile, JsonConvert.SerializeObject(captains, Formatting.Indented));
                
                Console.WriteLine(oldCaptain.Key + " successfully updated");
                return new KeyValuePair<string, int>(oldCaptain.Key, captains[oldCaptain.Key]);
            }
            else
            {
                Console.WriteLine(oldCaptain.Key + " failed to be updated");
                return null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public void GenerateDbFiles()
    {
        if (!File.Exists(captainFile))
        {
            File.WriteAllText(captainFile, "{}");
        }
        else
        {
            Console.WriteLine("captains.json already exists!");
        }
    }
}