using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Singletone { get; private set; }
    
    [SerializeField]
    private TMP_Text Health;

    public GameUI()
    {
        Singletone = this;
    }

    public void SetHealth(int value)
    {
        Health.text = value.ToString();
    }
}
