using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponAnimationController : MonoBehaviour
{

    private Animator _animator;

    private int amountOfAmmo = 2;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    private void Start()
    {
        _animator.SetTrigger("Reload");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _animator.SetTrigger("Fire");

            amountOfAmmo--;

            if (amountOfAmmo < 0)
            {
                _animator.SetTrigger("Reload");
                amountOfAmmo = 2;
            }
        }
    }
}
