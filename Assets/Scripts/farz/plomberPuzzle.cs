using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class plomberPuzzle : Puzzle
{


    private int totalPipes ;


    public int correctPipes=0;

    public GameObject[] pipes;

       private void OnEnable()

    {
        MovmentController.Instance.LockPlayerMovment();
totalPipes = this.transform.childCount;

        pipes = new GameObject[totalPipes];
        for (int i = 0; i < totalPipes; i++)
        {
            pipes[i] = this.transform.GetChild(index: i).gameObject;
                }
            
    }

    public void MoveCorrect(){
        correctPipes ++ ;
        if(correctPipes == totalPipes){
            GameWon();
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            gameObject.SetActive(false);
    }

    public  void MoveWrong(){
        correctPipes-- ;
             

    }
    

    private void GameWon(){
        AudioManager.Instance.PlaySound("taskdone");
        Controller.OnPuzzleDone.Invoke();
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        MovmentController.Instance.UnLockPlayerMovment();
    }


}
