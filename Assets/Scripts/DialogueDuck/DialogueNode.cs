using UnityEngine;

[CreateAssetMenu(fileName = "DialogueLine", menuName = "ScriptableObjects/DialogueLine", order = 1)]
public class DialogueNode : ScriptableObject
{
    // the lines of dialogue the NPC says for this node
    public string[] _lines;
    public Sprite _thoughtBubbleSprite;

    // the potential replies from the player
    public string[] _playerReplyOptions;

    // each index in _playerReplyOptions corresponds to an NPC reply in _npcReplies
    // so, for example, if the player chooses the first option (= index 0) from _playerReplyOptions,
    // the next DialogueLine that should be shown is index 0 in _npcReplies
    public DialogueNode[] _npcReplies;
}