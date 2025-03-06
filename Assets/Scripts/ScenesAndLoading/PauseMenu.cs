using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _duckNameUI;

    // ------------------------------------------------------------------------
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _duckNameUI.SetActive(!_duckNameUI.activeInHierarchy);
        }
    }
}