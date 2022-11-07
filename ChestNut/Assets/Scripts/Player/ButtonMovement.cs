using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMovement : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    public Vector2 pos;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        pos = Vector2.zero;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        rectTransform.anchoredPosition = Vector2.zero;
        pos = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("OnPointerDown");
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag");
        pos = rectTransform.anchoredPosition + eventData.delta / canvas.scaleFactor;
        pos.x = Mathf.Clamp(pos.x, -50, 50);
        pos.y = Mathf.Clamp(pos.y, -50, 50);
        rectTransform.anchoredPosition = pos;
    }
}
