using UnityEngine;
using UnityEngine.UI;

public class ZoomImage : MonoBehaviour
{
    public RectTransform imageTransform;
    public Canvas canvas;

    public float zoomSpeed = 0.2f;
    public float minScale = 1f;
    public float maxScale = 3f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.001f)
        {
            ZoomAtCursor(scroll);
        }
    }

    private void ZoomAtCursor(float scroll)
    {
        float currentScale = imageTransform.localScale.x;
        float newScale = Mathf.Clamp(
            currentScale + scroll * zoomSpeed,
            minScale,
            maxScale
        );

        if (Mathf.Approximately(currentScale, newScale))
            return;

        Vector2 localMousePos;

        Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay
            ? null
            : canvas.worldCamera;

        // Posición del cursor relativa al RectTransform
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imageTransform,
            Input.mousePosition,
            cam,
            out localMousePos
        );

        float scaleFactor = newScale / currentScale;

        // Mantener fijo el punto bajo el cursor
        Vector2 offset = localMousePos * (scaleFactor - 1f);

        imageTransform.localScale = Vector3.one * newScale;
        imageTransform.anchoredPosition -= offset * currentScale;
    }
}