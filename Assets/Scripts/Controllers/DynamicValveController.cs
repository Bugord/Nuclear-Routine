using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

namespace DefaultNamespace.Controllers
{
    public class DynamicValveController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        private bool _canBeDragged;
        private bool _isDragged;
        private bool _isOver;

        private Camera _camera;
        [SerializeField] private Vector2 valvePosition;
        [SerializeField] private float distanceToSnap;
        [SerializeField] private GameObject valve;


        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnMouseEnter()
        {
            if (!_isDragged && _canBeDragged)
                CursorManager.Instance.SetCursor(CursorType.BeforeGrab);
            _isOver = true;
        }

        private void OnMouseExit()
        {
            if (!_isDragged)
                CursorManager.Instance.SetCursor(CursorType.Pointer);
            
            _isOver = false;
        }

        private void OnMouseDown()
        {
            if(!_canBeDragged)
                return;
            CursorManager.Instance.SetCursor(CursorType.Grab);

            _isDragged = true;
            _rigidbody2D.isKinematic = true;
        }

        private void OnMouseUp()
        {
            CursorManager.Instance.SetCursor(_isOver ? CursorType.BeforeGrab : CursorType.Pointer);
            
            _isDragged = false;
            _rigidbody2D.isKinematic = false;
        }

        private void LateUpdate()
        {
            if (_isDragged && _canBeDragged)
            {
                var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousePosition.x, mousePosition.y);

                if (Vector2.Distance(transform.position, valvePosition) <= distanceToSnap)
                {
                    valve.SetActive(true);
                    gameObject.SetActive(false);
                    CursorManager.Instance.SetCursor(CursorType.BeforeGrab);

                }
            }
        }

        private IEnumerator SetDragable()
        {
            yield return new WaitForSeconds(1);
            _canBeDragged = true;
        }

        public void Drop()
        {
            _canBeDragged = false;
            _isDragged = false;
            _rigidbody2D.isKinematic = false;
            StartCoroutine(SetDragable());
        }
    }
}