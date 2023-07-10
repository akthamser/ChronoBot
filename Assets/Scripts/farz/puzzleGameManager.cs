using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class puzzleGameManager : MonoBehaviour
{


    private int totalPipes;

    public TextMeshProUGUI TEXT;

    public int correctPipes=0;

    public GameObject[] pipes;

       private void OnEnable()

    {
totalPipes = this.transform.childCount;

        pipes = new GameObject[totalPipes];
        for (int i = 0; i < totalPipes; i++)
        {
            pipes[i] = this.transform.GetChild(index: i).gameObject;
                }
            
    }

    public void MoveCorrect(){
        correctPipes ++ ;
        Debug.Log("correctPipes : " + correctPipes);
        if(correctPipes == totalPipes){
            GameWon();
        }
    }

    public  void MoveWrong(){
        correctPipes-- ;
                TEXT.text = "";

    }
    

    private void GameWon(){
        TEXT.text = "You Won";
    }


  
}
