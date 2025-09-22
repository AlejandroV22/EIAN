using UnityEngine;

public class BotonConfiguration : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject panelConfiguration;
    public void OpenConfiguration()
    {
        if (panelConfiguration != null)
        {
            panelConfiguration.SetActive(true);
        }
        Debug.Log("Configuración abierta");
    }
}
