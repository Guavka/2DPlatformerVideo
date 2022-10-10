using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(Animator))]
public class BulletController : MonoBehaviour
{
    private Rigidbody2D _body;
    private CircleCollider2D _collider;
    private Animator _animator;

    [Header("Params")]
    [Range(0, 50)] [SerializeField] private float _speed = 5;
    [Range(1, 30)] [SerializeField] private float _lifeTime = 3;

    [Header("Animation")]
    [SerializeField] private string _exploreTriggerName = "ExploreTrigger";

    private bool _isMove = true;
    private sbyte _direction = 1;
    private float _lifeTimeCounter;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable() => _lifeTimeCounter = _lifeTime;

    private void Update()
    {
        if (_isMove)
        {
            _body.velocity = new Vector2(_speed * _direction, _body.velocity.y);

            _lifeTimeCounter -= Time.deltaTime;
            if (_lifeTimeCounter <= 0) Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;

        _body.velocity = new Vector2(0, 0);
        _isMove = false;
        _collider.enabled = false;
        _animator.SetTrigger(_exploreTriggerName);
    }

    public void Deactivate()
    {
        var localScale = transform.localScale;
        transform.localScale = new Vector3(Mathf.Abs(localScale.x), localScale.y, localScale.z);
        _direction = 1;

        gameObject.SetActive(false);
    }

    public void Init(bool isLeft)
    {
        if (isLeft)
        {
            var localScale = transform.localScale;
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            _direction = -1;
        }
        _isMove = true;
    }
}
