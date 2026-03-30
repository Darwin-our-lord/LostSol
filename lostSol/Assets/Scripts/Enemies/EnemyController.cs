using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float health = 20;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        animator.SetTrigger("Hit");
        if (health <= 0) Die();
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
