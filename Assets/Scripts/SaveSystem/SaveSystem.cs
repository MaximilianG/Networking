using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePseudo (MenuManager _menuManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/pseudo.saveddata";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(_menuManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPseudo()
    {
        string path = Application.persistentDataPath + "/pseudo.saveddata";

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
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
