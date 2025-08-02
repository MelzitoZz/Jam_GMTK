using UnityEngine;

public class BGMusicManager : MonoBehaviour
{
    public AudioClip bgmClip;
    [Range(0f, 1f)]
    public float volume = 0.5f;

    private AudioSource audioSource;

    void Awake()
    {
        // Procura ou adiciona um AudioSource automaticamente
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = volume;
    }

    void Start()
    {
        // Começa a tocar só se não estiver tocando ainda
        if (!audioSource.isPlaying && bgmClip != null)
            audioSource.Play();
    }
}