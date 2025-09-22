using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CambiarFondo : MonoBehaviour
{
    public TMP_Dropdown backgroundDropdown;
    public Image backgroundImage;
    public Sprite[] backgroundOptions; // Asigna tus sprites desde el inspector

    void Start()
    {
        // Restaurar última selección
        int savedIndex = PlayerPrefs.GetInt("BackgroundIndex", 0);
        backgroundDropdown.value = savedIndex;
        ApplyBackground(savedIndex);

        backgroundDropdown.onValueChanged.AddListener(OnBackgroundChanged);
    }

    void OnBackgroundChanged(int index)
    {
        ApplyBackground(index);

        // Guardar la elección del jugador
        PlayerPrefs.SetInt("BackgroundIndex", index);
        PlayerPrefs.Save();
    }

    void ApplyBackground(int index)
    {
        if (index >= 0 && index < backgroundOptions.Length)
        {
            backgroundImage.sprite = backgroundOptions[index];
        }
    }
}
