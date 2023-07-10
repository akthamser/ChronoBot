using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvasController : MonoBehaviour
{
    public static WorldCanvasController Instance;
    public Camera MainCamera;
    private void Awake()
    {
        Instance = this;
    }

    private void LateUpdate()
    {
        this.transform.rotation = MainCamera.transform.rotation;
    }
}
