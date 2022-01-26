using NodeCanvas.DialogueTrees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDialogueAction : EntityAction
{
    public DialogueTreeController DialogueController;

    public override bool IsCanUseAction()
    {
        return true;
    }

    public override void StartUseAction()
    {
        DialogueController.StartDialogue();
    }

    public override void StopUseAction()
    {
        //
    }
}
