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

    public AudioClip attackSwingSound;    // Som ao atacar (bater no ar)
    public AudioClip hitObjectSound;      // Som ao acertar objeto com tag específica
    public string hittableTag = "AlvoBatível"; // Defina a tag do objeto que pode receber hit

    private AudioSource audioSource;
    private bool canAttack = true;
    private bool hitSomething;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey) && canAttack)
        {
            if (animator != null)
                animator.SetTrigger("Attack");

            // Toca o som de ataque (swing) imediatamente
            if (attackSwingSound != null && audioSource != null)
                audioSource.PlayOneShot(attackSwingSound);

            canAttack = false;
            hitSomething = false;
            Invoke(nameof(DoAttack), attackDelay);
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    void DoAttack()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, destruivelLayer);
        foreach (Collider2D obj in hitObjects)
        {
            // Só aceita objetos com a tag definida
            if (obj.CompareTag(hittableTag))
            {
                JB_ObjetoDestruivel destruivel = obj.GetComponent<JB_ObjetoDestruivel>();
                if (destruivel != null)
                {
                    destruivel.TakeHit(damage);

                    // Só toca o som de hit na primeira colisão encontrada
                    if (!hitSomething && hitObjectSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(hitObjectSound);
                        hitSomething = true;
                    }
                }
            }
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