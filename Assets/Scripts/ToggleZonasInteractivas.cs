using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ToggleZonasInteractivas : MonoBehaviour
{
    [Header("Referencias")]
    public Toggle toggle;
    public GameObject zonasInteractivas;

    // Guardar alpha original de cada imagen
    private Dictionary<Image, float> alphaOriginal = new Dictionary<Image, float>();

    void Start()
    {
        if (zonasInteractivas != null)
        {
            foreach (Transform child in zonasInteractivas.transform)
            {
                Image image = child.GetComponent<Image>();
                if (image != null && !alphaOriginal.ContainsKey(image))
                {
                    alphaOriginal[image] = image.color.a;
                }
            }
        }

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
                if (image != null && alphaOriginal.ContainsKey(image))
                {
                    Color color = image.color;
                    // Restaurar su alpha original o ponerlo en 0
                    color.a = estado ? alphaOriginal[image] : 0f;
                    image.color = color;

                        // Activar o desactivar la interactividad
                    image.raycastTarget = true;
                }
            }
        }
    }
}
