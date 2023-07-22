using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CollectionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite hoverSprite;

    private Image buttonImage;
    private RectTransform buttonRectTransform;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonRectTransform = GetComponent<RectTransform>();
        buttonImage.sprite = defaultSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = hoverSprite;
        buttonRectTransform.sizeDelta = new Vector2(222f, 333f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = defaultSprite;
        buttonRectTransform.sizeDelta = new Vector2(212f, 323f);
    }
}