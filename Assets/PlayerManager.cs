using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private bool isActivePlayer = true;

    private void Start()
    {
        if (isActivePlayer)
        {
            PlayerMovement playerMovement = transform.GetComponentInChildren<PlayerMovement>();
            playerMovement.SetFov(transform.GetComponentsInChildren<FieldOfView>());

            FindObjectOfType<CinemachineVirtualCamera>().Follow = playerMovement.transform;
        }
    }
}
