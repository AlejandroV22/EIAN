using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ToggleZonasInteractivas : MonoBehaviour
{
    [Header("Referencias")]
    public Toggle toggle;
    
    public GameObject[] zonasInteractivas;

    
    private Dictionary<Graphic, float> alphaOriginal = new Dictionary<Graphic, float>();

    
    public static bool zonasActivas = false;

    void Start()
    {
        if (toggle == null)
        {
            Debug.LogError("[ToggleZonasInteractivas] Falta asignar el Toggle.");
            return;
        }

        toggle.isOn = false;

        if (zonasInteractivas != null)
        {
            foreach (GameObject zona in zonasInteractivas)
            {
                if (zona == null) continue;

                foreach (Graphic graphic in zona.GetComponentsInChildren<Graphic>())
                {
                    if (!alphaOriginal.ContainsKey(graphic))
                        alphaOriginal[graphic] = graphic.color.a;
                }
            }
        }

        ToggleZonas(toggle.isOn);
        toggle.onValueChanged.AddListener(ToggleZonas);
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