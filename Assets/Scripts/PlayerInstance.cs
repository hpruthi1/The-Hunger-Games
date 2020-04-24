using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PlayerInstance : NetworkBehaviour
{
    public PlayerSpawner PlayerSpawner;
    public static event Action <PlayerInstance> onPlayerInstance;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        PlayerSpawner = GetComponent<PlayerSpawner>();
        onPlayerInstance?.Invoke(this);
    }

}
