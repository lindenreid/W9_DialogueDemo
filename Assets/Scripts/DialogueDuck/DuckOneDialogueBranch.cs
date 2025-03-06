using TMPro;

using UnityEngine;
using UnityEngine.Assertions;

public class DuckOneDialogueBranch : MonoBehaviour
{
    [SerializeField] private float _interactionDistance = 2.0f;
    [SerializeField] private GameObject _interactionPromptIcon;
    [SerializeField] private GameObject _activeDialogueIcon;
    [SerializeField] private GameObject _angerIcon;
    [SerializeField] private GameObject _happyIcon;
    [SerializeField] private DialogueUI _dialogue;
    [SerializeField] private string[] _lines;
    [SerializeField] private string _goodReply;
    [SerializeField] private string _badReply;

    private int _currentLine;
    private bool _runningDialogue;
    private bool _waitingForPlayerResponse;

    private void Update ()
    {
        if(Player.Instance == null) return;
        
        if(Vector3.Distance(transform.position, Player.Instance.transform.position) < _interactionDistance)
        {
            if(!_waitingForPlayerResponse && Input.GetKeyDown(KeyCode.Space))
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
            _waitingForPlayerResponse = false;
            _currentLine = 0;
            _interactionPromptIcon.SetActive(false);
            _activeDialogueIcon.SetActive(false);
            _happyIcon.SetActive(false);
            _angerIcon.SetActive(false);
            _dialogue.HideDialogue();
        }
    }

    private void AdvanceDialogue ()
    {
        _interactionPromptIcon.SetActive(false);
        _activeDialogueIcon.SetActive(true);
        
        _runningDialogue = true;

        if(_currentLine >= _lines.Length)
        {
            // if we run out of NPC lines, it's time to show the player dialogue options
            _dialogue.ShowPlayerOptions();
            _waitingForPlayerResponse = true;
        }
        else 
        {
            // otherwise, continue showing NPC dialogue lines
            _dialogue.ShowDialogue(_lines[_currentLine]);
        }

        _currentLine++;
    }

    // a button in the UI calls this
    public void ClickedGoodOption ()
    {
        _activeDialogueIcon.SetActive(false);
        _happyIcon.SetActive(true);
        _dialogue.ShowDialogue(_goodReply);

        // dialogue will reset next time it's displayed
        _currentLine = 0;
    }

    // a button in the UI calls this
    public void ClickedBadOption ()
    {
        _activeDialogueIcon.SetActive(false);
        _angerIcon.SetActive(true);
        _dialogue.ShowDialogue(_badReply);

        _currentLine = 0;
    }
}
