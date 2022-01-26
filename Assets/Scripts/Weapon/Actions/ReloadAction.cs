using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAction : WeaponAction
{
    public override bool IsCanUseAction()
    {
        return true;
    }

    public override void StartUseAction()
    {
        Debug.Log("Reloading");
    }

    public override void StopUseAction()
    {
        //
    }
}
