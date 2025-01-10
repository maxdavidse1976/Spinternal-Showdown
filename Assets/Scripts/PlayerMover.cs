using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float _speed = 5f;

    void Update()
    {
        // Get input for horizontal and vertical axes
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Create a movement vector
        Vector3 movement = new Vector3(moveX, moveY, 0) * _speed * Time.deltaTime;

        // Move the player
        transform.position += movement;

    }
}
