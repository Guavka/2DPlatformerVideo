using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveComp : MonoBehaviour
{
    [Range(0, 50)] [SerializeField] private float _speed = 5;
    [Range(0, 50)] [SerializeField] private float _jumpPower = 5;

    private Rigidbody2D _body;

    private bool _isOnGround = false;

    /*
     * Awake
     * Start
     * OnDestroy
     * 
     * OnEnable
     * OnDisable
     */
    /*
     * Update
     * FixedUpdate
     * LateUpdate
     */


    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");

        _body.velocity = new Vector2(horizontalInput * _speed, _body.velocity.y);

        if (Input.GetAxis("Jump") != 0 && _isOnGround)
        {
            _body.velocity = new Vector2(_body.velocity.x, _jumpPower);
            _isOnGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) _isOnGround = true;
    }
}
