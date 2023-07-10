    using UnityEngine;

    public class Pipe : MonoBehaviour
    {
        public Transform pipeTransform;
        public bool isCorrectRotation = false;




        public float[] targetRotations ;

        public plomberPuzzle gameManager;

        private void Start()
        {

            pipeTransform  =transform.GetComponent<Transform>();


            int randomRotation = Random.Range(0, 4); // 0, 1, 2, or 3
            float rotation = randomRotation * 90f;
            pipeTransform.rotation = Quaternion.Euler(x: 0f, 0f, rotation);
            if(IsRotationCorrect(currentRotation:rotation)){
                gameManager.MoveCorrect();
                isCorrectRotation =true;
            }
            
        }

        public void RotatePipe()
        {
            pipeTransform.Rotate(xAngle: 0f, 0f, 90f);


        if (IsRotationCorrect(currentRotation:GetNormalizedRotation(pipeTransform.eulerAngles.z)) && isCorrectRotation == false  )  
            {   
                            gameManager.MoveCorrect();

                isCorrectRotation= true;
            }
            else if(isCorrectRotation)
            { 
                isCorrectRotation= false;
            gameManager.MoveWrong(); // ?????
            }
        }

            private bool IsRotationCorrect(float currentRotation)
        {
        bool correct = false;

            foreach (float rot in targetRotations)
        {


            if((Mathf.Floor(currentRotation) -  rot )< 0.0001 ){
                currentRotation  = Mathf.Floor(currentRotation);
            }
            if(currentRotation == rot)
                correct = true;
        }
    
            return correct;

        }

 private float GetNormalizedRotation(float rotation)
    {
        rotation %= 360f;
        if (rotation < 0f)
        {
            rotation += 360f;
        }
       
        return rotation;
    }
    }
