using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public class LookNoteAction : EntityAction
{
    public override bool IsCanUseAction()
    {
        return true;
    }

    public override void StartUseAction()
    {
        EventBus.RaiseEvent<INoteHandler>(h => h.OnOpenNote(GetEntity().ItemId));
        
        //EventManager.RaiseOnStopPlayerMovement();
        //EventManager.RaiseOnDisablePlayerInteraction();
        //EventManager.RaiseOnDisableWeaponController();
    }

    public override void StopUseAction()
    {
        //
    }
}
