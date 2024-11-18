using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public void RespawnPlayer(Transform respawnPoint, Transform player)
    {
        player.position = respawnPoint.position;
    }

}
