using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WildPokemon : MonoBehaviour
{
    

    [SerializeField] private WildPokemonSO data;

    [SerializeField] private float wanderRange = 6f;

    private NavMeshAgent agent;

    private Transform center;

    private float timeToDeSpawn = 30.0f;
    private float timeToMove = 5.0f;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        center = transform;

        timeToDeSpawn = Random.Range(25.0f, 30.0f);
        timeToMove = Random.Range(4.0f, 8.0f);
        timeToMove = 1f;

        StartCoroutine(RadomMovement());


        Destroy(gameObject, timeToDeSpawn);
    }

    public WildPokemonSO getData()
    {
        return data;
    }

    private IEnumerator RadomMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToMove);

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                Vector3 point;

                if (RandomPoint(center.position, wanderRange, out point))
                {
                    agent.SetDestination(point);

                    timeToMove = Random.Range(4.0f, 8.0f);
                }
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }
}
