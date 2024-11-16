using System.Collections;
using UnityEngine;

public class PassThrough : MonoBehaviour
{

    private new Collider2D collider;
    private bool playerOn;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {

        if (playerOn && Input.GetAxisRaw("Vertical") < 0)
        {
            collider.enabled = false;
            StartCoroutine(EnableCollider());
        }

    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
    }

    private void SetPlayerOnPLatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            playerOn = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SetPlayerOnPLatform(other, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        SetPlayerOnPLatform(other, true);
    }

}
