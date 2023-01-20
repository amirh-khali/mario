using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 velocity;
    private float inputAxis;

    public float speed = 10f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, speed * inputAxis, speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Vector2 position = _rigidbody.position;
        position += velocity * Time.deltaTime;

        _rigidbody.MovePosition(position);
    }
}
