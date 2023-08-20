using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    public static SpawnNPC instance;

    [SerializeField] private LayerMask layersNPCCannotSpawnOn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //public LayerMask solidObjects;

    public void SpawnNPCs(Collider2D spawnableAreaCollider, GameObject[] npcs, List<int> indexes)
    {
        for(int i = 0; i < indexes.Count; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition(spawnableAreaCollider);
            if (spawnPosition != Vector2.zero)
            {
                int randomIndexNPC = Random.Range(0, npcs.Length);
                GameObject spawnedNPC = Instantiate(npcs[randomIndexNPC], spawnPosition, Quaternion.identity);
                NPCController npcController = spawnedNPC.GetComponent<NPCController>();
                npcController.SetQuizIndex(indexes[i]);
            }
        }
    }

    private Vector2 GetRandomSpawnPosition(Collider2D spawnableAreaCollider)
    {
        Vector2 spawnPosition = Vector2.zero;
        bool isSpawnPosValid = false;

        int attemptCount = 0;
        int maxAttempts = 200;

        //int layerToNotSpawnOn = LayerMask.NameToLayer("SolidObjects");
        while (!isSpawnPosValid && attemptCount < maxAttempts)
        {
            spawnPosition = GetRandomPointInCollider(spawnableAreaCollider);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 1f);

            bool isInvalidCollision = false;
            foreach(Collider2D collider in colliders)
            {
                if (((1 << collider.gameObject.layer) & layersNPCCannotSpawnOn) != 0)
                {
                    isInvalidCollision = true;
                    break;
                }
            }

            if (!isInvalidCollision)
            {
                isSpawnPosValid = true;
            }

            attemptCount++;
        }

        if (!isSpawnPosValid)
        {
            return Vector2.zero;
        }

        return spawnPosition;
    }

    private Vector2 GetRandomPointInCollider(Collider2D collider, float offset = 1f)
    {
        Bounds collBounds = collider.bounds;

        Vector2 minBounds = new Vector2(collBounds.min.x + offset, collBounds.min.y + offset);
        Vector2 maxBounds = new Vector2(collBounds.max.x - offset, collBounds.max.y - offset);

        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);

        return new Vector2(randomX, randomY);
    }
}
