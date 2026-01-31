using UnityEngine;

public class PlayerController : MonoBehaviour, ITickeable, IFixedTickeable
{
    [SerializeField]
    private Rigidbody2D player;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private ShootController shootController;
    [SerializeField]
    private MaskController maskController;

    private float horizontal;
    private float vertical;

    private Vector2 velocity;

    public Vector2 Velocity { get => velocity; }

    public void Tick()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            shootController.Shot();
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1))
        {
            maskController.Swap(true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            maskController.Swap(false);
        }
    }

    public void FixedTick()
    {
        velocity.x = horizontal;
        velocity.y = vertical;

        if(maskController.Current == "mask.speed")
        {
            velocity = velocity.normalized * maxSpeed * 3;
        }
        else
        {
            velocity = velocity.normalized * maxSpeed;
        }

        player.linearVelocity = velocity;

        if (horizontal != 0)
        {
            spriteRenderer.flipX = velocity.x > 0;
        }
    }
}
