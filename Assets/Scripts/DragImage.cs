using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DragImage : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Vector2 lastMousePosition;

    [Header("Opciones de centrado")]
    public Vector3 defaultScale = Vector3.one;
    public float animationDuration = 0.4f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - lastMousePosition;
        rectTransform.anchoredPosition += delta;
        lastMousePosition = eventData.position;
    }

    public void CenterImage()
    {
        StopAllCoroutines(); // Para evitar que se acumulen animaciones
        StartCoroutine(AnimateToCenter(Vector2.zero, defaultScale));
    }

    IEnumerator AnimateToCenter(Vector2 targetPos, Vector3 targetScale)
    {
        Vector2 startPos = rectTransform.anchoredPosition;
        Vector3 startScale = rectTransform.localScale;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / animationDuration;
            float smoothT = Mathf.SmoothStep(0f, 1f, t); // Suaviza el movimiento

            rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, smoothT);
            rectTransform.localScale = Vector3.Lerp(startScale, targetScale, smoothT);

            yield return null;
        }

        rectTransform.anchoredPosition = targetPos;
        rectTransform.localScale = targetScale;
    }
}
