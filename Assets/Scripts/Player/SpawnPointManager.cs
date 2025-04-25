using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class SpawnPointManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;

    private void Awake()
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            // Buscar automáticamente puntos de spawn si no se asignan manualmente
            spawnPoints = new List<Transform>();
            foreach (Transform child in transform)
            {
                spawnPoints.Add(child);
            }
        }
    }

    public Vector3 GetRandomSpawnPoint()
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return Vector3.zero;
        }

        int index = Random.Range(0, spawnPoints.Count);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPoints[index].position, out hit, 10f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            Debug.LogWarning("Spawn point not valid on NavMesh!");
            return spawnPoints[index].position;
        }
    }
}
