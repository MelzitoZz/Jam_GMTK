using UnityEngine;

public class UsarItens : MonoBehaviour
{
    public BauController bau;
    public GeladeiraController geladeira;
    public EstanteController estante; 
    public JB_Inventory inventario;
    public GameObject jogador;
    public GameObject bauDist;
    public GameObject estanteDist;
    public GameObject geladeiraDist;
    public GameObject PCDist; // objeto usado só para calcular a distância do PC

    public float distanciaMaxima = 10f;
    float distancia;
    float distanciaEst;
    float distanciaGel;
    float disPC;

    public AudioClip audioConcluido;
    private AudioSource audioSource;
    public BackgroundAudioManager backgroundAudioManager;

    private bool overlayAudioParado = false;

    // Fade durations para cada tipo de overlay (ajuste conforme necessário)
    private const int fadeOverlayNormal = 5;
    private const int fadeOverlayImportante = 12;
    private const int fadeOverlaySla = 10;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void UseItem(Sprite itemSprite)
    {
        if (itemSprite == null)
        {
            Debug.LogWarning("Nenhum item selecionado para uso.");
            return;
        }

        string itemName = itemSprite.name;
        Debug.Log("Usando item: " + itemName);

        bool fezAcao = false;
        bool pararOverlay = false;
        int fadeTime = fadeOverlayNormal; // valor padrão

        switch (itemName)
        {
            case "CHAVE_0":
                distancia = Vector2.Distance(jogador.transform.position, bauDist.transform.position);
                if (distancia <= distanciaMaxima && bau != null)
                {
                    bau.DestrancarBau();
                    fezAcao = true;
                    if (inventario != null)
                    {
                        inventario.RemoveItem(itemSprite);
                        inventario.UpdateUI();
                    }
                }
                else
                {
                    Debug.Log("Não foi possível utilizar a chave.");
                }
                break;

            case "BOLA_0":
                distancia = Vector2.Distance(jogador.transform.position, bauDist.transform.position);
                if (distancia <= distanciaMaxima && bau != null && bau.aberto)
                {
                    bau.ColocarBola();
                    fezAcao = true;
                    pararOverlay = true;
                    fadeTime = fadeOverlayImportante;
                    if (inventario != null)
                    {
                        inventario.RemoveItem(itemSprite);
                        inventario.UpdateUI();
                    }
                }
                else
                {
                    Debug.Log("Não foi possível utilizar a bola.");
                }
                break;

            case "BONECA_0":
                distancia = Vector2.Distance(jogador.transform.position, bauDist.transform.position);
                if (distancia <= distanciaMaxima && bau != null && bau.aberto)
                {
                    bau.ColocarBoneca();
                    fezAcao = true;
                    pararOverlay = true;
                    fadeTime = fadeOverlayImportante;
                    if (inventario != null)
                    {
                        inventario.RemoveItem(itemSprite);
                        inventario.UpdateUI();
                    }
                }
                else
                {
                    Debug.Log("Não foi possível utilizar a boneca.");
                }
                break;

            case "LIVRO_0":
                distanciaEst = Vector2.Distance(jogador.transform.position, estanteDist.transform.position);
                if (distanciaEst <= distanciaMaxima && estante != null && !estante.temLivro)
                {
                    estante.ColocarLivro();
                    fezAcao = true;
                    pararOverlay = true;
                    fadeTime = fadeOverlayNormal;
                    if (inventario != null)
                    {
                        inventario.RemoveItem(itemSprite);
                        inventario.UpdateUI();
                    }
                }
                else
                {
                    Debug.Log("Não foi possível utilizar o livro.");
                }
                break;

            case "LEITE_0":
                distanciaGel = Vector2.Distance(jogador.transform.position, geladeiraDist.transform.position);
                if (distanciaGel <= distanciaMaxima && geladeira != null && geladeira.aberta && !geladeira.temLeite)
                {
                    geladeira.ColocarLeite();
                    fezAcao = true;
                    pararOverlay = true;
                    fadeTime = fadeOverlayImportante;
                    if (inventario != null)
                    {
                        inventario.RemoveItem(itemSprite);
                        inventario.UpdateUI();
                    }
                }
                else
                {
                    Debug.Log("Não foi possível utilizar o leite.");
                }
                break;

            case "CRACHÁ_0":
                disPC = Vector2.Distance(jogador.transform.position, PCDist.transform.position);
                if (disPC <= distanciaMaxima)
                {
                    fezAcao = true;
                    if (inventario != null)
                    {
                        inventario.RemoveItem(itemSprite);
                        inventario.UpdateUI();
                    }
                    // Desativa o overlay 10 imediatamente usando o método que você já tem
                    if (backgroundAudioManager != null)
                    {
                        backgroundAudioManager.StopOverlayAudio(10);
                    }
                }
                else
                {
                    Debug.Log("Não foi possível utilizar o crachá.");
                }
                break;


            default:
                Debug.Log("Esse item não faz nada especial.");
                break;
        }

        if (fezAcao && audioConcluido != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioConcluido);
        }

        if (fezAcao && pararOverlay && !overlayAudioParado && backgroundAudioManager != null)
        {
            backgroundAudioManager.StopOverlayAudio(fadeTime);
            overlayAudioParado = true;
        }
    }
}