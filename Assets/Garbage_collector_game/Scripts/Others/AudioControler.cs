using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioControler : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string exposedVolumeParam = "MasterVolume"; // Debe coincidir con el parámetro expuesto en el Mixer

    [Header("UI")]
    [SerializeField] private Slider volumeSlider;      // Slider de la UI (0..1)
    [SerializeField] private Image volumeIconImage;    // Imagen a actualizar
    [SerializeField] private Sprite[] volumeLevelSprites = new Sprite[4]; // 0: mute, 1: bajo, 2: normal, 3: alto

    [Header("Persistencia")]
    [SerializeField] private string playerPrefsKey = "audio.volume.master";

    private void OnEnable()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    private void OnDisable()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(SetVolume);
        }
    }

    private void Start()
    {
        float initial = 1f;
        if (PlayerPrefs.HasKey(playerPrefsKey))
        {
            initial = PlayerPrefs.GetFloat(playerPrefsKey, 1f);
        }

        if (volumeSlider != null)
        {
            volumeSlider.value = Mathf.Clamp01(initial);
        }

        // Aplica volumen e icono de inicio
        SetVolume(Mathf.Clamp01(initial));
    }

    // Método público para asignar desde el evento del Slider o por código
    public void SetVolume(float value)
    {
        value = Mathf.Clamp01(value);

        // Mixer en dB (logarítmico). Si value ~ 0, dejamos un valor bajo (-80 dB)
        float dB = Mathf.Approximately(value, 0f) ? -80f : Mathf.Log10(value) * 20f;
        if (audioMixer != null && !string.IsNullOrEmpty(exposedVolumeParam))
        {
            audioMixer.SetFloat(exposedVolumeParam, dB);
        }

        // Actualiza el icono según el porcentaje
        UpdateVolumeIcon(value);

        // Guarda preferencia
        PlayerPrefs.SetFloat(playerPrefsKey, value);
        PlayerPrefs.Save();
    }

    private void UpdateVolumeIcon(float value)
    {
        if (volumeIconImage == null || volumeLevelSprites == null || volumeLevelSprites.Length < 4)
            return;

        int index;
        if (value <= 0.0001f)
            index = 0; // sin sonido
        else if (value <= 0.33f)
            index = 1; // bajo
        else if (value <= 0.66f)
            index = 2; // normal
        else
            index = 3; // alto

        var sprite = volumeLevelSprites[index];
        if (sprite != null)
        {
            volumeIconImage.sprite = sprite;
        }
    }
}
