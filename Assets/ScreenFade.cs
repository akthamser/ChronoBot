using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ScreenFade : MonoBehaviour
{
    public static ScreenFade instance;
    private Animator _animator;
    private void Awake()
    {   
        instance = this;
        _animator = GetComponent<Animator>();
    }
    public void FadeIn(string namescene)
    {
        AudioManager.Instance.SceneIsloading = true;
        _animator.Play("Fadein");
        StartCoroutine(WaitThanLoadScene(namescene));

    }

    private IEnumerator WaitThanLoadScene(string scene)
    {
        yield return new WaitForSeconds(3);
        AudioManager.Instance.SceneIsloading = true;
        SceneManager.LoadScene(scene);

    }

    public void FadeOut() => _animator.Play("Fadeout");

 
   public void loadscenebyname(string scenename)
    {
        AudioManager.Instance.SceneIsloading = true;
        SceneManager.LoadScene(scenename);
    }


    public void reloadScene()
    {
        AudioManager.Instance.SceneIsloading = true;
        AudioManager.Instance.SoundSources.Clear();
        FadeIn(SceneManager.GetActiveScene().name);
    }


}
