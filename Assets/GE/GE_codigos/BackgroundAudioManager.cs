using UnityEngine;

public class BackgroundAudioManager : MonoBehaviour
{
    [Header("Música de Fundo")]
    public AudioClip bgmClip;
    [Range(0f, 1f)]
    public float bgmVolume = 0.3f;

    [Header("Áudios de Loop Secundário (playlist)")]
    public AudioClip[] overlayClips = new AudioClip[14];
    [Range(0f, 1f)]
    public float overlayVolume = 0.7f;

    private AudioSource bgmSource;
    private AudioSource overlaySource;

    private bool[] overlayDisabled = new bool[14];
    private int currentOverlayIndex = -1;

    private bool overlay0Tocado = false;

    void Awake()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;
        bgmSource.playOnAwake = true;
        bgmSource.Play();

        overlaySource = gameObject.AddComponent<AudioSource>();
        overlaySource.loop = false;
        overlaySource.volume = overlayVolume;
        overlaySource.playOnAwake = false;

        PlayNextOverlay();
    }

    void Update()
    {
        if (!overlaySource.isPlaying)
        {
            PlayNextOverlay();
        }
    }

    void PlayNextOverlay()
    {
        // Toca o overlay 0 só uma vez, se ainda não foi tocado e não foi desativado
        if (!overlay0Tocado && overlayClips.Length > 0 && overlayClips[0] != null && !overlayDisabled[0])
        {
            currentOverlayIndex = 0;
            overlaySource.clip = overlayClips[0];
            overlaySource.loop = false;
            overlaySource.Play();
            overlay0Tocado = true;

            // Desativa automaticamente o overlay 0 após tocar a primeira vez
            StopOverlayAudio(0);

            return;
        }

        // Loop dos overlays 1 em diante
        int overlaysCount = overlayClips.Length;
        int tries = 0;
        int startIndex = currentOverlayIndex < 1 ? 1 : currentOverlayIndex + 1;

        do
        {
            currentOverlayIndex = (startIndex + tries) % overlaysCount;
            tries++;
            if (currentOverlayIndex == 0)
                currentOverlayIndex = 1;
        }
        while ((overlayDisabled[currentOverlayIndex] || overlayClips[currentOverlayIndex] == null) && tries <= overlaysCount);

        if (!overlayDisabled[currentOverlayIndex] && overlayClips[currentOverlayIndex] != null && currentOverlayIndex != 0)
        {
            overlaySource.clip = overlayClips[currentOverlayIndex];
            overlaySource.loop = false;
            overlaySource.Play();
        }
        else
        {
            overlaySource.Stop();
        }
    }

    /// <summary>
    /// Desativa o overlay desejado.
    /// </summary>
    public void StopOverlayAudio(int index)
    {
        if (index >= 0 && index < overlayDisabled.Length)
        {
            overlayDisabled[index] = true;
            if (index == 0)
            {
                // Se desativar overlay 0, nunca mais toca
                overlay0Tocado = true;
            }
            if (currentOverlayIndex == index && overlaySource.isPlaying)
            {
                overlaySource.Stop();
                PlayNextOverlay();
            }
        }
    }

    /// <summary>
    /// Retorna TRUE se o overlay está desativado ou fora dos limites.
    /// </summary>
    public bool IsOverlayDisabled(int index)
    {
        if (index >= 0 && index < overlayDisabled.Length)
            return overlayDisabled[index];
        return true;
    }
}