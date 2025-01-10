using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossMover : MonoBehaviour
{
    [SerializeField] Vector2 _worldBoundsMin = new Vector2(-8f, -4.5f); // Min world bounds
    [SerializeField] Vector2 _worldBoundsMax = new Vector2(8f, 4.5f);   // Max world bounds
    [SerializeField] float _speed = 2f;       // Speed for smooth movement
    [SerializeField] float _lashOutSpeed = 10f;  // Speed for lashing out
    [SerializeField] float _lashOutCooldown = 3f; // Time between lash outs
    [SerializeField] float _smoothness = 1f; // How smooth does the boss move, smaller value means smoother movement

    Vector2 _targetPosition;
    Vector2 _startPosition;
    Transform _player;
    float _lashOutTimer = 0f;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomTargetPosition();
    }

    void Update()
    {
        // Handle smooth movement
        SmoothMovement();

        // Handle lash out logic
        _lashOutTimer -= Time.deltaTime;
        if (_lashOutTimer <= 0f)
        {
            LashOut();
            _lashOutTimer = _lashOutCooldown; // Reset the cooldown
        }
        // Smoothly move toward the target position
        transform.position = Vector2.Lerp(transform.position, _targetPosition, Time.deltaTime * _smoothness);

        // if we are getting close, set a new position
        if (Vector2.Distance(transform.position, _targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    void SmoothMovement()
    {
        // Move the boss toward the target position
        float step = _speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, step);

        // Check if close to the target and set a new one if needed
        if (Vector2.Distance(transform.position, _targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    void SetRandomTargetPosition()
    {
        // Generate a random target position within world bounds
        float randomX = Random.Range(_worldBoundsMin.x, _worldBoundsMax.x);
        float randomY = Random.Range(_worldBoundsMin.y, _worldBoundsMax.y);
        _targetPosition = new Vector2(randomX, randomY);
    }

    void LashOut()
    {
        if (_player == null) return;

        // Dash toward the player's current position
        Vector2 directionToPlayer = (_player.position - transform.position).normalized;
        Vector2 lashOutTarget = (Vector2) transform.position + directionToPlayer * 5f; // Dash distance

        // Clamp the lash-out target within the world bounds
        lashOutTarget.x = Mathf.Clamp(lashOutTarget.x, _worldBoundsMin.x, _worldBoundsMax.x);
        lashOutTarget.y = Mathf.Clamp(lashOutTarget.y, _worldBoundsMin.y, _worldBoundsMax.y);

        // Set the boss's position to the lash-out target
        transform.position = Vector2.MoveTowards(transform.position, lashOutTarget, _lashOutSpeed * Time.deltaTime);
    }
}
