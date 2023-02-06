using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCameraHandler : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetBool("Aim", Input.GetKey(KeyCode.Mouse1));
    }
}
