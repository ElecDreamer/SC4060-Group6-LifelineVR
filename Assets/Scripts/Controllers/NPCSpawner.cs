using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [System.Serializable]
    public class NPCSpawnData
    {
        public PathFollower prefab;
        public int spawnCount;
        public PathSegment[] pathSegments;
    }

    public List<NPCSpawnData> npcSpawnDataList;

    private PathSegment[] cachedPathSegments;

    public void Start()
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
                SpawnNpc(spawnData.prefab, spawnData.pathSegments);
            }
        }
    }

    private void SpawnNpc(PathFollower prefab, PathSegment[] pathSegments = null)
    {
        // Randomly select a path segment from the provided or cached segments
        PathFollower npc = Instantiate(prefab);
        PathSegment[] pathSegmentsToUse = (pathSegments == null || pathSegments.Length == 0) ? cachedPathSegments : pathSegments;
        PathSegment randomSegment = pathSegmentsToUse[Random.Range(0, pathSegmentsToUse.Length)];
        npc.GetComponent<VirtualTransform>().position = randomSegment.GetComponent<VirtualTransform>().position;
        npc.currentPathSegment = randomSegment;
    }
}
