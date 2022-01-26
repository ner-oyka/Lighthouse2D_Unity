using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public class BaseWeapon : MonoBehaviour
{
    public int MagazineAmount = 1;
    public int Ammunition;
    [Range(0.5f, 10)] public float ReloadTime;

    [Range(0.01f, 25)] public float FireRate;

    private int remainingAmmunition;
    private float nextShootTime = 0;

    private bool isShooting;

    private List<WeaponAction> WeaponActions = new List<WeaponAction>();

    private void Awake()
    {
        WeaponActions.AddRange(GetComponents<WeaponAction>());

        remainingAmmunition = Ammunition;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);
        EventBus.RaiseEvent<IWeaponHandler>(h => h.OnUpdateAmountAmmunition(this, remainingAmmunition, MagazineAmount));
        yield break;
    }

    public void StartShot()
    {
        isShooting = true;
    }

    public void StopShot()
    {
        //nextShootTime = 0;
        isShooting = false;
    }

    public int GetAmmunition()
    {
        return remainingAmmunition;
    }

    public void UpdateWeapon()
    {
        if (isShooting)
        {
            if (remainingAmmunition > 0)
            {
                nextShootTime -= 1 * Time.deltaTime;
                if (nextShootTime <= 0)
                {                    
                    UseWeaponAction_Main();
                    remainingAmmunition--;
                    EventBus.RaiseEvent<IWeaponHandler>(h => h.OnUpdateAmountAmmunition(this, remainingAmmunition, MagazineAmount));
                    nextShootTime = FireRate;
                }
            }
            else
            {
                if (MagazineAmount > 0)
                {
                    nextShootTime = ReloadTime;
                    remainingAmmunition = Ammunition;
                    MagazineAmount--;
                    EventBus.RaiseEvent<IWeaponHandler>(h => h.OnUpdateAmountAmmunition(this, remainingAmmunition, MagazineAmount));
                    UseWeaponAction_Reloading();
                }
            }
        }
        else
        {
            if (nextShootTime > 0)
            {
                nextShootTime -= 1 * Time.deltaTime;
            }
        }

    }

    public void UseWeaponAction_Main()
    {
        WeaponActions.FindAll(action => action.ActionType == E_WeaponActionTypes.Main).ForEach(val => val.StartUseAction());
    }
    public void UseWeaponAction_Secondary()
    {
        WeaponActions.FindAll(action => action.ActionType == E_WeaponActionTypes.Secondary).ForEach(val => val.StartUseAction());
    }
    public void UseWeaponAction_Special()
    {
        WeaponActions.FindAll(action => action.ActionType == E_WeaponActionTypes.Special).ForEach(val => val.StartUseAction());
    }
    public void UseWeaponAction_Reloading()
    {
        WeaponActions.FindAll(action => action.ActionType == E_WeaponActionTypes.Reloading).ForEach(val => val.StartUseAction());
    }
}
