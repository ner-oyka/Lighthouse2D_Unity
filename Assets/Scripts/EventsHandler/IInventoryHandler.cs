using EventBusSystem;

public interface IInventoryHandler : IGlobalSubscriber
{
    void OnOpenInventory();
    void OnCloseInventory();
}