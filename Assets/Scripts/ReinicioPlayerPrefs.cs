using UnityEngine;

public class ReinicioPlayerPrefs : MonoBehaviour
{
    public void ReiniciarProgreso()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Progreso reiniciado.");
    }
}
