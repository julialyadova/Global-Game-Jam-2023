using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Spear : NetworkBehaviour
{
    public Transform Head;
    public float HeadRaycastDistance = 0.4f;
    public int BaseDamage = 10;
    public float SpeedDamageMultiplier = 0.5f;

    private Rigidbody _rigidbody;
    private bool _hit;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = Head.position;
    }
    
    void FixedUpdate()
    {
        if (_hit)
            return;
        
        if (Physics.Raycast(Head.position, transform.TransformDirection(Vector3.forward), out var hitInfo, HeadRaycastDistance))
        {
            if(hitInfo.transform.CompareTag("DamageZone"))
                return;
            
            
            Hit(hitInfo);
        }

        if(_rigidbody.velocity != Vector3.zero)
            _rigidbody.rotation = Quaternion.LookRotation(_rigidbody.velocity);  
    }

    void Hit(RaycastHit hitInfo)
    {
        _hit = true;
        
        if (IsServer)
        {
            var health = hitInfo.collider.GameObject().GetComponent<Health>();
            if (health)
            {
                health.TakeDamage(BaseDamage + (int)(_rigidbody.velocity.magnitude * SpeedDamageMultiplier));
                Destroy(gameObject);
            }
        }
        
        _rigidbody.isKinematic = true;
    }
}
