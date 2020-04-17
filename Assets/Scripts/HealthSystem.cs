using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float Health = 100;
  
    public float healthincrease(float _health)
    {
        Health += _health;
        Health = Mathf.Clamp(Health, 0, 100);
        return Health;
    }
    public float healthDecrease(float _health)
    {
        Health -= _health;
        Health = Mathf.Clamp(Health, 0, 100);
        return Health;
    }
}
