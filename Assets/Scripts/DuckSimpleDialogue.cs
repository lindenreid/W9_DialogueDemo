using TMPro;

using UnityEngine;
using UnityEngine.Assertions;

public class DuckSimpleDialogue : MonoBehaviour
{
    [SerializeField] private float _interactionDistance = 2.0f;
    [SerializeField] private GameObject _interactionPromptIcon;
    [SerializeField] private GameObject _activeDialogueIcon;
    [SerializeField] private DialogueUI _dialogue;
    [SerializeField] private string[] _lines;

    private int _currentLine;
    private bool _runningDialogue;

    private void Update ()
    {
        if(Vector3.Distance(transform.position, Player.Instance.transform.position) < _interactionDistance)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                AdvanceDialogue();
            }
            else if(!_runningDialogue)
            {
                _interactionPromptIcon.SetActive(true);
            }
        }
        else
        {
            _runningDialogue = false;
            _currentLine = 0;
            _interactionPromptIcon.SetActive(false);
            _activeDialogueIcon.SetActive(false);
            _dialogue.HideDialogue();
        }
    }

    private void AdvanceDialogue ()
    {
        _interactionPromptIcon.SetActive(false);
        _activeDialogueIcon.SetActive(true);
        
        _runningDialogue = true;

        _dialogue.ShowDialogue(_lines[_currentLine]);

        _currentLine++;
        if(_currentLine >= _lines.Length)
        {
            _currentLine = 0;
        }
    }
}
