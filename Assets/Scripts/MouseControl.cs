using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using EventBusSystem;

public class MouseControl : MonoBehaviour, INoteHandler, IDialogHandler, IInventoryHandler
{
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out MousePosition lpMousePosition);

    [StructLayout(LayoutKind.Sequential)]
    public struct MousePosition
    {
        public int x;
        public int y;
    }

    MousePosition mouseSavePosition;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void OnCloseNote()
    {
        SetCursorPos(mouseSavePosition.x, mouseSavePosition.y);
    }

    public void OnOpenNote(string noteId)
    {
        GetCursorPos(out mouseSavePosition);
    }

    public void OnStartDialog()
    {
        GetCursorPos(out mouseSavePosition);
    }

    public void OnCancelDialog()
    {
        SetCursorPos(mouseSavePosition.x, mouseSavePosition.y);
    }

    public void OnOpenInventory()
    {
        GetCursorPos(out mouseSavePosition);
    }

    public void OnCloseInventory()
    {
        SetCursorPos(mouseSavePosition.x, mouseSavePosition.y);
    }
}
