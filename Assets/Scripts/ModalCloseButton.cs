using System;
using UnityEngine;

public class ModalCloseButton : MonoBehaviour
{
    private ModalDialog _modalDialog;

    private void Awake()
    {
        _modalDialog = GetComponentInParent<ModalDialog>();
    }

    public void CloseModal()
    {
        _modalDialog.CloseDialog();
    }
}