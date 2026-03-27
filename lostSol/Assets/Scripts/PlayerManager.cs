using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("PlayerStats")]
    [SerializeField]
    float playerHealth = 100;

    [Header("UI")]
    [SerializeField]
    GameObject deathUI;

    public void TakeDamage(float dmg)
    {
        playerHealth -= dmg;
        if (playerHealth <= 0) Die();
    }

    public void Die()
    {
        Time.timeScale = 0f;
        deathUI.SetActive(true);
    }

}
