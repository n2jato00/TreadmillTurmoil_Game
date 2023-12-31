using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawn : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Set this array in the Unity Editor.
    public float itemLifetime = 5f;
    public float distanceInFrontOfCamera = 1.6f; // How far in front of the camera the object appears.
    public float heightAboveCamera = 0f; // How high above the camera the object appears.
    public float lateralOffsetFromCamera = 0.1f; // How far to the side from the camera the object appears.


    private GameObject currentItem;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        ThrowItem.OnBallThrown += HandleThrowEvent;

        // Create the initial item
        CreateNewItem();
    }

    private void HandleThrowEvent()
    {
        if (currentItem != null)
        {
            Destroy(currentItem, itemLifetime);
            StartCoroutine(ItemRespawnDelay(0.25f));
        }
    }


    private void CreateNewItem()
    {
        GameObject randomItemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

        // Determine the spawn position
        Vector3 spawnPosition = mainCamera.transform.position +
                                mainCamera.transform.forward * distanceInFrontOfCamera +
                                Vector3.up * heightAboveCamera +
                                mainCamera.transform.right * lateralOffsetFromCamera;

        currentItem = Instantiate(randomItemPrefab, spawnPosition, randomItemPrefab.transform.rotation);
        PauseManager.canThrow = true;
    }

    private void OnDestroy()
    {
        ThrowItem.OnBallThrown -= HandleThrowEvent;
    }

    private IEnumerator ItemRespawnDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Set the "Sprint" parameter to false after the delay
        CreateNewItem();
    }
}
