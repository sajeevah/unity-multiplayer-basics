using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNameText;
    [SerializeField] private Renderer displayColorRenderer;

    [SyncVar(hook = nameof(HandleDisplayNameUpdate))]
    [SerializeField] private string displayName = "Missing Name";

    [SyncVar(hook = nameof(HandleDisplayColorUpdate))]
    [SerializeField] private Color displayColor;

    #region Server

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

    [Command]
    private void CmdSetDisplayName(string displayName)
    {
        SetDisplayName(displayName);

        RpcSetDisplayName(displayName);
    }

    #endregion

    #region Client

    private void HandleDisplayColorUpdate(Color oldColor, Color newColor)
    {
        displayColorRenderer.material.SetColor("_BaseColor", newColor);
    }

    private void HandleDisplayNameUpdate(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    [ClientRpc]
    private void RpcSetDisplayName(string newName)
    {
        Debug.Log($"new name set by client is {newName}");
    }

    [ContextMenu("Set My Name")]
    public void SetMyname()
    {
        CmdSetDisplayName("My New Name");
    }

    #endregion

}