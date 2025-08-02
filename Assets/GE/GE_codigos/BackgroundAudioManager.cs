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

    void Awake()
    {
        // Música de fundo
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;
        bgmSource.playOnAwake = true;
        bgmSource.Play();

        // Overlay: apenas um AudioSource
        overlaySource = gameObject.AddComponent<AudioSource>();
        overlaySource.loop = false;
        overlaySource.volume = overlayVolume;
        overlaySource.playOnAwake = false;

        // Inicia tocando o primeiro válido
        PlayNextOverlay();
    }

    void Update()
    {
        // Quando o overlay termina, toca o próximo
        if (!overlaySource.isPlaying)
        {
            PlayNextOverlay();
        }
    }

    void PlayNextOverlay()
    {
        int tries = 0;
        do
        {
            currentOverlayIndex = (currentOverlayIndex + 1) % overlayClips.Length;
            tries++;
            // pula se estiver desativado ou nulo
        } while ((overlayDisabled[currentOverlayIndex] || overlayClips[currentOverlayIndex] == null) && tries <= overlayClips.Length);

        // Se encontrou um válido
        if (!overlayDisabled[currentOverlayIndex] && overlayClips[currentOverlayIndex] != null)
        {
            overlaySource.clip = overlayClips[currentOverlayIndex];
            overlaySource.Play();
        }
        else
        {
            // Não há mais áudios para tocar
            overlaySource.Stop();
        }
    }

    // Chame esta função para parar (pular) um overlay específico (ex: ao completar uma task)
    public void StopOverlayAudio(int index)
    {
        if (index >= 0 && index < overlayDisabled.Length)
        {
            overlayDisabled[index] = true;
            // Se está tocando o que foi desativado, já troca para o próximo
            if (currentOverlayIndex == index && overlaySource.isPlaying)
            {
                overlaySource.Stop();
                PlayNextOverlay();
            }
        }
    }
}