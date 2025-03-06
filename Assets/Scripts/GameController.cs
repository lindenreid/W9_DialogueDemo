using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private string[] _levelNames = new string[4] {"Main Menu", "Main World", "Land of Pink Trees", "Land of Pine Trees"};
    private bool[] _locationsVisited = new bool[4]; // auotmatically fills all spots with false

    public static GameController Instance { get; private set; } // singleton stuff

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

        // subscribe SceneLoaded method to scene loading event
        SceneManager.sceneLoaded += SceneLoaded;
    }

    // ------------------------------------------------------------------------
    public void LoadGame ()
    {
        _locationsVisited = SaveDataLoader.Instance.SaveData._LevelsVisited;
    }

    // ------------------------------------------------------------------------
    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // we don't care about Scene 0 - that's the main menu
        if(scene.buildIndex == 0) return;

        // add our current Scene to the list of places the player has been
        _locationsVisited[scene.buildIndex] = true;

        // save changes
        SaveDataLoader.Instance.SavePlayerData(_locationsVisited);
    }

    // ------------------------------------------------------------------------
    // returns a list of all of the names of the locations the player has visited
    public List<string> GetNamesOfLocationsVisited ()
    {
        List<string> locations = new List<string>();
        for(int i = 1; i < _locationsVisited.Length; i++)
        {
            if(_locationsVisited[i])
            {
                locations.Add(_levelNames[i]);
            }
        }
        return locations;
    }
}