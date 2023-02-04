using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    private Camera _camera;
    public void Update()
    {
        var transform1 = Camera.main.transform;
        var rotation = transform1.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
}
