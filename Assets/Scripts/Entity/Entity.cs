using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public class Entity : MonoBehaviour, IEntityHandler
{
    public E_EntityTypes Type;
    public float Health = 10f;
    public string Name;

    public string ItemId;

    private float m_CurrentHealth;
    private SpriteRenderer m_SpriteRender;

    private List<EntityAction> m_EntityActions = new List<EntityAction>();

    private void Start()
    {
        m_CurrentHealth = Health;
        m_SpriteRender = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        m_EntityActions.AddRange(GetComponents<EntityAction>());
    }

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void OnTakeToInventory(Entity entity)
    {
    }

    public void OnWeaponHit(in Entity entity, float value)
    {
        WeaponHitHandler(entity, value);
    }

    public void OnKillEntity(Entity entity)
    {

    }

    private void WeaponHitHandler(in Entity entity, float value)
    {
        if (entity == this)
        {
            m_CurrentHealth -= value;
            m_SpriteRender.color = Color.Lerp(m_SpriteRender.color, Color.red, 1 - m_CurrentHealth / Health);
            if (m_CurrentHealth <= 0)
            {
                //call destroy action
                EventBus.RaiseEvent<IEntityHandler>(h => h.OnKillEntity(this));

                Destroy(gameObject, 0.17f);
            }
        }
    }

    public void UseEntityAction_Main()
    {
        m_EntityActions.FindAll(action => action.ActionType == E_EntityActionTypes.Main).ForEach(val => val.StartUseAction());
    }
    public void UseEntityAction_Secondary()
    {
        m_EntityActions.FindAll(action => action.ActionType == E_EntityActionTypes.Secondary).ForEach(val => val.StartUseAction());
    }

    public void OnShowDescription(string description)
    {

    }

    public void OnHideDescription()
    {

    }

    public void OnShowActionMenu(in Entity entity)
    {

    }
}
