using UnityEngine;
using UnityEngine.InputSystem;
using EventBusSystem;

public class PlayerInputManager : MonoBehaviour, InputActions.IPlayerActions
{
    public static PlayerInputManager instance = null;

    [HideInInspector]
    public Vector2 MousePosition;
    [HideInInspector]
    public Vector2 MovementDirection;
    [HideInInspector]
    public Vector2 ScrollDirection;

    private InputActions m_InputActions;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        InitializeManager();
    }

    private void InitializeManager()
    {
        //...
    }

    private void Awake()
    {
        m_InputActions = new InputActions();
        m_InputActions.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        m_InputActions.Player.Enable();
    }

    private void OnDisable()
    {
        m_InputActions.Player.Disable();
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
        {
            EventBus.RaiseEvent<IPlayerInputHandler>(h => h.OnInventory());
        }
    }

    public void OnMouseLeft(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
        {
            EventBus.RaiseEvent<IMouseInputHandler>(h => h.OnMouseLeftDown());
        }
        if (context.action.phase == InputActionPhase.Canceled)
        {
            EventBus.RaiseEvent<IMouseInputHandler>(h => h.OnMouseLeftUp());
        }
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementDirection = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
        {
            EventBus.RaiseEvent<IPlayerInputHandler>(h => h.OnStartSprint());
        }
        if (context.action.phase == InputActionPhase.Canceled)
        {
            EventBus.RaiseEvent<IPlayerInputHandler>(h => h.OnStopSprint());
        }
    }

    public void OnMouseRight(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
        {
            EventBus.RaiseEvent<IMouseInputHandler>(h => h.OnMouseRightDown());
        }
        if (context.action.phase == InputActionPhase.Canceled)
        {
            EventBus.RaiseEvent<IMouseInputHandler>(h => h.OnMouseRightUp());
        }
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        ScrollDirection = context.ReadValue<Vector2>();

        if (context.action.phase == InputActionPhase.Started)
        {
            if (ScrollDirection.y > 0)
            {
                EventBus.RaiseEvent<IMouseInputHandler>(h => h.OnMouseWheelUp());
            }

            if (ScrollDirection.y < 0)
            {
                EventBus.RaiseEvent<IMouseInputHandler>(h => h.OnMouseWheelDown());
            }
        }        

    }

    public void OnFlashlight(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
        {
            EventBus.RaiseEvent<IPlayerInputHandler>(h => h.OnFlashlight()); 
        }
        if (context.action.phase == InputActionPhase.Canceled)
        {
            //
        }
    }
}
