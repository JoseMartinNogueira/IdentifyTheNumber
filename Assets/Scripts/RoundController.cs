using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : MonoBehaviour
{
    [SerializeField] private Text textShow;
    [SerializeField] private Text counter;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button centreButton;
    [SerializeField] private Button rightButton;

    private string correctNumberText;
    private int correctNumber;
    private int correctButton;

    private string leftButtonText;
    private string centreButtonText;
    private string rightButtonText;

    private bool nextRound = false;

    public int correctAnswers = 0;
    public int wrongAnswers = 0;

    private int lastCorrectCount;
    private int lastWrongCount;
    public bool strikeOne = false;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCounter();
        StartRound();

        leftButton.GetComponent<ButtonScript>().FadeIn();
        centreButton.GetComponent<ButtonScript>().FadeIn();
        rightButton.GetComponent<ButtonScript>().FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCounter();
        if (nextRound)
        {
            nextRound = false;

            StartCoroutine(StartNextRound());
            
            strikeOne = false;
            leftButton.GetComponent<ButtonScript>().FadeIn();
            centreButton.GetComponent<ButtonScript>().FadeIn();
            rightButton.GetComponent<ButtonScript>().FadeIn();
        }
        else
        {
            
            if (lastWrongCount+2 == wrongAnswers)
            {
                ActivateCorrectButton();
                lastWrongCount = wrongAnswers;
                nextRound = true;

            }
            if(lastCorrectCount + 1 == correctAnswers)
            {

                StartCoroutine(EndRound());
            }
            
        }
        
    }

    private void StartRound()
    {

        RandomNumbers.GenerateRandomNumbers(out correctNumberText, out correctNumber);

        textShow.text = correctNumberText;

        textShow.GetComponent<ShowTextScript>().RestartAnimation();

        RandomizeButtons(out leftButtonText, out centreButtonText, out rightButtonText);

        leftButton.GetComponentInChildren<Text>().text = leftButtonText;
        leftButton.GetComponent<ButtonScript>().numberToCheck = correctNumber.ToString();
        
        centreButton.GetComponentInChildren<Text>().text = centreButtonText;
        centreButton.GetComponent<ButtonScript>().numberToCheck = correctNumber.ToString();
        
        rightButton.GetComponentInChildren<Text>().text = rightButtonText;
        rightButton.GetComponent<ButtonScript>().numberToCheck = correctNumber.ToString();
        

        
    }

    public void UpdateCounter()
    {
        counter.text = "Encerts: " + correctAnswers.ToString() + "\n" + "Errades: " + wrongAnswers.ToString(); 
    }

    private void RandomizeButtons(out string leftButtonText, out string centreButtonText, out string rightButtonText)
    {
        int number1;
        int number2;

        GetTwoDifferentRandomNumbers(out number1, out number2);

        correctButton = Random.Range(0, 2);
        string[] shuffle = new string[3];
        shuffle[correctButton] = correctNumber.ToString();
        shuffle[(correctButton + 1) % 3] = ((correctNumber + number1) % RandomNumbers.MAX_NUMBER + 1).ToString();
        shuffle[(correctButton + 2) % 3] = ((correctNumber + number2) % RandomNumbers.MAX_NUMBER + 1).ToString();

        leftButtonText = shuffle[0];
        centreButtonText = shuffle[1];
        rightButtonText = shuffle[2];


    }

    private void GetTwoDifferentRandomNumbers(out int candidate1, out int candidate2)
    {
        int number1 = Random.Range(1, RandomNumbers.MAX_NUMBER - 1);
        int number2;
        do
        {
            number2 = Random.Range(1, RandomNumbers.MAX_NUMBER - 1);
        }
        while (number1 == number2);

        candidate1 = number1;
        candidate2 = number2;
    }

    private void ActivateCorrectButton()
    {
        switch(correctButton)
        {
            case 0:
                leftButton.GetComponent<Animator>().SetTrigger("Correct");
                break;
            case 1:
                centreButton.GetComponent<Animator>().SetTrigger("Correct");
                break;
            case 2:
                rightButton.GetComponent<Animator>().SetTrigger("Correct");
                break;
        }
    } 

    IEnumerator StartNextRound()
    {
        yield return new WaitForSeconds(2);
        StartRound();
        yield return new WaitForSeconds(2);
    }
    IEnumerator EndRound()
    {
        
        leftButton.GetComponent<ButtonScript>().FadeOut();
        centreButton.GetComponent<ButtonScript>().FadeOut();
        rightButton.GetComponent<ButtonScript>().FadeOut();

        yield return new WaitForSeconds(1);

        lastCorrectCount = correctAnswers;
        lastWrongCount = wrongAnswers;
        nextRound = true;
    }
}
