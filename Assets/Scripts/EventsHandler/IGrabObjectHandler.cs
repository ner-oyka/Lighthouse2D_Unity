using EventBusSystem;

public interface IGrabObjectHandler : IGlobalSubscriber
{
    void OnStartGrabObject();
    void OnStopGrabObject();
}