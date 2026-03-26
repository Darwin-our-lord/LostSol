using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyBehavior : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    LayerMask layermask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position,transform.position) < 15)
        {
            bool hit = Physics.Raycast(transform.position, -(transform.position - player.transform.position).normalized, out RaycastHit hitInfo, 10);
            if (!hit) return;
            if (hitInfo.collider.gameObject.CompareTag("Player"))
            {
                agent.SetDestination(player.transform.position);
            }

        }
    }
}
