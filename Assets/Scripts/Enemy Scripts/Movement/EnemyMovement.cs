using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    //private Animator anime;
    private Transform curPoint;
    public float speed;
    public SpriteRenderer spr;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        //anime = GetComponent<Animator>();
        curPoint = pointB.transform;

    }


    void Update()
    {

        Vector2 point = curPoint.position - transform.position;

        if (curPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        }

        if (Vector2.Distance(transform.position, curPoint.position) < 0.5f && curPoint == pointB.transform)
        {
            spr.flipX = true;
            curPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, curPoint.position) < 0.5f && curPoint == pointA.transform)
        {
            spr.flipX = false;
            curPoint = pointB.transform;
        }

    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);

    }
}
