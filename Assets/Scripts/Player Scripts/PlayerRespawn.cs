using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{

    public Transform currentRespawnPoint;
    private RespawnController respawnController;

    private void Start()
    {
        respawnController = Object.FindFirstObjectByType<RespawnController>();
    }

    public void UpdateRespawnPoint(Transform newRespawnPoint)
    {
        currentRespawnPoint = newRespawnPoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RespawnController") && currentRespawnPoint != null)
        {
            respawnController.RespawnPlayer(currentRespawnPoint, transform);
        }
    }

}
