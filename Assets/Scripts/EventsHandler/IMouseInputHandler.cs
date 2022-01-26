using EventBusSystem;

public interface IMouseInputHandler : IGlobalSubscriber
{
    void OnMouseLeftDown();
    void OnMouseLeftUp();
    void OnMouseRightDown();
    void OnMouseRightUp();
    void OnMouseWheelUp();
    void OnMouseWheelDown();
}