using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
   
    public string numberToCheck;
    private Animator buttonAnimator;
    private RoundController roundController;


    // Start is called before the first frame update
    void Start()
    {
        buttonAnimator = this.GetComponent<Animator>();
        roundController = this.GetComponentInParent<RoundController>();
        this.GetComponent<Button>().onClick.AddListener(() => CheckNumber() );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckNumber()
    {
        if(string.Equals(numberToCheck, this.GetComponentInChildren<Text>().text))
        {
            buttonAnimator.SetTrigger("Correct");
            roundController.correctAnswers++;
        }
        else 
        {

            buttonAnimator.SetTrigger("Wrong");
            if(!roundController.strikeOne )
            {
                buttonAnimator.SetBool("FadeOut", true);
                roundController.wrongAnswers++;
            }
            else
            {
                roundController.wrongAnswers++;
            }
            
        }
    }

    public void FadeIn()
    {
        buttonAnimator.SetTrigger("FadeIn");
        buttonAnimator.SetBool("FadeOut", false);
    }

    public void FadeOut()
    {
        if(!buttonAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeIn"))
        {
            buttonAnimator.SetBool("FadeOut", true);
        }   
    }
    
}
