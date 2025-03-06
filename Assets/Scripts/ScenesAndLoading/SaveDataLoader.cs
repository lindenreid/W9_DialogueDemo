using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

using UnityEngine;

// Serializable tag allows us to write the data in this class to other formats,
// important for allowing us to store it as save data!
// we could not call XmlSerializer.Serialize() on it without this tag
[Serializable]
public class PlayerSaveData
{
    // data we want to Serialize (including Save) needs to either be public
    // or have the [SerializeField] tag!
    public bool[] _LevelsVisited = new bool[4];
}

public class SaveDataLoader : MonoBehaviour
{
    private PlayerSaveData _saveData;
    public PlayerSaveData SaveData {
        get {return _saveData;}
    }

    public static SaveDataLoader Instance { get; private set; } // singleton stuff

    // ------------------------------------------------------------------------
    // singleton stuff
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        // persist this GameObject between Scene loads
        DontDestroyOnLoad(this);
    }

    // ------------------------------------------------------------------------
    private string _filePath {
        get
        {
            string dir = Application.persistentDataPath + "/" + "saves";
            if(!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }
            dir += "/" + "save.binary";
            return dir;
        }
    }

    // ------------------------------------------------------------------------
    public bool _HasSaveFile
    {
        get
        {
            FileInfo existingSave = new FileInfo(_filePath);
            if(existingSave != null && existingSave.Exists) {
                return true;
            }  
            return false;
        }
    }

    // ------------------------------------------------------------------------
    public void CreateNewSave ()
    {
        // clear any existing save data
        FileInfo existingSave = new FileInfo(_filePath);
        if(existingSave != null && existingSave.Exists) {
            existingSave.Delete();
        }  

        // create and save new save data
        _saveData = new PlayerSaveData();
        SavePlayerData(_saveData._LevelsVisited);
    }

    // ------------------------------------------------------------------------
    public void SavePlayerData (bool[] newLocationData)
    {
        // update our save data with new information
        _saveData._LevelsVisited = newLocationData;

        // write the save data to an XML file
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerSaveData));
        TextWriter writer = new StreamWriter(_filePath);

        // the try/catch statement allows us to safely execute the lines of code inside try{}
        // if any errors are thrown, we'll print out info about them
        // but continue to execute the rest of the method
        try {
            serializer.Serialize(writer, _saveData);
        }
        catch (SerializationException e) {
            Debug.LogError("Player save data file saving failed; reason: " + e.Message);
        }

        writer.Close();
    }

    // ------------------------------------------------------------------------
    public void LoadSaveData () {
        // use the XMLSerializer class to read our XML save file
        // and turn it into the data stored inside the PlayerSaveData class!
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerSaveData));
        FileStream fs = new FileStream(_filePath, FileMode.Open);

        PlayerSaveData saveData;
        try {
            saveData = (PlayerSaveData)serializer.Deserialize(fs);

            if(saveData != null)
            {
                _saveData = saveData;
            }
        } catch (SystemException e) {
            Debug.LogError("Save file deserialization failed. Message: " + e.Message);
        }

        fs.Close();
    }
}