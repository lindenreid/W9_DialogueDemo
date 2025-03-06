using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Start ()
    {
        List<string> locations = GameController.Instance.GetNamesOfLocationsVisited();
        string locationText = "";
        foreach(string locationName in locations)
        {
            locationText += locationName + "\n";
        }

        _text.text = locationText;
    }
}
