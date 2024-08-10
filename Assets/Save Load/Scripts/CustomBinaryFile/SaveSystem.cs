using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // To work with file, when opening and closing
using System.Runtime.Serialization.Formatters.Binary; // To access the binary formater 

public static class SaveSystem
{

   public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        // Application.persistentDataPath this will get a path to a data directory on the operating system that will not change
        // string path = Path.Combine(Application.persistentDataPath, "/player.fun");
        string path = Application.persistentDataPath + "/player.fun";

        // Filestream is a stream of data contain in a file, and we can use a particular file steam to read and write form a file.
        // FileMode is a an action you want to do the the file
        FileStream stream = new FileStream(path, FileMode.Create);

        // data to write 
        PlayerData playerData = new PlayerData(player);

        // incert to a file, convert the data to binary, Serialize write down to the file
        formatter.Serialize(stream, playerData);

        Debug.Log("File has being save to " + path);

        stream.Close(); // close the stream after writing data
    }

    public static PlayerData LoadPlayer()
    {
        // Application.persistentDataPath this will get a path to a data directory on the operating system that will not change
        string path = Application.persistentDataPath + "/player.fun";
        //string path = Path.Combine(Application.persistentDataPath, "/player.fun");


        if (File.Exists(path))
        {
            //check if the file exist
            BinaryFormatter formatter = new BinaryFormatter();

            // Open the file
            FileStream stream = new FileStream(path, FileMode.Open);

            // read the file stream, the convert to the player data
            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close(); // close the stream after writing data
            Debug.Log("File has being loaded from " + path);
            return playerData;

        }
        else
        {
            Debug.Log("Save file not found in " + path);
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
