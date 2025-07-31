using UnityEngine;

public class JB_ItemPickup : MonoBehaviour
{
    public Sprite itemSprite; // O sprite que aparecerá no slot

    public void Collect()
    {
        JB_Inventory inv = FindObjectOfType<JB_Inventory>();
        if (inv != null && inv.AddItem(itemSprite))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.P))
        {
            JB_Inventory inv = FindObjectOfType<JB_Inventory>();
            if (inv != null && inv.AddItem(itemSprite))
            {
                Destroy(gameObject); // Remove o item do mundo
            }
        }
    }


}