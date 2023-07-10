using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GotoMenuOnVideoComplete : MonoBehaviour
{
    public VideoClip clip;
    void Start()
    {
        StartCoroutine(Gotomenu());
    }
    private void Update()
    {


    }
    private IEnumerator Gotomenu()
    {
        yield return new WaitForSeconds((float)clip.length);
        SceneManager.LoadScene("Mainmenuee");
    }

}
