using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public enum EnemyState
    {
        Chase,
        Idle,
        Attack
        
    }

    private float _speed = 2;
    private float _gravity = 5;
    private float _attackDelay = 1.5f;
    private float _nextAttack = -1f;

    Vector3 _velocity;

    [SerializeField]
    private EnemyState _currentState = EnemyState.Chase;

    private Transform _player;

    private Health _playerHealth;

    private CharacterController _controller;

    //reference to character controller

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.GetComponent<Health>();
        if (_player == null || _playerHealth == null)
        {
            Debug.LogError("Player Components are NULL");
        }

    }

    private void Update()
    {

        switch(_currentState)
        {
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Chase:
                CalculateMovement();
                break;

        }

    }


    void CalculateMovement()
    {
            if (_controller.isGrounded == true)
            {

                Vector3 direction = _player.position - transform.position;
                direction.Normalize();
                direction.y = 0f;
                transform.localRotation = Quaternion.LookRotation(direction);
                _velocity = direction * _speed;

            }

            _velocity.y -= _gravity;

            _controller.Move(_velocity * Time.deltaTime);

    }

    void Attack()
    {
        if (Time.time > _nextAttack)
        {
            if (_playerHealth != null)
            {
                _playerHealth.Damage(10);
                _nextAttack = Time.time + _attackDelay;
            }
        }
    }

    public void StartAttack()
    {
        _currentState = EnemyState.Attack;
    }

    public void EndAttack()
    {
        _currentState = EnemyState.Chase;
    }

}




