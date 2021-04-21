using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float _speed = 2;
    private float _gravity = 5;

    Vector3 _velocity;

    private Transform _player;

    private CharacterController _controller;

    //reference to character controller

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //sheck if grounded
        //calculate direction (vector) = destination(player or target) - source (source/transform)
        //calculate velocity = direction * speed

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
}
