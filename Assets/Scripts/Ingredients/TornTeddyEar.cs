using UnityEngine;

public class TornTeddyEar : MonoBehaviour
{
    [SerializeField] private string itemName = "Torn Teddy Ear";
    [SerializeField] private GameObject cureUI; // Optional: assign in Inspector

    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected || !other.CompareTag("Player")) return;

        isCollected = true;

        // Add to inventory
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            inventory.AddItem(itemName);
        }

        // Update all zombies
        Zombie[] allZombies = Object.FindObjectsByType<Zombie>(FindObjectsSortMode.None);

        foreach (Zombie z in allZombies)
        {
            z.SetTeddyEar(true);
        }

        // Optional: enable cure UI
        if (cureUI != null)
        {
            cureUI.SetActive(true);
        }

        Debug.Log("Torn Teddy Ear collected. Zombies will hesitate now.");
        Destroy(gameObject);
    }
}
