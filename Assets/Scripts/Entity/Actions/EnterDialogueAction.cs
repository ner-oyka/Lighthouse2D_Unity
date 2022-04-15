using NodeCanvas.DialogueTrees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

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
        EventBus.RaiseEvent<IDialogHandler>(h => h.OnStartDialog());
    }

    public override void StopUseAction()
    {
        //
    }
}
