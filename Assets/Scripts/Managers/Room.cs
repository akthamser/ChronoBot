using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Room : MonoBehaviour
{
    public CinemachineVirtualCamera RoomVCam;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RoomsController.Instance.ActivateRoom(this);
            print(true);
        }
    }
}
