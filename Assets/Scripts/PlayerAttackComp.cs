using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMoveComp))]
public class PlayerAttackComp : MonoBehaviour
{
    private Animator _animator;
    private PlayerMoveComp _moveComp;

    [Header("Params")]
    [Range(0.1f, 10)] [SerializeField] private float _attackCalldown = 1;
    [SerializeField] private BulletController _bulletPrefab;
    [SerializeField] private Vector3 _shootPosition;

    [Header("Animation")]
    [SerializeField] private string _isAttackTriggerName = "AttackTrigger";

    private float _counter = 0;
    private Transform _fireballHolder;
    private Transform _shootPoint;

    private List<BulletController> _bulletPool;

    private void Awake()
    {
        _bulletPool = new List<BulletController>();

        _animator = GetComponent<Animator>();
        _moveComp = GetComponent<PlayerMoveComp>();

        _fireballHolder = new GameObject("FireballHolder").transform;

        CreateShootPoint();
    }

    private void Update()
    {
        _counter += Time.deltaTime;

        CheckAttack();
    }

    private void CreateShootPoint()
    {
        var go = new GameObject("ShootPoint");
        go.transform.parent = transform;
        go.transform.localPosition = _shootPosition;
        _shootPoint = go.transform;
    }

    private void CheckAttack()
    {
        if (Input.GetAxis("Fire1") != 0 && _counter >= _attackCalldown)
        {
            _counter = 0;
            _animator.SetTrigger(_isAttackTriggerName);

            var bullet = Instantiate(_bulletPrefab, _fireballHolder);
            bullet.Init(_moveComp.IsLeft);
        }
    }
}
