using UnityEngine;
using EventBusSystem;

public class PlayerInteractionHandler : MonoBehaviour, IMouseInputHandler
{
    private Camera m_MainCamera;

    private Entity selectedEntity = null;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    private void Awake()
    {
        m_MainCamera = Camera.main;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(m_MainCamera.ScreenToWorldPoint(PlayerInputManager.instance.MousePosition), Vector2.zero, 1000f, LayerMask.GetMask("Entity"));
        if (hit.collider != null)
        {
            //in every frame
            //---
            if (selectedEntity)
            {
                EventBus.RaiseEvent<IEntityHandler>(h => h.OnShowDescription(selectedEntity.Name));
            }
            //---

            if (hit.transform.GetComponent<Entity>() == selectedEntity)
            {
                return;
            }
            selectedEntity = hit.transform.GetComponent<Entity>();

            // select action
            EventBus.RaiseEvent<IEntityHandler>(h => h.OnShowDescription(selectedEntity.Name));
            return;
        }
        if (selectedEntity != null)
        {
            selectedEntity = null;

            //deselect action
            EventBus.RaiseEvent<IEntityHandler>(h => h.OnHideDescription());
        }
    }

    private void UseMainAction()
    {
        if (selectedEntity != null)
        {
            if (Vector2.Distance(transform.position, selectedEntity.transform.position) <= GameSettings.instance.MaxDistanceToEntityInteraction)
            {
                //activate action
                selectedEntity.UseEntityAction_Main();
            }
        }
    }

    private void ActionMenu()
    {
        if (selectedEntity != null)
        {
            if (Vector2.Distance(transform.position, selectedEntity.transform.position) <= GameSettings.instance.MaxDistanceToEntityInteraction)
            {
                //activate actions menu
                EventBus.RaiseEvent<IEntityHandler>(h => h.OnShowActionMenu(selectedEntity));
            }
        }
    }

    public void OnMouseLeftDown()
    {
        UseMainAction();
    }

    public void OnMouseLeftUp()
    {

    }

    public void OnMouseRightDown()
    {
        ActionMenu();
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
