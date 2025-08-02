using UnityEngine;

public class VentiladorController : MonoBehaviour
{
    public AudioClip somVentilador;  
    public Transform jogador;          // Referência ao jogador
    public float alcanceMaximo = 10f;  // Distância máxima para ouvir o som
    private AudioSource audioSource;
    private bool tocando = false;

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
        }
    }
}
