using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIController : MonoBehaviour
{
    public TextMeshProUGUI Time;



    private void Update()
    {
        Time.text = Utility.FormatTime((int)InGameEventsManager.Instance.Timer);
    }
}
