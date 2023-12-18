using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject KillerPrefab;
    [SerializeField] private GameObject SurvivorPrefab;

    [ServerRpc(RequireOwnership = false)]
    public void SpawnPLayerServerRpc(ulong clientId, int prefabId)
    {
        GameObject newPlayer;
        if (prefabId == 0)
        {
            newPlayer = Instantiate(KillerPrefab);
        }
        else
        {
            newPlayer = Instantiate(SurvivorPrefab);
        }

        NetworkObject netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId, true);
    }
}
