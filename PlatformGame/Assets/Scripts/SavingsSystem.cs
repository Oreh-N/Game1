using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SavingSystem
{
    static string path = Application.persistentDataPath + "/savings";


    public static void SavePlayerState(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(path, FileMode.Create))
        { 
            PlayerData data = new PlayerData(player);
            formatter.Serialize(stream, data);
        }
    }


    public static PlayerData LoadPlayerState()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else 
        { 
            Debug.LogError("Saved file does't exist");
            return null;
        }
    }

}
