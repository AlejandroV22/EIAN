using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ToggleZonasInteractivas : MonoBehaviour
{
    [Header("Referencias")]
    public Toggle toggle;
    public GameObject zonasInteractivas;

    // Guarda alpha original de cada gráfico (Image o UIPolygon)
    private Dictionary<Graphic, float> alphaOriginal = new Dictionary<Graphic, float>();

    // Bandera global accesible desde otros scripts
    public static bool zonasActivas = true;

    void Start()
    {
        if (zonasInteractivas != null)
        {
            foreach (Transform child in zonasInteractivas.transform)
            {
                // Buscamos cualquier componente que herede de Graphic
                Graphic graphic = child.GetComponent<Graphic>();
                if (graphic != null && !alphaOriginal.ContainsKey(graphic))
                {
                    alphaOriginal[graphic] = graphic.color.a;
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
        zonasActivas = estado; // 🔑 actualiza la bandera global

        if (zonasInteractivas != null)
        {
            foreach (Transform child in zonasInteractivas.transform)
            {
                Graphic graphic = child.GetComponent<Graphic>();
                if (graphic != null && alphaOriginal.ContainsKey(graphic))
                {
                    Color color = graphic.color;
                    color.a = estado ? alphaOriginal[graphic] : 0f;
                    graphic.color = color;
                }
            }
        }
    }
}
