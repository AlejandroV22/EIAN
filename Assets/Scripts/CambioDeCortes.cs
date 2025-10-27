using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CambioDeCorte : MonoBehaviour
{
    [Header("Referencias")]
    public Dropdown dropdownCortes; // O TMP_Dropdown si usas TextMeshPro
    public List<GameObject> cortes; // Asigna todos los cortes aquí (Corte1, Corte2, Corte3...)

    private int corteActual = 0;

    void Start()
    {
        // Ocultar todos menos el primero
        for (int i = 0; i < cortes.Count; i++)
        {
            cortes[i].SetActive(i == corteActual);
        }

        // Agregar listener al dropdown
        if (dropdownCortes != null)
        {
            dropdownCortes.onValueChanged.AddListener(CambiarCorte);
        }
    }

    public void CambiarCorte(int indice)
    {
        if (indice < 0 || indice >= cortes.Count)
            return;

        // Ocultar el actual
        cortes[corteActual].SetActive(false);

        // Mostrar el nuevo
        cortes[indice].SetActive(true);

        corteActual = indice;

        Debug.Log("Corte cambiado a: " + cortes[indice].name);
    }
}
