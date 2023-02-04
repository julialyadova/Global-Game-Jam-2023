using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    private Camera _camera;
    public void Update()
    {
        if (_camera == null)
        {
            FindCamera();
            return;
        }

        var transform1 = _camera.transform;
        var rotation = transform1.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    private void FindCamera()
    {
        _camera = Camera.main;
    }
}
