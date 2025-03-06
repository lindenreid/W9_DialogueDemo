using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _loadButton;

    // ------------------------------------------------------------------------
    private void Start ()
    {
        // disables the "load" button if there's no save file to load
        _loadButton.interactable = SaveDataLoader.Instance._HasSaveFile;
    }

    // ------------------------------------------------------------------------
    public void LoadGame ()
    {
        SaveDataLoader.Instance.LoadSaveData();
        GameController.Instance.LoadGame();
        LoadMainScene();
    }

    // ------------------------------------------------------------------------
    public void StartNewGame ()
    {
        SaveDataLoader.Instance.CreateNewSave();
        LoadMainScene();
    }

    // ------------------------------------------------------------------------
    private void LoadMainScene ()
    {
        SceneManager.LoadScene(1);
    }
}
