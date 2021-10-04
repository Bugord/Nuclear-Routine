using System;
using UnityEngine;

namespace DefaultNamespace
{
    public enum CursorType
    {
        Pointer,
        BeforeGrab, 
        Grab
    }
    public class CursorManager : MonoBehaviour
    {
        public static CursorManager Instance;

        [SerializeField] private Texture2D pointerCursor;
        [SerializeField] private Texture2D beforeGrabCursor;
        [SerializeField] private Texture2D grabCursor;
        private readonly Vector2 _hotSpot = new Vector2(7f,0f);
        private readonly CursorMode _cursorMode = CursorMode.Auto;
        private void Awake()
        {
            Instance = this;
            Cursor.SetCursor(pointerCursor, _hotSpot, _cursorMode);
        }

        public void SetCursor(CursorType cursorType)
        {
            switch (cursorType)
            {
                case CursorType.Pointer:
                    Cursor.SetCursor(pointerCursor, _hotSpot, _cursorMode);
                    return;
                case CursorType.BeforeGrab:
                    Cursor.SetCursor(beforeGrabCursor, _hotSpot, _cursorMode);
                    return;
                case CursorType.Grab:
                    Cursor.SetCursor(grabCursor, _hotSpot, _cursorMode);
                    return;
            }
        }
    }
}