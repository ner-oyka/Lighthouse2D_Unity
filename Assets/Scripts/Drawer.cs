using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public class Drawer : MonoBehaviour, IMouseInputHandler
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

                EventBus.RaiseEvent<IGrabObjectHandler>(h => h.OnStartGrabObject());
            }
        }
    }

    private void MouseLeftUp()
    {
        isGrubbed = false;
        EventBus.RaiseEvent<IGrabObjectHandler>(h => h.OnStopGrabObject());
    }

    private void FixedUpdate()
    {
        if (isGrubbed)
        {
            Vector3 entityPos = saveTouchPoint - (saveTouchPoint - transform.position);
            var _mousePosition = Camera.main.ScreenToWorldPoint(PlayerInputManager.instance.MousePosition);
            var dist = Vector2.Distance(_mousePosition, entityPos);
            _mousePosition = (_mousePosition - entityPos).normalized;

            GetComponent<Rigidbody2D>().velocity = _mousePosition * 10000 * dist * Time.fixedDeltaTime;
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
