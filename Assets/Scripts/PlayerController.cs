using UnityEngine;
using UnityEngine.UIElements;

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
    public float bounds;

    private float horizontal;
    private float vertical;

    private Vector2 velocity;

    public Vector2 Velocity { get => velocity; }

    public void Tick()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        /*if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            shootController.Shot();
        }*/

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
        if (player.transform.position.y >= bounds && vertical > 0)
        {
            vertical = 0;
        }

        if (player.transform.position.y <= -bounds && vertical < 0)
        {
            vertical = 0;
        }

        if (player.transform.position.x >= bounds && horizontal > 0)
        {
            horizontal = 0;
        }

        if(player.transform.position.x <= -bounds && horizontal < 0)
        {
            horizontal = 0;
        }

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
            if(horizontal > 0)
            {
                spriteRenderer.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                spriteRenderer.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
