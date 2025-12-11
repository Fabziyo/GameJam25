using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [Header("Bewegung")]
    public float speed = 3f;
    public Vector2 bounds = new Vector2(2f, 1f);

    private Vector3 startPos;
    private Rigidbody2D rb;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        startPos = transform.position;

        
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void FixedUpdate()
    {
        
        float x = Mathf.PingPong(Time.time * speed, bounds.x * 2f) - bounds.x;
        float y = Mathf.PingPong(Time.time * speed * 0.7f, bounds.y * 2f) - bounds.y;

        Vector2 targetPos = (Vector2)(startPos + new Vector3(x, y, 0f));

        
        rb.MovePosition(targetPos);
    }
}
