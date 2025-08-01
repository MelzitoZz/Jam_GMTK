using UnityEngine;

public class JB_PlayerCollect : MonoBehaviour
{
    public Transform collectPoint;
    public float collectRange = 0.5f;
    public LayerMask itemLayer;
    public KeyCode collectKey = KeyCode.P;

    private JB_Inventory inventory;

    void Start()
    {
        inventory = FindObjectOfType<JB_Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(collectKey))
        {
            Collider2D[] items = Physics2D.OverlapCircleAll(collectPoint.position, collectRange, itemLayer);
            foreach (Collider2D item in items)
            {
                JB_ItemPickup pickup = item.GetComponent<JB_ItemPickup>();
                if (pickup != null)
                {
                    pickup.Collect();
                    break;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (collectPoint == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(collectPoint.position, collectRange);
    }
}