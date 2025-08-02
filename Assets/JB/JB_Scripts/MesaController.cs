using UnityEngine;

public class MesaController : MonoBehaviour
{
    public Sprite mesaSemLivroSprite;
    public SpriteRenderer sr;
    public Sprite livroSprite;
    public JB_Inventory inventario;
    public JB_ColetarMensagem coletarMensagem; // Mensagem de "User P"

    private bool livroColetado = false;
    private bool jogadorPerto = false;

    void Start()
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        if (inventario == null)
            inventario = FindObjectOfType<JB_Inventory>();

        if (coletarMensagem != null)
            coletarMensagem.HideMessage();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!livroColetado && other.CompareTag("Player"))
        {
            jogadorPerto = true;
            coletarMensagem?.ShowMessage();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            coletarMensagem?.HideMessage();
        }
    }

    void Update()
    {
        if (jogadorPerto && !livroColetado && Input.GetKeyDown(KeyCode.P))
        {
            if (inventario != null && inventario.AddItem(livroSprite))
            {
                livroColetado = true;
                sr.sprite = mesaSemLivroSprite;
                coletarMensagem?.HideMessage();
                Debug.Log("Livro coletado da mesa.");
            }
            else
            {
                Debug.Log("Inventário cheio ou inventário não encontrado.");
            }
        }
    }
}
