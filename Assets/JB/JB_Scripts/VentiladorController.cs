using UnityEngine;

public class VentiladorController : MonoBehaviour
{
    public AudioClip somVentilador;
    public AudioClip audioTaskConcluida; // Som de task concluída (toca só uma vez)
    public Transform jogador;            // Referência ao jogador
    public float alcanceMaximo = 10f;

    public BackgroundAudioManager backgroundAudioManager;

    private AudioSource audioSource;
    private bool tocando = false;
    private bool taskConcluidaTocada = false; // Flag

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = somVentilador;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0f;
    }

    void Update()
    {
        if (!tocando) return;

        if (jogador == null) return;

        float distancia = Vector3.Distance(jogador.position, transform.position);

        if (distancia > alcanceMaximo)
        {
            audioSource.volume = 0f;
        }
        else
        {
            // volume varia de 1 (perto) a 0 (longe)
            audioSource.volume = 1 - (distancia / alcanceMaximo);
        }
    }

    public void AlternarSom()
    {
        if (tocando)
        {
            audioSource.Stop();
            tocando = false;
            audioSource.volume = 0f;
            Debug.Log("Ventilador desligado.");
        }
        else
        {
            audioSource.Play();
            tocando = true;
            Debug.Log("Ventilador ligado.");

            // Task concluída: só toca UMA vez na vida!
            if (!taskConcluidaTocada && audioTaskConcluida != null)
            {
                AudioSource.PlayClipAtPoint(audioTaskConcluida, transform.position);
                taskConcluidaTocada = true;
            }

            // Para overlay 7 do BackgroundAudioManager
            if (backgroundAudioManager != null)
            {
                backgroundAudioManager.StopOverlayAudio(7);
                Debug.Log("Overlay 7 desativado após ligar o ventilador.");
            }
            else
            {
                Debug.LogWarning("BackgroundAudioManager não está atribuído no VentiladorController!");
            }
        }
    }
}