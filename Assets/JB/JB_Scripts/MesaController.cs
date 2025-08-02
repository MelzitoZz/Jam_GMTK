using UnityEngine;

public class MesaController : MonoBehaviour
{
    public Sprite mesaSemLivroSprite;
    public SpriteRenderer sr;
    public Sprite livroSprite;
    public JB_Inventory inventario;

    private bool livroColetado = false;

    void Start()
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        if (inventario == null)
            inventario = FindObjectOfType<JB_Inventory>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!livroColetado && other.CompareTag("Player") && Input.GetKeyDown(KeyCode.P))
        {
            if (inventario != null && inventario.AddItem(livroSprite))
            {
                livroColetado = true;
                sr.sprite = mesaSemLivroSprite;
                Debug.Log("Livro coletado da mesa.");
            }
            else
            {
                Debug.Log("Inventário cheio ou inventário não encontrado.");
            }
        }
    }
}
