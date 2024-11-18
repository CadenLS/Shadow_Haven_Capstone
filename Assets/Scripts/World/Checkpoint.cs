using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public BoxCollider2D trigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Checkpoint Found");

            PlayerRespawn playerRespawn = collision.GetComponent<PlayerRespawn>();
            if (playerRespawn != null)
            {
                playerRespawn.UpdateRespawnPoint(transform);
                Debug.Log($"Respawn set to: {transform.position}");
            }
        }
    }

}
