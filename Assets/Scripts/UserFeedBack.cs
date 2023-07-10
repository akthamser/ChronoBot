using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UserFeedBack : MonoBehaviour
{
    public static UserFeedBack Instance;
    public TextMeshProUGUI UserFeedBackText;
    public void SetTextOneFrame(string Text)
    {
        UserFeedBackText.gameObject.SetActive(true);
        UserFeedBackText.text = Text;
        StartCoroutine(IDisableText());
    }

    private void Awake()
    {
        Instance = this;
        UserFeedBackText.gameObject.SetActive(false);
        UserFeedBackText.text = "";
    }

    public void SetText(string Text)
    {
        UserFeedBackText.gameObject.SetActive(true);
        UserFeedBackText.text = Text;
    }
    public void DisableText()
    {
        UserFeedBackText.gameObject.SetActive(false);
        UserFeedBackText.text = "";
    }

    public IEnumerator IDisableText()
    {
        yield return new WaitForEndOfFrame();
        UserFeedBackText.gameObject.SetActive(false);
        UserFeedBackText.text = "";
    }
}
