using EventBusSystem;

public interface INoteHandler : IGlobalSubscriber
{
    void OnOpenNote(string noteId);
    void OnCloseNote();
}