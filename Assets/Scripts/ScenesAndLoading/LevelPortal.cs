using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour
{
    [SerializeField] private int _levelToLoad;

    // ------------------------------------------------------------------------
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            SceneManager.LoadScene(_levelToLoad);
        }
    }
}
