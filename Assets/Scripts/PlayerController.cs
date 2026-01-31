using UnityEngine;

public class PlayerController : MonoBehaviour, ITickeable, IFixedTickeable
{
    [SerializeField]
    private Rigidbody2D player;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float maxSpeed;

    private float horizontal;
    private float vertical;

    private Vector2 velocity;

    public Vector2 Velocity { get => velocity; }

    public void Tick()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    public void FixedTick()
    {
        velocity.x = horizontal;
        velocity.y = vertical;

        velocity = velocity.normalized * maxSpeed;

        player.linearVelocity = velocity;

        if (horizontal != 0)
        {
            spriteRenderer.flipX = velocity.x > 0;
        }
    }
}
