using UnityEngine;
using Mirror;

public class HealthSystem : NetworkBehaviour
{
    [SyncVar]
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

    [Command]
    public void CmdUpdateHelth(float _health)
    {
        Health = _health;
    }
}
