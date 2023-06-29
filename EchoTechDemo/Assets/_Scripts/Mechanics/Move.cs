using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private InputHandler _input = null;
    [SerializeField, Range(0f,100f)] private float _maxSpeed = 4f, _maxAcceleration = 35f, _maxAirAcceleration = 20f;

    private Vector2 _direction, _desiredVelocity, _velocity;
    private Rigidbody2D _rigidbody;
    private Ground _ground;

    private float _maxSpeedChange, _acceleration;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
    }

    private void Update()
    {
        _direction.x = _input.RetrieveMoveInput();
        Debug.Log(_ground.Friction);
        _desiredVelocity = new Vector2(_direction.x,0) * Mathf.Max(_maxSpeed - _ground.Friction, 0);
    }

    private void FixedUpdate()
    {
        _isGrounded = _ground.IsGrounded;
        _velocity = _rigidbody.velocity;
        _acceleration = _isGrounded ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

        _rigidbody.velocity = _velocity;
    }
}
