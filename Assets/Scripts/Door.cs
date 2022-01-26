using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public class Door : MonoBehaviour, IMouseInputHandler
{
    bool isGrubbed;
    Vector3 saveTouchPoint;

    void Start()
    {
        this.enabled = false;
    }

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    private void MouseLeftDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(PlayerInputManager.instance.MousePosition), Vector2.zero, 100f, LayerMask.GetMask("Entity"));
        if (hit.collider != null)
        {
            if (hit.collider.transform == transform)
            {
                isGrubbed = true;
                saveTouchPoint = hit.point;
            }
        }
    }

    private void MouseLeftUp()
    {
        isGrubbed = false;
    }

    private void FixedUpdate()
    {
        if (isGrubbed)
        {
            Vector3 entityPos = saveTouchPoint - (saveTouchPoint - transform.position);
            var _mousePosition = Camera.main.ScreenToWorldPoint(PlayerInputManager.instance.MousePosition);
            var dist = Vector2.Distance(_mousePosition, entityPos);

            Quaternion targetRot = Quaternion.LookRotation(Vector3.forward, entityPos - _mousePosition);
            targetRot = Quaternion.Slerp(transform.rotation, targetRot, Time.fixedDeltaTime * 10f);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.fixedDeltaTime * 15f);

        }
    }

    public void OnMouseLeftDown()
    {
        MouseLeftDown();
    }

    public void OnMouseLeftUp()
    {
        MouseLeftUp();
    }

    public void OnMouseRightDown()
    {

    }

    public void OnMouseRightUp()
    {

    }

    public void OnMouseWheelUp()
    {

    }

    public void OnMouseWheelDown()
    {

    }
}
