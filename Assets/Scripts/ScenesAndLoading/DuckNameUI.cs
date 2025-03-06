using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class DuckNameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _duckNameText;
    [SerializeField] private TMP_InputField _inputField;

    // ------------------------------------------------------------------------
    private void Start()
    {
        _duckNameText.text = PlayerPrefsLoader.Instance.DuckName;
        gameObject.SetActive(false);
    }

    // ------------------------------------------------------------------------
    public void SetDuckName ()
    {
        _duckNameText.text = _inputField.text;
        PlayerPrefsLoader.Instance.SaveDuckName(_inputField.text);
    }
}
