using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject wep;

    NavMeshAgent agent;
    GameObject player;
    LayerMask layermask;

    Animator wepAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");

        wepAnimator =  wep.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 2)
        {
            bool hit = Physics.Raycast(transform.position, -(transform.position - player.transform.position).normalized, out RaycastHit hitInfo, 1);
            if (!hit) return;
            if (hitInfo.collider.gameObject.CompareTag("Player"))
            {
                wepAnimator.SetTrigger("Swing");
            }
        }
        else if (Vector3.Distance(player.transform.position,transform.position) < 15)
        {
            bool hit = Physics.Raycast(transform.position, -(transform.position - player.transform.position).normalized, out RaycastHit hitInfo, 15);
            if (!hit) return;
            if (hitInfo.collider.gameObject.CompareTag("Player"))
            {
                Vector3 flatToPlayer = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);

                agent.SetDestination(player.transform.position);
            }

        }
    }
}
