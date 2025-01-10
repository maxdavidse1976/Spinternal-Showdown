using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
    }
}
