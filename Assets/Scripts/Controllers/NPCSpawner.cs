using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [System.Serializable]
    public class NPCSpawnData
    {
        public PathFollower prefab;
        public int spawnCount;
    }

    public List<NPCSpawnData> npcSpawnDataList;

    private PathSegment[] cachedPathSegments;

    private void Start()
    {
        CachePathSegments();
        SpawnNpcs();
    }

    private void CachePathSegments()
    {
        cachedPathSegments = FindObjectsOfType<PathSegment>();
        if (cachedPathSegments.Length == 0)
        {
            Debug.LogWarning("No PathSegments found in the scene.");
        }
    }

    private void SpawnNpcs()
    {
        foreach (var spawnData in npcSpawnDataList)
        {
            for (int i = 0; i < spawnData.spawnCount; i++)
            {
                SpawnNpc(spawnData.prefab);
            }
        }
    }

    private void SpawnNpc(PathFollower prefab)
    {
        PathFollower npc = Instantiate(prefab);
        PathSegment randomSegment = cachedPathSegments[Random.Range(0, cachedPathSegments.Length)];
        npc.GetComponent<VirtualTransform>().position = randomSegment.GetComponent<VirtualTransform>().position;
        npc.currentPathSegment = randomSegment;
    }
}
