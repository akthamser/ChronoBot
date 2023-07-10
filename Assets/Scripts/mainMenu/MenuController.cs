using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public static MenuController instance;
    UIDocument _doc;
    Button _playButton;
    Button _settingsButton;

    Button _QuitButton;


    Button _creditButton;


    public Button _muteButton;





    public Sprite muteSprite;
    public Sprite unmuteSprite;




    Button chernoubelChapter;
     Button  titanicChapter;
      Button    hiroushimaChapter ;
      Button  miroChapter;





    public VisualTreeAsset settingTemplate;
    public VisualTreeAsset creditsTemplate;
    public VisualTreeAsset chaptersTemplate;
    public VisualElement creditsContent;
    public VisualElement leftSection;
    public VisualElement settingsContent;

    public VisualElement mainContent;

    public TemplateContainer chaptersContent;

    public VisualElement focusedElement;


    VisualElement buttonWrapper;

       VisualElement root ;


    Slider musicSlider;
    Slider soundSlider;

    void Awake()
    {
        if(instance == null)
        instance = this;

        _doc = GetComponent<UIDocument>();
          root = _doc.rootVisualElement;





        buttonWrapper = root.Q<VisualElement>("buttons");
        _playButton = root.Q<Button>("Start");
        _settingsButton = root.Q<Button>("Settings");
        _creditButton = root.Q<Button>("Credits");
        _QuitButton = root.Q<Button>("Quit");
        leftSection = root.Q<VisualElement>("leftSec");
        _muteButton = root.Q<Button>("mute");

       

        _playButton.clicked  += Play;
        _settingsButton.clicked += Settings;
        _creditButton.clicked += credits;
        _muteButton.clicked += muteClicked;
        _QuitButton.clicked += Quit;

        creditsContent = creditsTemplate.CloneTree();

        var backButton = creditsContent.Q<Button>("back");
        backButton.RegisterCallback<MouseEnterEvent>((evt) => AudioManager.Instance.PlaySound("Select"));
        backButton.clicked += () => {
            buttonWrapper.Clear();

            buttonWrapper.Add(_playButton);
            buttonWrapper.Add(_settingsButton);
            buttonWrapper.Add(_creditButton);
            buttonWrapper.Add(_QuitButton);

        };
        settingsContent = settingTemplate.CloneTree();



         chaptersContent = chaptersTemplate.CloneTree();

        var backButtonChapter = chaptersContent.Q<Button>("back");
        backButton.RegisterCallback<MouseEnterEvent>((evt) => AudioManager.Instance.PlaySound("Select"));
        var startButtonChapter = chaptersContent.Q<Button>("Start");
        startButtonChapter.RegisterCallback<MouseEnterEvent>((evt) => AudioManager.Instance.PlaySound("Select"));
        chernoubelChapter = chaptersContent.Q<Button>("chernoubel");
         titanicChapter = chaptersContent.Q<Button>("titanic");
         hiroushimaChapter = chaptersContent.Q<Button>("hiroushima");
         miroChapter = chaptersContent.Q<Button>(name: "mirwa7a");

        
chernoubelChapter.RegisterCallback<FocusEvent>((evt) => { focusedElement = evt.target as VisualElement;AudioManager.Instance.PlaySound("Select"); });
titanicChapter.RegisterCallback<FocusEvent>((evt) => { focusedElement = evt.target as VisualElement; AudioManager.Instance.PlaySound("Select"); });
hiroushimaChapter.RegisterCallback<FocusEvent>((evt) => { focusedElement = evt.target as VisualElement; AudioManager.Instance.PlaySound("Select"); });
miroChapter.RegisterCallback<FocusEvent>((evt) => { focusedElement = evt.target as VisualElement; AudioManager.Instance.PlaySound("Select"); });


        _playButton.RegisterCallback<MouseEnterEvent>((evt) => AudioManager.Instance.PlaySound("Select"));
        _settingsButton.RegisterCallback<MouseEnterEvent>((evt) => AudioManager.Instance.PlaySound("Select"));
        _creditButton.RegisterCallback<MouseEnterEvent>((evt) => AudioManager.Instance.PlaySound("Select"));
        _QuitButton.RegisterCallback<MouseEnterEvent>((evt) => AudioManager.Instance.PlaySound("Select"));


        backButtonChapter.clicked += () => {
            root.Clear();
            root.Add(leftSection);
            root.Add(_muteButton);


            
        };

        startButtonChapter.clicked += () => {
            if(focusedElement != null && focusedElement.name == "chernoubel"){
                           ScreenFade.instance.FadeIn("FinalGame");

            }
        
        };







        var backButtonSettings = settingsContent.Q<Button>("back");
        backButtonSettings.RegisterCallback<MouseEnterEvent>((evt) => AudioManager.Instance.PlaySound("Select"));
        musicSlider = settingsContent.Q<Slider>(name: "music");
        soundSlider = settingsContent.Q<Slider>(name :"sounds");

        musicSlider.RegisterValueChangedCallback(evt =>
            {
            AudioManager.Instance.MusicVolumeChanged(evt.newValue);
        });


        soundSlider.RegisterValueChangedCallback(evt =>
        {

            AudioManager.Instance.SoundVolumeChanged(evt.newValue);
        });

        backButtonSettings.clicked += () => {
            buttonWrapper.Clear();

            buttonWrapper.Add(_playButton);
            buttonWrapper.Add(_settingsButton);
            buttonWrapper.Add(_creditButton);
            buttonWrapper.Add(_QuitButton);

        };

        

        
    }

    private void Play()
    {   

        root.Clear();
        root.Add(chaptersContent);
chaptersContent.style.width  = new Length(100, LengthUnit.Percent);
chaptersContent.style.height = new Length(100, LengthUnit.Percent);
    }

    private void credits(){

        AudioManager.Instance.SceneIsloading = true;
        ScreenFade.instance.FadeIn("Story");


}

    private void Settings(){

        buttonWrapper.Clear();
        buttonWrapper.Add(settingsContent);
        musicSlider.value = AudioManager.Instance.MusicVolume;
        soundSlider.value = AudioManager.Instance.SoundVolume;
    }

    private void Quit(){
        Application.Quit();
    }


    private void muteClicked(){

        if(AudioManager.Instance.mute){
            _muteButton.style.backgroundImage = unmuteSprite.texture;
            AudioManager.Instance.mute = false;  
        }
            else{
            _muteButton.style.backgroundImage = muteSprite.texture;
            AudioManager.Instance.mute = true;
        }
    
    }
  
}
