using System.Collections;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    [SerializeField]
    float attackDmg = 5;
    [SerializeField]
    float attackDelayTime = 0.6f;
    [SerializeField]
    float staminaPrAttack = 10;


    Animator wepAnimator;

    bool waitingForDelayTime = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wepAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().TakeDamage(attackDmg);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !waitingForDelayTime)
        {
            wepAnimator.SetTrigger("Swing");
            waitingForDelayTime=true;
            StartCoroutine(WaitDelayTime());
        }
    }
    IEnumerator WaitDelayTime()
    {
        yield return new WaitForSeconds(attackDelayTime);
        waitingForDelayTime=false;
    }


}
