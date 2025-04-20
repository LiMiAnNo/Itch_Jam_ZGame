using UnityEngine;

public class MemoryShard : MonoBehaviour
{
    [SerializeField] private string itemName = "Memory Shard";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            inventory.AddItem(itemName);
        }

        Debug.Log("Memory Shard picked up.");
        Destroy(gameObject);
    }
}
