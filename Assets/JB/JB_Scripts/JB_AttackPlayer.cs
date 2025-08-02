using UnityEngine;

public class JB_AttackPlayer : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask destruivelLayer;
    public int damage = 1;
    public KeyCode attackKey = KeyCode.Space;
    public Animator animator;
    public float attackDelay = 0.3f;
    public float attackCooldown = 0.5f;

    private bool canAttack = true;

    void Update()
    {
        if (Input.GetKeyDown(attackKey) && canAttack)
        {
            if (animator != null)
                animator.SetTrigger("Attack");

            canAttack = false;
            Invoke(nameof(DoAttack), attackDelay);
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    void DoAttack()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, destruivelLayer);
        foreach (Collider2D obj in hitObjects)
        {
            JB_ObjetoDestruivel destruivel = obj.GetComponent<JB_ObjetoDestruivel>();
            if (destruivel != null)
                destruivel.TakeHit(damage);
        }
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}