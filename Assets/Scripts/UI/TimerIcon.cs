using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerIcon : MonoBehaviour
{
  
    public float value;
    public GameObject redicon, orengeicon, greenicon;
    [HideInInspector]
    public bool on;
    private void OnEnable()
    {

    }
    public void StartCount(float time)
    {
        StopAllCoroutines();
        StartCoroutine(IStartCount(time));
    }

    private IEnumerator IStartCount(float time)
    {
        
            on = true;
            value = 0;
        while (value <= 1)
        {
             value += Time.deltaTime/time;
            yield return null;
        }

        on = false;


    }
    void Update()
    {
        Camera camera = Camera.main;
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        
        if (!on)
        {
            redicon.SetActive(false);
            orengeicon.SetActive(false);
            greenicon.SetActive(false);
        }
        else if (value < 0.33 )
        {
            redicon.SetActive(true);
            orengeicon.SetActive(false);
            greenicon.SetActive(false);
        }
        else if (value < 0.85)
        {
            redicon.SetActive(false);
            orengeicon.SetActive(true);
            greenicon.SetActive(false);
        }
        else
        {
            redicon.SetActive(false);
            orengeicon.SetActive(false);
            greenicon.SetActive(true);
        }

      
        
        


    }
}
