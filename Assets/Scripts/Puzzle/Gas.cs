using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    public float Range;
    public ParticleSystem gasparticles;
    public ParticleSystem gasPressurepartical;

    public bool WorkWithTime;
    public float OnTime;
    public float OffTime;

    private Vector3 GasPositionDefault;
    public bool ON_Off;

    void Start()
    {
        GasPositionDefault = gasparticles.transform.position;
        if (WorkWithTime)
            StartCoroutine(IGasCycle());

        StartCoroutine(Isoundcheck());
    }
    IEnumerator Isoundcheck()
    {
        yield return null;
        AudioManager.Instance.PlaySound(gameObject.name);
    }
    private IEnumerator IGasCycle()
    {
        while (WorkWithTime) { 
        TurnOn();
        yield return new WaitForSeconds(OnTime);
        TurnOff();
        yield return new WaitForSeconds(OffTime);
        }
    
    }

    public void TurnOff()
    { 
        ON_Off = false;
        AudioManager.Instance.StopSound(gameObject.name,0.4f);
    }
    public void TurnOn()
    { 
       ON_Off=true;
        AudioManager.Instance.PlaySound(gameObject.name);
    }
    public void TurnOff(float time)
    {
        
        StopAllCoroutines();
        StartCoroutine( WaitThanSwitchOn_Off(time, false));
        
    }
    public void TurnOn(float time)
    {   
        StopAllCoroutines();
       StartCoroutine( WaitThanSwitchOn_Off(time, true));
    }
    private IEnumerator WaitThanSwitchOn_Off(float time, bool On_Off)
    {
        yield return new WaitForSeconds(time);
        ON_Off = On_Off;
        if(On_Off)
        AudioManager.Instance.PlaySound(gameObject.name);
        else
            AudioManager.Instance.StopSound(gameObject.name, 0.4f);

    }
    void Update()
    {
        if (ON_Off)
        {
            
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            Physics.Raycast(ray, out hit, Range);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    //PlayerDead
                    MovmentController.Instance.Die();
                }


                if (hit.distance < transform.InverseTransformPoint(GasPositionDefault).z)
                    gasparticles.transform.position = hit.point;
                else
                    gasparticles.transform.position = GasPositionDefault;

                if (hit.distance < 0.4f && !hit.collider.CompareTag("Player"))
                {
                    gasparticles.Stop();
                    gasPressurepartical.Stop();
                }
                else
                {

                    if (gasparticles.isStopped) gasparticles.Play();
                    if (gasPressurepartical.isStopped) gasPressurepartical.Play();
                }

            }
            else
            {

                if (gasparticles.isStopped) gasparticles.Play();
                if (gasPressurepartical.isStopped) gasPressurepartical.Play();
                gasparticles.transform.position = GasPositionDefault;
            }
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
        }
        else
        {
            gasparticles.Stop();
            gasPressurepartical.Stop();
        }
        
       
    }
}
