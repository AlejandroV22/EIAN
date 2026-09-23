using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CambioDeCorte : MonoBehaviour
{
    [Header("Referencias")]
    public Dropdown dropdownCortes; 
    public List<GameObject> cortes; 

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
