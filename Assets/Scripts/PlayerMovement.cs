using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : NetworkBehaviour
{

    [SerializeField] private NavMeshAgent agent;

    private Camera mainCamera;

    #region Server

    [Command]
    private void CmdMove(Vector3 position)
    {
        if(!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) return;

        RpcSetMove(position);     
    }

    #endregion

    #region Client

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        mainCamera = Camera.main;
    }

    [ClientCallback]
    private void Update()
    {
        if (!isOwned) return;

        if (!Input.GetMouseButtonDown(1)) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return;

        CmdMove(hit.point);

    }

    [ClientRpc]
    private void RpcSetMove(Vector3 position)
    {
        agent.SetDestination(position);
    }


    #endregion
}
