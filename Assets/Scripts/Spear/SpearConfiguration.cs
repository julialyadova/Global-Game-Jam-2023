using UnityEngine;
using UnityEngine.Serialization;

public class SpearConfiguration : MonoBehaviour
{

    [FormerlySerializedAs("CenterOfAMass")] 
    public Vector3 centerOfAMass;

    void Start()
    {
        gameObject.GetComponent<Rigidbody>().centerOfMass = centerOfAMass;
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 45, ForceMode.Impulse);
    }
}
