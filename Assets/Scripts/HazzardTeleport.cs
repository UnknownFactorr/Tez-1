using UnityEngine;

public class HazzardTeleport : MonoBehaviour
{
    public GameObject respawnPoint;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.position = respawnPoint.transform.position;
        }
    }
}
