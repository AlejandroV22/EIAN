using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CambioDeCorte : MonoBehaviour
{
    [Header("Referencias")]
    public TMP_Dropdown dropdownCortes;
    public List<GameObject> cortes;

    private int corteActual = 0;

    void Start()
    {
        for (int i = 0; i < cortes.Count; i++)
        {
            cortes[i].SetActive(i == corteActual);
        }

        if (dropdownCortes != null)
        {
            dropdownCortes.onValueChanged.AddListener(CambiarCorte);
        }
    }

    public void CambiarCorte(int indice)
    {
        if (indice < 0 || indice >= cortes.Count)
            return;

        cortes[corteActual].SetActive(false);
        cortes[indice].SetActive(true);
        corteActual = indice;

        Debug.Log("Corte cambiado a: " + cortes[indice].name);
    }
}