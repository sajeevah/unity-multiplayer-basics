using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private string displayName = "Missing Name";

    [SyncVar]
    [SerializeField] private Color displayColor;

    [Server]
    public void SetDisplayName(string displayName)
    { 
        this.displayName = displayName;
    }

    [Server]
    public void SetDisplayColor(Color color)
    {
        displayColor = color;
    }
}
