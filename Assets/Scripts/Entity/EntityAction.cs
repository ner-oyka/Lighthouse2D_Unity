using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAction : MonoBehaviour
{
    private Entity entity;
    public E_EntityActionTypes ActionType;
    public string ActionName;

    private void Awake()
    {
        entity = GetComponent<Entity>();
    }

    public Entity GetEntity()
    {
        return entity;
    }

    public abstract void StartUseAction();
    public abstract void StopUseAction();
    public abstract bool IsCanUseAction();
}
