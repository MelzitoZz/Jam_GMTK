using UnityEngine;

public class JB_ItemPickup : MonoBehaviour
{
    public Sprite itemSprite;

    public void Collect()
    {
        JB_Inventory inv = FindObjectOfType<JB_Inventory>();
        if (inv != null && inv.AddItem(itemSprite))
        {
            Destroy(gameObject);
        }
    }
}