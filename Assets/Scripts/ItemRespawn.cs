using UnityEngine;

public class ItemRespawn : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Set this array in the Unity Editor.
    public float itemLifetime = 5f;
    public float createNewItemTime = 2f;
    public float distanceInFrontOfCamera = 1.6f; // How far in front of the camera the object appears.
    public float heightAboveCamera = 0f; // How high above the camera the object appears.
    public float lateralOffsetFromCamera = 0.1f; // How far to the side from the camera the object appears.


    private GameObject currentItem;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        ThrowItem.OnBallThrown += HandleThrowEvent;
        BodyPartHitDetection.OnBodyPartHit += HandleHitEvent;

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

    private void HandleHitEvent(string bodyPart, Rigidbody rb)
    {
        if (currentItem != null)
        {
            CancelInvoke("CreateNewItem");
            // change item layer after hit
            currentItem.layer = 6;
        }
        CreateNewItem();
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
        BodyPartHitDetection.OnBodyPartHit -= HandleHitEvent;
        ThrowItem.OnBallThrown -= HandleThrowEvent;
    }
}
