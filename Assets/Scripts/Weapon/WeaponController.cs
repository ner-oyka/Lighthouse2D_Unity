// Light Deer Games, 2021-2022

using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public class WeaponController : MonoBehaviour, IMouseInputHandler
{
    public List<BaseWeapon> playerWeapons;

    private int currentWeaponIndex = 0;


    private void OnEnable()
    {
        DisableWeapons();
        playerWeapons[currentWeaponIndex].gameObject.SetActive(true);
        playerWeapons[currentWeaponIndex].enabled = true;
        playerWeapons[currentWeaponIndex].StopShot();

        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        playerWeapons[currentWeaponIndex].enabled = false;

        EventBus.Unsubscribe(this);
    }

    public void OnMouseLeftDown()
    {
        StartShot();
    }

    public void OnMouseLeftUp()
    {
        StopShot();
    }

    public void OnMouseRightDown()
    {
    }

    public void OnMouseRightUp()
    {
    }

    public void OnMouseWheelUp()
    {
        ChangeWeaponUp();
    }

    public void OnMouseWheelDown()
    {
        ChangeWeaponDown();
    }

    private void StartShot()
    {
        if (playerWeapons[currentWeaponIndex] == null)
            return;

        playerWeapons[currentWeaponIndex].StartShot();
    }

    private void StopShot()
    {
        if (playerWeapons[currentWeaponIndex] == null)
            return;

        playerWeapons[currentWeaponIndex].StopShot();
    }

    private void ChangeWeaponDown()
    {
        DisableWeapons();
        if (currentWeaponIndex <= 0)
        {
            currentWeaponIndex = playerWeapons.Count - 1;
        }
        else
        {
            currentWeaponIndex--;
        }
        playerWeapons[currentWeaponIndex].gameObject.SetActive(true);
        EventBus.RaiseEvent<IWeaponHandler>(h => h.OnUpdateAmountAmmunition(playerWeapons[currentWeaponIndex], 
            playerWeapons[currentWeaponIndex].GetAmmunition(), 
            playerWeapons[currentWeaponIndex].MagazineAmount));
    }

    private void ChangeWeaponUp()
    {
        DisableWeapons();
        if (currentWeaponIndex >= playerWeapons.Count - 1)
        {
            currentWeaponIndex = 0;
        }
        else
        {
            currentWeaponIndex++;
        }
        playerWeapons[currentWeaponIndex].gameObject.SetActive(true);
        EventBus.RaiseEvent<IWeaponHandler>(h => h.OnUpdateAmountAmmunition(playerWeapons[currentWeaponIndex],
            playerWeapons[currentWeaponIndex].GetAmmunition(),
            playerWeapons[currentWeaponIndex].MagazineAmount));
    }

    private void DisableWeapons()
    {
        foreach (var w in playerWeapons)
        {
            w.gameObject.SetActive(false);
        }
    }
}
