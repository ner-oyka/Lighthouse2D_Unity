using EventBusSystem;

public interface IPlayerStateHandler : IGlobalSubscriber
{
    void OnPlayerAiming();
    void OnPlayerDefault();
}