using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class SpearConfiguration : MonoBehaviour
{
    public Transform Head;
    public float HeadRaycastDistance = 1;

    private Rigidbody _rigidbody;
    private bool _hit;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = Head.position;
    }
    
    void FixedUpdate()
    {
        if (Physics.Raycast(Head.position, transform.TransformDirection(Vector3.forward), out var hitInfo, HeadRaycastDistance))
        {
            Hit();
        }

        if(!_hit && _rigidbody.velocity != Vector3.zero)
            _rigidbody.rotation = Quaternion.LookRotation(_rigidbody.velocity);  
    }

    void Hit()
    {
        _hit = true;
        _rigidbody.isKinematic = true;
    }
}
