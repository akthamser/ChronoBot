using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RoomsController : MonoBehaviour
{


    public static RoomsController Instance;

    public Room[] Rooms;
     public Room CurrentRoom;

    [Header("LostSignalEffect")]
    public VideoPlayer LostSignalEffect;
    public float MinSound;
    public float MaxSound;
    public float MinAlpha;
    public float MaxAlpha;
    public float BlendSpeed = 2;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        StartRoom();
    }

    private void StartRoom()
    {
        for (int i = 0; i < Rooms.Length; i++)
        {
            Rooms[i].RoomVCam.Priority = 0;
        }

        CurrentRoom.RoomVCam.Priority = 1;
    }

    private IEnumerator ActivateRoomCoroutine;
    public void ActivateRoom(Room room)
    {
        if (room == CurrentRoom)
            return;


        if(ActivateRoomCoroutine != null)
            StopCoroutine(ActivateRoomCoroutine);

         ActivateRoomCoroutine = IActivateRoom(room);
         StartCoroutine(ActivateRoomCoroutine);

    }


    public IEnumerator IActivateRoom(Room room)
    {
        LostSignalEffect.Play();
        float LerpValue = 0;
        while(LerpValue < 1)
        {
            LerpValue += Time.deltaTime * BlendSpeed;
            LostSignalEffect.SetDirectAudioVolume(0, Mathf.Lerp(MinSound, MaxSound,LerpValue));

            LostSignalEffect.targetCameraAlpha = Mathf.Lerp(MinAlpha, MaxAlpha, LerpValue);
            yield return null;
        }

        CurrentRoom = room;
        for (int i = 0; i < Rooms.Length; i++)
        {
            Rooms[i].RoomVCam.Priority = 0;
        }

        room.RoomVCam.Priority = 1;

        LerpValue = 0;
        while (LerpValue < 1)
        {
            LerpValue += Time.deltaTime * BlendSpeed;
            LostSignalEffect.SetDirectAudioVolume(0, Mathf.Lerp(MaxSound, 0, LerpValue));

            LostSignalEffect.targetCameraAlpha = Mathf.Lerp(MaxAlpha, 0, LerpValue);
            yield return null;
        }
    }
}
