using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(Destr), 1f);
    }
    
    private void Destr()
    {
        Destroy(gameObject);
    }

}
