using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAction : MonoBehaviour
{
    private BaseWeapon Weapon;
    public E_WeaponActionTypes ActionType;

    private void Awake()
    {
        Weapon = GetComponent<BaseWeapon>();
    }

    public BaseWeapon GetWeapon()
    {
        return Weapon;
    }

    public abstract void StartUseAction();
    public abstract void StopUseAction();
    public abstract bool IsCanUseAction();
}
