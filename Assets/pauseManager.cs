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
    Button _resumeButton;

    Button _QuitButton;


       VisualElement root ;


    Slider musicSlider;
    Slider soundSlider;


    void Awake()
    {
        if(instance==null)
        instance = this;

        _doc = GetComponent<UIDocument>();
          root = _doc.rootVisualElement;
              root.style.display = DisplayStyle.None; 




        _resumeButton = root.Q<Button>("resume");
        _QuitButton = root.Q<Button>("Quit");


      
        _resumeButton.clicked  += resume;
      
        _QuitButton.clicked += Quit;




        musicSlider= root.Q<Slider>(name: "music");
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



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& root.style.display != DisplayStyle.None)
        {
            resume();
        }

            
    }

    private void Quit(){
                Debug.Log("button clicked");

        ScreenFade.instance.FadeIn("MainMenuee");
        }

    private void resume(){
        Debug.Log("button clicked");
        root.style.display = DisplayStyle.None;
        Time.timeScale = 1;
        AudioManager.Instance.Resumeall();
    }
    private void Restart()
    {
        ScreenFade.instance.reloadScene();
    }

    

    public void pause(){
        print("paused");
          Time.timeScale = 0f;
                  root.style.display = DisplayStyle.Flex;

        musicSlider.value = AudioManager.Instance.MusicVolume;
        soundSlider.value = AudioManager.Instance.SoundVolume;

        AudioManager.Instance.pauseall();


    }


  
}
