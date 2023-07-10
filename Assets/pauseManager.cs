using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class pauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static pauseManager instance;
    UIDocument _doc;
    Button _resumeButton;Button _restartButton;

    Button _QuitButton;


       VisualElement root ;


    Slider musicSlider;
    Slider soundSlider;


    void Awake()
    {
        if(instance==null)
        instance = this;


        Paused = false;
        _doc = GetComponent<UIDocument>();
          root = _doc.rootVisualElement;
              root.style.display = DisplayStyle.None; 




        _resumeButton = root.Q<Button>("resume");
        _QuitButton = root.Q<Button>("Quit");
        _restartButton = root.Q<Button>("restart");


        _resumeButton.clicked  += resume;
      
        _QuitButton.clicked += Quit;

        _restartButton.clicked += Restart;


        musicSlider = root.Q<Slider>(name: "music");
         soundSlider = root.Q<Slider>("sounds");



        musicSlider.RegisterValueChangedCallback(evt =>
{
    AudioManager.Instance.MusicVolumeChanged(evt.newValue);
});
        soundSlider.RegisterValueChangedCallback(evt =>
{
    AudioManager.Instance.SoundVolumeChanged(evt.newValue);
});
    

        
    }


    private bool Paused = false;
    private void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.Escape) && Paused)
        {
            resume();
        }
        else if (Input.GetKeyDown(KeyCode.Escape)&&!Paused)
        {
            pause();
        }

    }

    private void Quit(){
                Debug.Log("button clicked");

        ScreenFade.instance.FadeIn("MainMenuee");
        resume();
        }

    private void resume(){
        Paused = false;
        Debug.Log("button clicked");
        root.style.display = DisplayStyle.None;
        Time.timeScale = 1;
        AudioManager.Instance.Resumeall();
    }
    private void Restart()
    {
        ScreenFade.instance.reloadScene();
        resume();
    }

    

    public void pause(){
          Time.timeScale = 0f;
          root.style.display = DisplayStyle.Flex;
        print(root.style.display);
        musicSlider.value = AudioManager.Instance.MusicVolume;
        soundSlider.value = AudioManager.Instance.SoundVolume;

        AudioManager.Instance.pauseall();
        Paused = true;

    }

    private IEnumerator delay()
    {
        yield return null;
        Paused = true;
    }

  
}
