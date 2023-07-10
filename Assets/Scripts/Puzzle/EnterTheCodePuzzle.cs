using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterTheCodePuzzle : Puzzle
{

    public TextMeshProUGUI CodeText;
    public int CodeSize;
    public string RightCode;
    

    private void OnEnable()
    {
        CodeText.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Exit();
    }
    public void wright(string _char)
    { 
        if(CodeText.text.Length < CodeSize)
                 CodeText.text += _char;
        AudioManager.Instance.PlaySound("num" + Random.Range(1, 4));

    }
    public void Delete()
    {
        string result="";
        for (int i = 0; i < CodeText.text.Length-1; i++)
        { 
            result += CodeText.text[i];
        }
        CodeText.text = result;
    
    }
    public void Submit()
    {
        if (CodeText.text == RightCode)
            rightpassword();
        else
            wrongpassword();

    }
    public void Exit()
    {
        gameObject.SetActive(false);

    }
    private void wrongpassword()
    {
        AudioManager.Instance.PlaySound("error");
        CodeText.text = "";
    }
    private void rightpassword()
    {
        AudioManager.Instance.PlaySound("taskdone");
        Controller.OnPuzzleDone.Invoke();
        gameObject.SetActive(false);  
    }
    private void OnDisable()
    {
        Controller.Player.UnLockPlayerMovment();
    }

}
