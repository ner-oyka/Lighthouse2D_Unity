using EventBusSystem;

public interface IEntityHandler : IGlobalSubscriber
{
    void OnTakeToInventory(Entity entity);
    void OnWeaponHit(in Entity entity, float value);
    void OnKillEntity(Entity entity);
    void OnShowDescription(string description);
    void OnHideDescription();
    void OnShowActionMenu(in Entity entity);
}