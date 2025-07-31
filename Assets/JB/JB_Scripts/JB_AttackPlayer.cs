using UnityEngine;

public class JB_AttackPlayer : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask destruivelLayer;
    public int damage = 1;
    public KeyCode attackKey = KeyCode.Space;

    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, destruivelLayer);
        foreach (Collider2D obj in hitObjects)
        {
            JB_ObjetoDestruivel destruivel = obj.GetComponent<JB_ObjetoDestruivel>();
            if (destruivel != null)
            {
                destruivel.TakeHit(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}