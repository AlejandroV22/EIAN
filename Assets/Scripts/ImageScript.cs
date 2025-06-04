using UnityEngine;
using UnityEngine.UI;

public class ZoomImage : MonoBehaviour
{
    public RectTransform imageTransform;
    public float zoomSpeed = 0.1f;
    public float minScale = 1f;
    public float maxScale = 3f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float newScale = Mathf.Clamp(imageTransform.localScale.x + scroll * zoomSpeed, minScale, maxScale);
            imageTransform.localScale = new Vector3(newScale, newScale, 1f);
        }
    }
}
