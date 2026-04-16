using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("PlayerStats")]
    [SerializeField]
    float maxPlayerHealth = 100;
    [SerializeField]
    float maxStamina = 100;

    [Header("TempPlayerStats")]
    [SerializeField]
    float playerHealth = 100;
    [SerializeField]
    public float currentStaminaLeft = 100;

    [Header("UI")]
    [SerializeField]
    GameObject deathUI;


    PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>(); //isn't used i don't know why i felt like including it here <3
    }
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
