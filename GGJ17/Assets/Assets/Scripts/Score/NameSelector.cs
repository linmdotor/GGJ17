using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NameSelector : MonoBehaviour {

    public Text firstLetterText;
    public Text secondLetterText;
    public Text thirdLetterText;

    public Text doneText;

    public Color unselectedColor;
    public Color selectedColor;
    
    private string currentFirstLetter;
    private string currentSecondLetter;
    private string currentThirdLetter;


    private int currentSelectedSpace = 0;

    private bool nameSelectorActive = true;


    private List<string> possiblesLetters = new List<string>();

	void Start () {

        currentFirstLetter = "A";
        currentSecondLetter = "A";
        currentThirdLetter = "A";
        
        currentSelectedSpace = 0;
        
        possiblesLetters.Add("A");
        possiblesLetters.Add("B");
        possiblesLetters.Add("C");
        possiblesLetters.Add("D");
        possiblesLetters.Add("E");
        possiblesLetters.Add("F");
        possiblesLetters.Add("G");
        possiblesLetters.Add("H");
        possiblesLetters.Add("I");
        possiblesLetters.Add("J");
        possiblesLetters.Add("K");
        possiblesLetters.Add("L");
        possiblesLetters.Add("M");
        possiblesLetters.Add("N");
        possiblesLetters.Add("O");
        possiblesLetters.Add("P");
        possiblesLetters.Add("Q");
        possiblesLetters.Add("R");
        possiblesLetters.Add("S");
        possiblesLetters.Add("T");
        possiblesLetters.Add("U");
        possiblesLetters.Add("V");
        possiblesLetters.Add("W");
        possiblesLetters.Add("X");
        possiblesLetters.Add("Y");
        possiblesLetters.Add("Z");
        possiblesLetters.Add("-");
        possiblesLetters.Add("+");
        possiblesLetters.Add("_");
        possiblesLetters.Add("/");

        highlightLetter();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (nameSelectorActive)
        {
            if (currentSelectedSpace == 3)
            {
              if (Input.GetButtonDown(KeyCodes.Submit))
              {
                  UIManager.UIManagerInstance.nameSelector.SetActive(false);
                  UIManager.UIManagerInstance.buttons.SetActive(true);
                  //UIManager.UIManagerInstance.highlightPlayAgainButton();
                  
                  PersistentScore.PersistentScoreInstance.setFinalScore(GameManager.GameManagerInstance.score,currentFirstLetter+currentSecondLetter+currentThirdLetter);

                  DeadCanvas.DeadCanvasInstance.updateHighScore(PersistentScore.PersistentScoreInstance.scores);
                  return;
              }
            }

            if (Input.GetButtonDown(KeyCodes.Up))
            {
                if (currentSelectedSpace == 0)
                {
                    int index = possiblesLetters.IndexOf(currentFirstLetter);
                    index++;

                    if (index == possiblesLetters.Count)
                        index = 0;

                    firstLetterText.text = possiblesLetters[index];
                    currentFirstLetter = possiblesLetters[index];

                }
                else if (currentSelectedSpace == 1)
                {
                    int index = possiblesLetters.IndexOf(currentSecondLetter);
                    index++;

                    if (index == possiblesLetters.Count)
                        index = 0;

                    secondLetterText.text = possiblesLetters[index];
                    currentSecondLetter = possiblesLetters[index];
                }
                else if (currentSelectedSpace == 2)
                {
                    int index = possiblesLetters.IndexOf(currentThirdLetter);
                    index++;

                    if (index == possiblesLetters.Count)
                        index = 0;

                    thirdLetterText.text = possiblesLetters[index];
                    currentThirdLetter = possiblesLetters[index];

                }
            }
            if (Input.GetButtonDown(KeyCodes.Down))
            {
                if (currentSelectedSpace == 0)
                {
                    int index = possiblesLetters.IndexOf(currentFirstLetter);
                    index--;

                    if (index < 0)
                        index = possiblesLetters.Count - 1;

                    firstLetterText.text = possiblesLetters[index];
                    currentFirstLetter = possiblesLetters[index];
                }
                else if (currentSelectedSpace == 1)
                {
                    int index = possiblesLetters.IndexOf(currentSecondLetter);
                    index--;

                    if (index < 0)
                        index = possiblesLetters.Count - 1;

                    secondLetterText.text = possiblesLetters[index];
                    currentSecondLetter = possiblesLetters[index];
                }
                else if (currentSelectedSpace == 2)
                {
                    int index = possiblesLetters.IndexOf(currentThirdLetter);
                    index--;

                    if (index < 0)
                        index = possiblesLetters.Count - 1;

                    thirdLetterText.text = possiblesLetters[index];
                    currentThirdLetter = possiblesLetters[index];

                }
            }


            if (Input.GetButtonDown(KeyCodes.Left))
            {
                currentSelectedSpace--;

                if (currentSelectedSpace < 0)
                    currentSelectedSpace++;

                highlightLetter();

            }

            if (Input.GetButtonDown(KeyCodes.Right))
            {
                currentSelectedSpace++;

                if (currentSelectedSpace > 3)
                    currentSelectedSpace--;

                highlightLetter();
            }
        }       
	}

    private void highlightLetter()
    {
        //Debug.Log("highlight");
        if(currentSelectedSpace == 0)
        {
            firstLetterText.color = selectedColor;
            secondLetterText.color = unselectedColor;
            thirdLetterText.color = unselectedColor;
            doneText.color = unselectedColor;
        }
        else if (currentSelectedSpace == 1)
        {
            firstLetterText.color = unselectedColor;
            secondLetterText.color = selectedColor;
            thirdLetterText.color = unselectedColor;
            doneText.color = unselectedColor;
        }
        else if (currentSelectedSpace == 2)
        {
            firstLetterText.color = unselectedColor;
            secondLetterText.color = unselectedColor;
            thirdLetterText.color = selectedColor;
            doneText.color = unselectedColor;
        }
        else if (currentSelectedSpace == 3)
        {
            firstLetterText.color = unselectedColor;
            secondLetterText.color = unselectedColor;
            thirdLetterText.color = unselectedColor;
            doneText.color = selectedColor;
        }
    }
}
