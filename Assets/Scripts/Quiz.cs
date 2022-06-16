using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    // Serialized variable to use Text Mesh Pro to write down text on the question in the user interface.
    [SerializeField] TextMeshProUGUI questionText;

    // Serialized variable to use the question scriptable object class.
    [SerializeField] QuestionScriptableObject question;

    // Serialized variable to create fields in Unity for the buttons available.
    [SerializeField] GameObject [] answerButtons;

    // Variable containing the correct answer index number.
    int correctAnswer;

    // Serialized sprite variable containing the default answer sprite.
    [SerializeField] Sprite defaultAnswerSprite;

    // Serialized sprite variable containing the correct answer sprite.
    [SerializeField] Sprite correctAnswerSprite;

    void Start()
    {
        // The question and answers are displayed.
        DisplayQuestion();
    }

    public void OnAnswerSelected(int index)
    {
        // Image variable linking to the buttons image.
        Image buttonImage;

        // If the answer selected as index is the correct answer...
        if (index == question.GetCorrectAnswer())
        {
            // The question text box will show the following 'correct' message...
            questionText.text = "Bonne reponse !";
            // The button image will be accessed...
            buttonImage = answerButtons[index].GetComponent<Image>();
            // And the corresponding sprite will be the correct answer sprite.
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            // We create a variable containing the Get Correct Answer method from the question scriptable object.
            // The value of this variable is an integer (or number).
            correctAnswer = question.GetCorrectAnswer();
            // We create a string variable to print the correct answer sentence on screen.
            string goodAnswer = question.GetAnswer(correctAnswer);
            // The question text box will display a message concatenated with the string variable sentence.
            questionText.text = "Faux, la bonne reponse est :\n" + goodAnswer;
            // The button image of the correct answer will be accessed...
            buttonImage = answerButtons[correctAnswer].GetComponent<Image>();
            // And the corresponding sprite will be the correct answer sprite.
            buttonImage.sprite = correctAnswerSprite;
        }

        // Once the player has answered the question (right or wrong), all buttons become disabled.
        SetButtonState(false);
    }

    // Everytime another question pops up...
    void GetNextQuestion()
    {
        // All buttons are enabled...
        SetButtonState(true);
        // All buttons image are set to default...
        SetDefaultButtonSprites();
        // The question and answers text are displayed.
        DisplayQuestion();
    }

    // Display Question & Answers Method.
    void DisplayQuestion()
    {
        // We call the Get Question method created in the scriptable object to get the question.
        // Then we store the question value in a variable using Text Mesh Pro to load the text on screen.
        questionText.text = question.GetQuestion();

        // In order to print on screen all the available answers, we use a 'for loop'.
        // For every element (i) starting from the beginning (index 0) to the last of the answer buttons (answerButtons.Length),
        // the loop browses every element by one iteration or step (i++).
        for (int i=0 ; i<answerButtons.Length ; i++)
        {
            // Once the answer buttons are imported in the script's serialized fields, we store the text value
            // of the buttons (TextMeshProUGUI) contained in the answer buttons' child component (GetComponentInChildren).
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            // We call the GetAnswer method to get the answer at each iteration of the loop.
            buttonText.text = question.GetAnswer(i);
        }
    }

    // Button State set to clickable (interactable) Method
    void SetButtonState(bool state)
    {
        // By default, we want all the answer buttons to be clickable.
        // The boolean variable 'state' can be set to true or false for all buttons.
        for (int i = 0; i < answerButtons.Length; i++)
        {
            // With the loop, we select all the buttons available from the components...
            Button button = answerButtons[i].GetComponent<Button>();
            // We set the boolean state active for the buttons (true or false).
            button.interactable = state;
        }
    }

    // Button Sprite set to default Method
    void SetDefaultButtonSprites()
    {
        // For all the answer buttons, we set their default image...
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}