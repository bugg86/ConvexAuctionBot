using ConvexAuctionBot.Services.Interfaces;
using Newtonsoft.Json;

namespace ConvexAuctionBot.Services;

public class PlayerService : IPlayerService
{
    private string playerFile = "../../../DB/players.json";
    
    public Dictionary<string, int>? GetPlayers()
    {
        try
        {
            Dictionary<string, int>? players =
                JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(playerFile));

            return players;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public KeyValuePair<string, int>? GetSinglePlayer(string name)
    {
        Dictionary<string, int>? players = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(playerFile));
        
        if (players is null)
        {
            Console.WriteLine("players.json does not exist");
            return null;
        }

        try
        {
            return new KeyValuePair<string, int>(name, players[name]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public KeyValuePair<string, int>? AddPlayer(KeyValuePair<string, int> player)
    {
        Dictionary<string, int>? players = GetPlayers();

        if (players is null)
        {
            Console.WriteLine("players.json does not exist");
            return null;
        }

        try
        {
            players.Add(player.Key, player.Value);
            
            if (players.TryGetValue(player.Key, out int temp))
            {
                File.WriteAllText(playerFile, JsonConvert.SerializeObject(players, Formatting.Indented));

                Console.WriteLine(player.Key + " successfully added");
                return player;
            }
            else
            {
                Console.WriteLine(player.Key + " failed to be added");
                return null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public Dictionary<string, int>? AddPlayers(Dictionary<string, int> playersToAdd)
    {
        Dictionary<string, int>? players = GetPlayers();

        if (players is null)
        {
            Console.WriteLine("players.json does not exist");
            return null;
        }

        try
        {
            foreach (KeyValuePair<string, int> captain in playersToAdd)
            {
                players.Add(captain.Key, captain.Value);
            }
            
            //Check if captains added here eventually
            
            File.WriteAllText(playerFile, JsonConvert.SerializeObject(players, Formatting.Indented));
            
            return playersToAdd; 
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public void DeletePlayerByName(string name)
    {
        Dictionary<string, int>? players = GetPlayers();

        if (players is null)
        {
            Console.WriteLine("players.json does not exist");
            return;
        }

        try
        {
            players.Remove(name);
            
            if (!players.TryGetValue(name, out int temp))
            {
                File.WriteAllText(playerFile, JsonConvert.SerializeObject(players, Formatting.Indented));
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
    
    public void DeletePlayerByObject(KeyValuePair<string, int> player)    
    {
        Dictionary<string, int>? players = GetPlayers();

        if (players is null)
        {
            Console.WriteLine("players.json does not exist");
            return;
        }

        try
        {
            players.Remove(player.Key);
            
            if (!players.TryGetValue(player.Key, out int temp))
            {
                File.WriteAllText(playerFile, JsonConvert.SerializeObject(players, Formatting.Indented));
                Console.WriteLine(player.Key + " successfully removed");
            }
            else
            {
                Console.WriteLine(player.Key + " failed to be removed");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    public KeyValuePair<string, int>? UpdatePlayer(KeyValuePair<string, int> newPlayer)
    {
        Dictionary<string, int>? players = GetPlayers();
        
        if (players is null)
        {
            Console.WriteLine("players.json does not exist");
            return null;
        }

        try
        {
            int oldPrice = players[newPlayer.Key];
            players[newPlayer.Key] = newPlayer.Value;
                
            if (!oldPrice.Equals(players[newPlayer.Key]))
            {
                File.WriteAllText(playerFile, JsonConvert.SerializeObject(players, Formatting.Indented));
                
                Console.WriteLine(newPlayer.Key + " successfully updated");
                return new KeyValuePair<string, int>(newPlayer.Key, newPlayer.Value);
            }
            else
            {
                Console.WriteLine(newPlayer.Key + " failed to be updated");
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
        if (!File.Exists(playerFile))
        {
            File.WriteAllText(playerFile, "{}");
        }
        else
        {
            Console.WriteLine("players.json already exists!");
        }
    }
}