// Light Deer Games, 2021-2022

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using EventBusSystem;

public class WeaponController : MonoBehaviour, IMouseInputHandler
{
    public List<BaseWeapon> playerWeapons;
    public Light2D aimingArea;

    private int currentWeaponIndex = 0;

    private bool aiming = false;

    private void OnEnable()
    {
        DisableWeapons();
        playerWeapons[currentWeaponIndex].gameObject.SetActive(true);
        playerWeapons[currentWeaponIndex].enabled = true;
        playerWeapons[currentWeaponIndex].StopShot();

        aimingArea.gameObject.SetActive(false);

        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        playerWeapons[currentWeaponIndex].enabled = false;

        EventBus.Unsubscribe(this);
    }

    public void OnMouseLeftDown()
    {
        if (aiming)
        {
            StartShot();
        }
    }

    public void OnMouseLeftUp()
    {
        StopShot();
    }

    public void OnMouseRightDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(PlayerInputManager.instance.MousePosition), Vector2.zero, 1000f, LayerMask.GetMask("Entity"));
        if (hit.collider != null)
        {
            if (Vector2.Distance(transform.position, hit.transform.position) <= GameSettings.instance.MaxDistanceToEntityInteraction)
            {
                return;
            }
        }

        aiming = true;
        aimingArea.gameObject.SetActive(true);
        EventBus.RaiseEvent<IPlayerStateHandler>(h => h.OnPlayerAiming());
    }

    public void OnMouseRightUp()
    {
        aiming = false;
        aimingArea.pointLightInnerAngle = aimingArea.pointLightOuterAngle = 90.0f;
        aimingArea.gameObject.SetActive(false);
        StopShot();
        EventBus.RaiseEvent<IPlayerStateHandler>(h => h.OnPlayerDefault());
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

    private void Update()
    {
        if (aiming)
        {
            aimingArea.pointLightInnerAngle = aimingArea.pointLightOuterAngle = Mathf.Lerp(aimingArea.pointLightOuterAngle, 1.5f, Time.deltaTime * 3.0f);
        }
    }
}
