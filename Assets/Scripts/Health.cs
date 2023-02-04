using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class Health : NetworkBehaviour
{
    private int _maxHealth;
    [SerializeField]
    private NetworkVariable<int> _value = new (100);
    public NetworkVariable<int> Value => _value;

    public UnityEvent onDeath;
    public UnityEvent<HealthChangedEventArgs> onHealthChanged;
    private void Start()
    {
        _maxHealth = _value.Value;
        UpdateUiHealth(Value.Value);
    }

    void OnEnable()
    {
        // Subscribe for when Health value changes
        // This usually gets triggered when the server modifies that variable
        // and is later replicated down to clients
        
        Value.OnValueChanged += OnHealthChanged;
    }

    void OnDisable()
    {
        Value.OnValueChanged -= OnHealthChanged;
    }

    void OnHealthChanged(int oldValue, int newValue)
    {
        // Update UI, if this a client instance and it's the owner of the object
        UpdateUiHealth(newValue);
        Debug.LogFormat("{0} has {1} health!", gameObject.name, _value.Value);
    }

    public void UpdateUiHealth(int value)
    {
        if (IsOwner && IsClient)
        {
            //GameUI.Singletone.SetHealth(value);
            // TODO: Update UI code?
        }
    }

    public void TakeDamage(int amount)
    {
        //Health should be modified server-side only
        if (IsServer)
        {
            var oldValue = Value.Value;
            var newValue = oldValue - amount;
        
            onHealthChanged.Invoke(new HealthChangedEventArgs(oldValue, newValue, _maxHealth));
            
            if (newValue <= 0)
            {
                newValue = 0;
                
                onDeath.Invoke();
            }

            Value.Value = newValue;
        }
    }
}


public struct HealthChangedEventArgs
{
    public readonly int OldValue;
    public readonly int NewValue;
    public readonly int MaxHealth;

    public HealthChangedEventArgs(int oldValue, int newValue, int maxHealth)
    {
        OldValue = oldValue;
        NewValue = newValue;
        MaxHealth = maxHealth;
    }
}
