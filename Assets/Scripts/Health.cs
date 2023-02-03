using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField]
    private NetworkVariable<int> _value = new (100);
    public NetworkVariable<int> Value => _value;

    private void Start()
    {
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
            GameUI.Singletone.SetHealth(value);
            // TODO: Update UI code?
        }
    }

    public void TakeDamage(int amount)
    {
        //Health should be modified server-side only
        if (IsServer)
        {
            Value.Value -= amount;
        
            if (Value.Value <= 0)
            {
                Value.Value = 0;
            }
        }
    }
}
