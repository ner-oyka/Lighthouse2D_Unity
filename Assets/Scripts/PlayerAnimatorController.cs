using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
using Aarthificial.Reanimation;

public class PlayerAnimatorController : MonoBehaviour, IPlayerStateHandler
{
    private PlayerMovement _movementController;
    private Reanimator _animatorController;

    private bool isAiming;

    private void OnEnable()
    {
        _movementController = GetComponent<PlayerMovement>();
        _animatorController = GetComponent<Reanimator>();

        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void OnPlayerAiming()
    {
        isAiming = true;
    }

    public void OnPlayerDefault()
    {
        isAiming = false;
    }

    private void Update()
    {
        _animatorController.Set("root_movement", isAiming);
        _animatorController.Set("idle_transition", _movementController.IsMove);   
        _animatorController.Set("isRunning", _movementController.IsRunning);   
        _animatorController.Set("isAiming", _movementController.IsMove);   
    }
}
