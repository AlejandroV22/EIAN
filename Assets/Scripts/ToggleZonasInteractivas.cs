using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ToggleZonasInteractivas : MonoBehaviour
{
    [Header("Referencias")]
    public Toggle toggle;
    public GameObject[] zonasInteractivas;

    // Guarda alpha original de cada gráfico (Image, Text, etc.)
    private Dictionary<Graphic, float> alphaOriginal = new Dictionary<Graphic, float>();

    // Bandera global accesible desde otros scripts
    public static bool zonasActivas = true;

    void Start()
    {
        if (zonasInteractivas != null)
        {
            foreach (GameObject zona in zonasInteractivas)
            {
                if (zona == null) continue;

                foreach (Graphic graphic in zona.GetComponentsInChildren<Graphic>())
                {
                    if (!alphaOriginal.ContainsKey(graphic))
                    {
                        alphaOriginal[graphic] = graphic.color.a;
                    }
                }
            }
        }

        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ToggleZonas);
            toggle.isOn = true;
        }
    }

    void ToggleZonas(bool estado)
    {
        zonasActivas = estado;

        if (zonasInteractivas == null) return;

        foreach (GameObject zona in zonasInteractivas)
        {
            if (zona == null) continue;

            foreach (Graphic graphic in zona.GetComponentsInChildren<Graphic>())
            {
                if (alphaOriginal.ContainsKey(graphic))
                {
                    Color color = graphic.color;
                    color.a = estado ? alphaOriginal[graphic] : 0f;
                    graphic.color = color;
                }
            }
        }
    }
}