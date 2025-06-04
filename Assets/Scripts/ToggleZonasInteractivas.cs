using UnityEngine;
using UnityEngine.UI;

public class ToggleZonasInteractivas : MonoBehaviour
{
    [Header("Referencias")]
    public Toggle toggle;
    public GameObject zonasInteractivas;

    void Start()
    {
        if (toggle != null && zonasInteractivas != null)
        {
            toggle.onValueChanged.AddListener(ToggleZonas);
            toggle.isOn = true;
        }
    }

    void ToggleZonas(bool estado)
    {
        if (zonasInteractivas != null)
        {
            foreach (Transform child in zonasInteractivas.transform)
            {
                Image image = child.GetComponent<Image>();
                if (image != null)
                {
                    Color color = image.color;
                    color.a = estado ? 1f : 0f;
                    image.color = color;
                    image.raycastTarget = true; // Asegurar que siga detectando clics
                }
            }
        }
    }
}
