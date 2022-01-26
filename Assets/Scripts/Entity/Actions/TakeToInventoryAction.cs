using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public class TakeToInventoryAction : EntityAction
{
    public override bool IsCanUseAction()
    {
        return true;
    }

    public override void StartUseAction()
    {
        EventBus.RaiseEvent<IEntityHandler>(h => h.OnTakeToInventory(GetEntity()));
    }

    public override void StopUseAction()
    {
        //
    }
}
