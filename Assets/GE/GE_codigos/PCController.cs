using UnityEngine;

public class PCController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite spriteDesligado;
    public Sprite spriteLigado;
    public Sprite spriteTrabalhando;
    public Sprite spriteBateuPonto;
    public Sprite spriteTaskConcluida;

    public AudioClip somBaterPonto;
    public AudioClip somTeclando;
    public AudioClip somTaskConcluida;

    public AudioSource audioSource;
    public BackgroundAudioManager backgroundAudioManager; // Arraste no Inspector

    private int estado = 0; // 0=desligado, 1=ligado, 2=trabalhando, 3=esperando bater ponto final, 4=task concluída, 5=desligado e inutilizável
    private bool crachaUsadoPrimeiraVez = false;
    private bool crachaUsadoSegundaVez = false;
    private bool tecladoTocando = false;

    void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = spriteDesligado;
    }

    public void InteragirMaoVazia()
    {
        switch (estado)
        {
            case 0: // Desligado
                spriteRenderer.sprite = spriteLigado;
                estado = 1;
                Debug.Log("PC ligado com mão vazia.");
                // PARA overlay 3 ao ligar o PC
                if (backgroundAudioManager != null)
                    backgroundAudioManager.StopOverlayAudio(3);
                break;
            case 2: // Trabalhar (depois da 1ª batida)
                spriteRenderer.sprite = spriteTrabalhando;
                if (somTeclando != null && !tecladoTocando)
                {
                    audioSource.loop = true;
                    audioSource.clip = somTeclando;
                    audioSource.Play();
                    tecladoTocando = true;
                }
                estado = 3;
                Debug.Log("Trabalhando...");
                break;
            case 4: // Desligar após concluir a task
                spriteRenderer.sprite = spriteDesligado;
                estado = 5; // PC inutilizável
                Debug.Log("PC desligado e não pode mais ser usado.");
                // PARA overlay 11 e 8 ao desligar o PC
                if (backgroundAudioManager != null)
                {
                    backgroundAudioManager.StopOverlayAudio(11);
                    backgroundAudioManager.StopOverlayAudio(8);
                }
                // TOCA SOM DE TASK CONCLUÍDA AO DESLIGAR
                if (somTaskConcluida != null)
                    audioSource.PlayOneShot(somTaskConcluida);
                break;
            case 1:
            case 3:
            case 5:
                Debug.Log("Não é possível interagir com o PC nesse estado com a mão vazia.");
                break;
        }
    }

    public bool UsarCracha()
    {
        if (estado == 1 && !crachaUsadoPrimeiraVez)
        {
            crachaUsadoPrimeiraVez = true;
            spriteRenderer.sprite = spriteBateuPonto;
            if (somBaterPonto != null) audioSource.PlayOneShot(somBaterPonto);
            estado = 2;
            Debug.Log("Primeira batida de ponto!");
            // PARA overlay 6 na primeira batida de crachá
            if (backgroundAudioManager != null)
                backgroundAudioManager.StopOverlayAudio(6);
            return false; // NÃO remove crachá ainda
        }
        else if (estado == 3 && !crachaUsadoSegundaVez)
        {
            crachaUsadoSegundaVez = true;
            spriteRenderer.sprite = spriteBateuPonto;
            // Para o teclado ANTES de tocar som de bater ponto
            if (tecladoTocando)
            {
                audioSource.Stop();
                audioSource.loop = false;
                tecladoTocando = false;
            }
            if (somBaterPonto != null) audioSource.PlayOneShot(somBaterPonto);
            estado = 4;
            Debug.Log("Ponto final batido! Interaja de novo com a mão vazia para desligar.");
            // PARA overlay 10 na segunda batida de crachá
            if (backgroundAudioManager != null)
                backgroundAudioManager.StopOverlayAudio(10);
            return true; // Agora remove o crachá!
        }
        else
        {
            Debug.Log("Não é possível usar o crachá agora!");
            return false;
        }
    }
}