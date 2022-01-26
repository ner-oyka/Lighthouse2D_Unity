using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

[Serializable]
public class InventoryItem
{
    public string Id;
    public string Name;
    public E_EntityTypes Type;

    public InventoryItem(string id, string name, E_EntityTypes type)
    {
        Id = id;
        Name = name;
        Type = type;
    }
}

public class Inventory : MonoBehaviour, IEntityHandler
{
    public List<InventoryItem> Items = new List<InventoryItem>();

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
        Take(entity);
    }

    public void OnWeaponHit(in Entity entity, float value)
    {

    }

    public void OnKillEntity(Entity entity)
    {

    }

    private void Take(in Entity entity)
    {
        InventoryItem item = new InventoryItem(entity.ItemId, entity.Name, entity.Type);
        Items.Add(item);

        Destroy(entity.gameObject);
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
