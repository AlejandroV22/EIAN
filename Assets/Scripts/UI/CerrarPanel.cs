using UnityEngine;

public class CerrarPanel : MonoBehaviour
{
    public GameObject panelAocultar;

    public void Cerrar()
    {
        panelAocultar.SetActive(false);
    }
}
