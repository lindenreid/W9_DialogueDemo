using UnityEngine;

public class PlayerPrefsLoader : MonoBehaviour
{
    private string _duckKey = "duckName";
    private string _duckDefaultName = "Duckie";

    public string DuckName {
        get 
        {
            return PlayerPrefs.GetString(_duckKey, _duckDefaultName);
        }
    }

    public static PlayerPrefsLoader Instance { get; private set; } // singleton stuff

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
    }

    // ------------------------------------------------------------------------
    public void SaveDuckName(string name)
    {
        PlayerPrefs.SetString(_duckKey, name);
        PlayerPrefs.Save();
    }
}