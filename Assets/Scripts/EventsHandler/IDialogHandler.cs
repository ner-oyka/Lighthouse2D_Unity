using EventBusSystem;

public interface IDialogHandler : IGlobalSubscriber
{
    void OnStartDialog();
    void OnCancelDialog();
}