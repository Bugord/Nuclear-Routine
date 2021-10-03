using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ModalLayout
{
    Horizontal,
    Vertical
}
public class ModalDialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleElement;
    
    [SerializeField] private Image imageElementHorizontal;
    [SerializeField] private Image imageElementVertical;

    [SerializeField] private TextMeshProUGUI descriptionElementHorizontal;
    [SerializeField] private TextMeshProUGUI descriptionElementVertical;
    
    public void SetContent(ModalContentSO content)
    {
        titleElement.text = content.title;
        if (content.modalLayout == ModalLayout.Horizontal)
        {
            imageElementHorizontal.sprite = content.sprite;
            descriptionElementHorizontal.text = content.description;
        }
        else
        {
            imageElementVertical.sprite = content.sprite;
            descriptionElementVertical.text = content.description;
        }
    }
}
