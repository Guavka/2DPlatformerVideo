using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMoveComp : MonoBehaviour
{
    [Header("Params")]
    [Range(0, 50)] [SerializeField] private float _speed = 5;
    [Range(0, 50)] [SerializeField] private float _jumpPower = 5;

    [Header("Animation")]
    [SerializeField] private string _jumpTriggerName = "JumpTrigger";
    [SerializeField] private string _isMoveBoolName = "IsMove";

    private Rigidbody2D _body;
    private Animator _animator;

    private bool _isLeft = false;
    private bool _isOnGround = false;


    public bool IsOnGround
    {
        get => _isOnGround;
    }

    public bool IsLeft
    {
        get => _isLeft;
    }


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
        _animator = GetComponent<Animator>();
    }

    private void CheckJump()
    {
        if (Input.GetAxis("Jump") != 0 && _isOnGround)
        {
            _body.velocity = new Vector2(_body.velocity.x, _jumpPower);
            _isOnGround = false;

            _animator.SetTrigger(_jumpTriggerName);
        }
    }
    private void CheckFip(float horizontalInput)
    {
        var isFlipLeft = horizontalInput < -0.01f && !_isLeft;
        var isFlipRight = horizontalInput > 0.01f && _isLeft;

        if (isFlipLeft || isFlipRight)
        {
            _isLeft = !_isLeft;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void CheckMove()
    {
        var horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput == 0)
        {
            _animator.SetBool(_isMoveBoolName, false);
            return;
        }

        _body.velocity = new Vector2(horizontalInput * _speed, _body.velocity.y);
        _animator.SetBool(_isMoveBoolName, true);

        CheckFip(horizontalInput);
    }

    private void Update()
    {
        CheckMove();
        CheckJump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) _isOnGround = true;
    }
}
