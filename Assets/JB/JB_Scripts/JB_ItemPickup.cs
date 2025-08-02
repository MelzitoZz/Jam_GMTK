using UnityEngine;

public class JB_ItemPickup : MonoBehaviour
{
    public Sprite itemSprite; 
    public JB_ColetarMensagem coletarMensagem; 

    public void Collect()
    {
        JB_Inventory inv = FindObjectOfType<JB_Inventory>();
        if (inv != null && inv.AddItem(itemSprite))
        {
            coletarMensagem?.HideMessage();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Não foi possível adicionar o item ao inventário.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            coletarMensagem?.ShowMessage();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            coletarMensagem?.HideMessage();
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
