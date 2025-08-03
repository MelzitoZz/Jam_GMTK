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
    public GameObject PCDist;
    public float distanciaMaxima = 10f;

    public AudioClip audioConcluido;
    private AudioSource audioSource;

    public BackgroundAudioManager backgroundAudioManager;

    // Flags para controle dos itens
    private bool bolaUsada = false;
    private bool bonecaUsada = false;
    private bool leiteUsado = false;
    private bool overlay12Removido = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Use null para itemSprite para "mão vazia"
    public void UseItem(Sprite itemSprite)
    {
        string itemName = itemSprite != null ? itemSprite.name : "MaoVazia";
        Debug.Log("Usando item: " + itemName);

        // PROTEÇÃO: Não pode estar segurando o crachá para nada, exceto bater ponto no PC
        bool isCracha = itemName == "CRACHÁ_0";

        // Se estiver com crachá, só permitir interação com o PC na hora certa
        if (isCracha)
        {
            float disPC = Vector2.Distance(jogador.transform.position, PCDist.transform.position);
            if (disPC <= distanciaMaxima)
            {
                PCController pcController = PCDist.GetComponent<PCController>();
                if (pcController != null)
                {
                    bool removerCracha = pcController.UsarCracha();
                    if (removerCracha)
                    {
                        RemoverItem(itemSprite);
                        Debug.Log("Crachá removido do inventário.");
                    }

                    if (backgroundAudioManager != null)
                    {
                        backgroundAudioManager.StopOverlayAudio(10);
                        Debug.Log("Overlay 10 desativado após uso do crachá.");
                    }
                }
                else
                {
                    Debug.LogWarning("PCController não encontrado no GameObject do PC!");
                }
            }
            else
            {
                Debug.Log("Você está longe demais do PC para usar o crachá.");
            }
            // Impede qualquer outro uso segurando o crachá!
            return;
        }

        // Bloqueia interações com a mão cheia de crachá em qualquer outro objeto!
        if (itemName == "CRACHÁ_0")
        {
            Debug.Log("Não é possível usar o crachá para essa interação.");
            return;
        }

        bool fezAcao = false;

        switch (itemName)
        {
            case "CHAVE_0":
                if (bau != null)
                {
                    float distancia = Vector2.Distance(jogador.transform.position, bauDist.transform.position);
                    if (distancia <= distanciaMaxima)
                    {
                        bau.DestrancarBau();
                        fezAcao = true;
                        RemoverItem(itemSprite);
                    }
                    else Debug.Log("Você está longe demais do baú.");
                }
                break;

            case "BOLA_0":
                if (bau != null && bau.aberto)
                {
                    float distancia = Vector2.Distance(jogador.transform.position, bauDist.transform.position);
                    if (distancia <= distanciaMaxima)
                    {
                        bau.ColocarBola();
                        fezAcao = true;
                        RemoverItem(itemSprite);
                        bolaUsada = true;
                        TentarRemoverOverlay12();
                    }
                    else Debug.Log("Você está longe demais do baú.");
                }
                break;

            case "BONECA_0":
                if (bau != null && bau.aberto)
                {
                    float distancia = Vector2.Distance(jogador.transform.position, bauDist.transform.position);
                    if (distancia <= distanciaMaxima)
                    {
                        bau.ColocarBoneca();
                        fezAcao = true;
                        RemoverItem(itemSprite);
                        bonecaUsada = true;
                        TentarRemoverOverlay12();
                    }
                    else Debug.Log("Você está longe demais do baú.");
                }
                break;

            case "LIVRO_0":
                if (estante != null && !estante.temLivro)
                {
                    float distancia = Vector2.Distance(jogador.transform.position, estanteDist.transform.position);
                    if (distancia <= distanciaMaxima)
                    {
                        estante.ColocarLivro();
                        fezAcao = true;
                        RemoverItem(itemSprite);
                        if (backgroundAudioManager != null)
                        {
                            backgroundAudioManager.StopOverlayAudio(5);
                            Debug.Log("Overlay 5 desativado após usar o livro.");
                        }
                    }
                    else Debug.Log("Você está longe demais da estante.");
                }
                break;

            case "LEITE_0":
                if (geladeira != null && geladeira.aberta && !geladeira.temLeite)
                {
                    float distancia = Vector2.Distance(jogador.transform.position, geladeiraDist.transform.position);
                    if (distancia <= distanciaMaxima)
                    {
                        geladeira.ColocarLeite();
                        fezAcao = true;
                        RemoverItem(itemSprite);
                        leiteUsado = true;
                        TentarRemoverOverlay12();
                    }
                    else Debug.Log("Você está longe demais da geladeira.");
                }
                break;

            case "XICARA DE CAFE_0":
                Debug.Log("Você tomou a xícara de café!");
                fezAcao = true;
                RemoverItem(itemSprite);
                if (backgroundAudioManager != null)
                {
                    backgroundAudioManager.StopOverlayAudio(1);
                    Debug.Log("Overlay 1 desativado após usar o café.");
                }
                break;

            case "MaoVazia": // Mão vazia usada para interagir com o PC (ligar/trabalhar/desligar)
                float distPC = Vector2.Distance(jogador.transform.position, PCDist.transform.position);
                if (distPC <= distanciaMaxima)
                {
                    PCController pcController = PCDist.GetComponent<PCController>();
                    if (pcController != null)
                    {
                        pcController.InteragirMaoVazia();
                        fezAcao = true;
                    }
                }
                break;

            default:
                Debug.Log("Esse item não faz nada especial.");
                break;
        }

        if (fezAcao && audioConcluido != null)
            audioSource.PlayOneShot(audioConcluido);
    }

    private void RemoverItem(Sprite itemSprite)
    {
        if (inventario != null)
        {
            inventario.RemoveItem(itemSprite);
            inventario.UpdateUI();
        }
    }

    private void TentarRemoverOverlay12()
    {
        if (!overlay12Removido && bolaUsada && bonecaUsada && leiteUsado)
        {
            if (backgroundAudioManager != null)
            {
                backgroundAudioManager.StopOverlayAudio(12);
                overlay12Removido = true;
                Debug.Log("Overlay 12 desativado após usar bola, boneca e leite.");
            }
            else
            {
                Debug.LogWarning("BackgroundAudioManager não está atribuído!");
            }
        }
    }
}