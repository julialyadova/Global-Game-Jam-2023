using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject Leaves;

    public void onDeath()
    {
        Leaves.SetActive(false);
    }
}
