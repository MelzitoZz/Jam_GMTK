using UnityEngine;

public class JB_ItemPickup : MonoBehaviour
{
    public Sprite itemSprite; // O sprite que aparecerá no slot
    public JB_ColetarMensagem coletarMensagem; // Arraste o script do Canvas aqui

    public void Collect()
    {
        JB_Inventory inv = FindObjectOfType<JB_Inventory>();
        if (inv != null && inv.AddItem(itemSprite))
        {
            if (coletarMensagem != null)
                coletarMensagem.HideMessage();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && coletarMensagem != null)
        {
            coletarMensagem.ShowMessage();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && coletarMensagem != null)
        {
            coletarMensagem.HideMessage();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.P))
        {
            Collect();
        }
    }
}