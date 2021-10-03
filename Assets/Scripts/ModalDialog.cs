using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
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
    
    [SerializeField] private HorizontalLayoutGroup _horizontalContentLayoutGroup;
    [SerializeField] private VerticalLayoutGroup _horizontalImageLayoutGroup;
    [SerializeField] private Image panelImage;
    
    public event Action Closed;

    public void SetContent(ModalContentSO content)
    {
        ((RectTransform)transform).localScale = Vector3.zero;
        panelImage.enabled = true;

        titleElement.transform.parent.gameObject.SetActive(!string.IsNullOrWhiteSpace(content.title));
        titleElement.text = content.title;
        if (content.modalLayout == ModalLayout.Horizontal)
        {
            imageElementHorizontal.transform.parent.gameObject.SetActive(content.sprite);
            imageElementHorizontal.sprite = content.sprite;
            imageElementHorizontal.SetNativeSize();
            descriptionElementHorizontal.text = content.description;
        }
        else
        {
            imageElementVertical.sprite = content.sprite;
            imageElementVertical.SetNativeSize();
            descriptionElementVertical.text = content.description;
        }

        if (content.reverseContent)
        {
            if (content.modalLayout == ModalLayout.Horizontal)
            {
                _horizontalContentLayoutGroup.reverseArrangement = true;
                _horizontalImageLayoutGroup.padding = new RectOffset(15, 0, 0, 0);
            }
        }
        else
        {
            if (content.modalLayout == ModalLayout.Horizontal)
            {
                _horizontalContentLayoutGroup.reverseArrangement = false;
                _horizontalImageLayoutGroup.padding = new RectOffset(0, 15, 0, 0);
            }
        }
        GetComponent<RectTransform>().anchoredPosition = content.position;
        gameObject.SetActive(true);

        StartCoroutine(Kostyl());
    }

    public void CloseDialog()
    {
        panelImage.enabled = false;
        gameObject.SetActive(false);
        Closed?.Invoke();
    }

    private IEnumerator Kostyl()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        MarkToUpdate();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        MarkToUpdate();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        var rect = ((RectTransform) transform); 
        rect.localScale = new Vector3(0.8f, 0.8f, 1f);
        DOTween.To(() => rect.localScale, x => rect.localScale = x, Vector3.one, 0.3f);
    }

    private void MarkToUpdate()
    {
        LayoutRebuilder.MarkLayoutForRebuild((RectTransform)transform);
        LayoutGroup[] parentLayoutGroups = gameObject.GetComponentsInParent<LayoutGroup>();
        foreach (LayoutGroup group in parentLayoutGroups) {
            LayoutRebuilder.MarkLayoutForRebuild((RectTransform)group.transform);
        }
    }
    
}
