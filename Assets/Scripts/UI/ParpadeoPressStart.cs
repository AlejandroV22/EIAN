using UnityEngine;
using UnityEngine.UI;

public class ParpadeoPressStart : MonoBehaviour
{
    [SerializeField] private float velocidad = 1.5f;   // parpadeos por segundo
    [SerializeField] private float alphaMinimo = 0f;   // transparencia mínima
    [SerializeField] private float alphaMaximo = 1f;   // transparencia máxima

    private Image imagen;

    void Awake()
    {
        imagen = GetComponent<Image>();
    }

    void Update()
    {
        bool visible = Mathf.Repeat(Time.time, 1f / velocidad) < (0.5f / velocidad);
        Color color = imagen.color;
        color.a = visible ? alphaMaximo : alphaMinimo;
        imagen.color = color;
    }
}