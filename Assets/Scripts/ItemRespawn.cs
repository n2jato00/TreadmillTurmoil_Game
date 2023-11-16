using UnityEngine;

public class ItemRespawn : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Set this array in the Unity Editor.
    public float itemLifetime = 5f;
    public float createNewItemTime = 1f;
    public float distanceInFrontOfCamera = 1.6f; // Kuinka kaukana kameran edessä esine ilmestyy.
    public float heightAboveCamera = 0f; // Kuinka korkealla kameran yläpuolella esine ilmestyy.
    public float lateralOffsetFromCamera = 0.1f; // Kuinka kaukana sivusuunnassa kamerasta esine ilmestyy.

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
            Invoke("CreateNewItem", createNewItemTime);
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

        currentItem = Instantiate(randomItemPrefab, spawnPosition, Quaternion.identity);
        PauseManager.canThrow = true;
    }

    private void OnDestroy()
    {
        ThrowItem.OnBallThrown -= HandleThrowEvent;
    }
}
