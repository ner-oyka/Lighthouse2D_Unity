using EventBusSystem;

public interface IPlayerInputHandler : IGlobalSubscriber
{
    void OnStartSprint();
    void OnStopSprint();
    void OnInventory();
    void OnFlashlight();
}