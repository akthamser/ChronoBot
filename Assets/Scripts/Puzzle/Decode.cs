using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Decode : Puzzle
{
    public TextMeshProUGUI Result;
    public Switch[] Switches;
    public Image SolvedImage;
    public Image NotSolvedImage;
    private bool Solved = false;
    
    [System.Serializable]
    public struct Switch
    {
        public Image SwitchActiveImage;
        public Image SwitchInactiveImage;
        public Image SwitchImage;
        public Image SwitchEnabled;
        public Image SwitchDisabled;
        public bool Active;
        public bool CorrectStat;
    }
    
    public void InverseSwith(int index)
    {
        if (Solved)
            return;


        Switches[index].Active = !Switches[index].Active;

        if (Switches[index].Active)
        {
            AudioManager.Instance.PlaySound("SwitchUp");
            Switches[index].SwitchActiveImage.gameObject.SetActive(true);
            Switches[index].SwitchInactiveImage.gameObject.SetActive(false);

            Switches[index].SwitchEnabled.gameObject.SetActive(true);
            Switches[index].SwitchDisabled.gameObject.SetActive(false);
        }
        else
        {
            AudioManager.Instance.PlaySound("SwitchDown");
            Switches[index].SwitchActiveImage.gameObject.SetActive(false);
            Switches[index].SwitchInactiveImage.gameObject.SetActive(true);

            Switches[index].SwitchEnabled.gameObject.SetActive(false);
            Switches[index].SwitchDisabled.gameObject.SetActive(true);
        }
        CheckStatus();
    }

    private void OnEnable()
    {
        for(int i =0;i< Switches.Length; i++)
        {
            Switches[i].Active = false;

            Switches[i].SwitchActiveImage.gameObject.SetActive(false);
            Switches[i].SwitchInactiveImage.gameObject.SetActive(true);

            Switches[i].SwitchEnabled.gameObject.SetActive(false);
            Switches[i].SwitchDisabled.gameObject.SetActive(true);
        }

        CheckStatus();
    }


    public void CheckStatus()
    {
        bool Correct = true;

        for(int i=0;i< Switches.Length; i++)
        {
            if(Switches[i].Active != Switches[i].CorrectStat)
            {
                Correct = false;
                break;
            }
        }

        Solved = Correct;
        if (Correct)
        {
            AudioManager.Instance.PlaySound("taskdone");
            StartCoroutine(IEndTask());
            SolvedImage.gameObject.SetActive(true);
            NotSolvedImage.gameObject.SetActive(false);
        }
        else
        {
            NotSolvedImage.gameObject.SetActive(true);
            SolvedImage.gameObject.SetActive(false);
        }



        string HexString = String.Empty;
        for (int i = 0 ; i < Switches.Length / 4 ; i++)
        {

            string BinaryString = String.Empty;

            for (int j = 0; j < 4; j++)
            {
                char SwitchBinary = Switches[4 * i + j].Active ? '1' : '0';
                BinaryString += SwitchBinary;
            }

            HexString += HexConverted(BinaryString);
        }

        Result.text = HexString;
    }

    public IEnumerator IEndTask()
    {
        yield return new WaitForSeconds(1.5f);
        Controller.OnPuzzleDone.Invoke();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Controller.Player.UnLockPlayerMovment();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
        }

    }

    private string HexConverted(string strBinary)
    {
        string strHex = Convert.ToInt32(strBinary, 2).ToString("X");
        return strHex;
    }
}
