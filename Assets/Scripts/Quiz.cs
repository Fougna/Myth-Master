using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    // Headers are useful to organize serialized variables in the inspector.
    [Header("Questions")]
    // Serialized variable to use Text Mesh Pro to write down text on the question in the user interface.
    [SerializeField] TextMeshProUGUI questionText;
    // Serialized list to store all the available questions.
    // To add to security, we also create a new list of questions in this variable.
    [SerializeField] List<QuestionScriptableObject> questions = new List<QuestionScriptableObject>();
    // Variable to use the question scriptable object class.
    QuestionScriptableObject currentQuestion;

    [Header("Answers")]
    // Serialized variable to create fields in Unity for the buttons available.
    [SerializeField] GameObject [] answerButtons;
    // Variable containing the correct answer index number.
    int correctAnswer;
    // Boolean variable to set if the player has answered the question before the timer runs out.
    bool hasAnsweredEarly = true;

    [Header("Buttons")]
    // Serialized sprite variable containing the default answer sprite.
    [SerializeField] Sprite defaultAnswerSprite;
    // Serialized sprite variable containing the correct answer sprite.
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    // Serialized Field to select the image for the timer in Unity.
    [SerializeField] Image timerImage;
    // In order to access the timer script, we declare a variable related to it.
    Timer timer;

    [Header("Scoring")]
    // Serialized variable to use Text Mesh Pro to modify the score on the UI.
    [SerializeField] TextMeshProUGUI scoreText;
    // In order to access the score script, we declare a variable related to it.
    Score score;

    void Start()
    {
        // At start, we need a reference to the timer game object...
        timer = FindObjectOfType<Timer>();
        // And a reference to the score game object.
        score = FindObjectOfType<Score>();
    }

    void Update()
    {
        // We tie the fillAmount option from the timer image component to the timer fillFraction variable that was set up to public in the Timer script.
        timerImage.fillAmount = timer.fillFraction;
        // If it's possible for the timer to load the next question...
        if (timer.loadNextQuestion)
        {
            // The player mustn't have answered early...
            hasAnsweredEarly = false;
            // Then we load a new question...
            GetNextQuestion();
            // Once the new question is loaded, we set the possibility to load another question back to false,
            // so that the script doesn't keep loading a new question every frame.
            timer.loadNextQuestion = false;
        }
        // Or if the player hasn't answered early and cannot answer the question...
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            // The answer is displayed. To ensure it won't pass accidentally a correct answer,
            // we use a common practice to pass a negative value since in an array of numbers always starts with 0.
            DisplayAnswer(-1);
            // The buttons are disabled.
            SetButtonState(false);
        }
    }

    // Method for when the answer is selected.
    // We put the index in the parameters, so it can display the correct answer.
    public void OnAnswerSelected(int index)
    {
        // The player answered before the timer runs out...
        hasAnsweredEarly = true;
        // Therefore, the answer is displayed...
        DisplayAnswer(index);
        // Since the player has answered the question, all buttons are disabled...
        SetButtonState(false);
        // The timer is stopped...
        timer.CancelTimer();
        // And the score text is modified after the score calculation.
        scoreText.text = "Score: " + score.CalculateScore() + "%";
    }

    // Method to display the answer.
    // We put the index in the parameters, so it can display the correct answer.
    void DisplayAnswer(int index)
    {
        // Image variable linking to the buttons image.
        Image buttonImage;

        // If the answer selected as index is the correct answer...
        if (index == currentQuestion.GetCorrectAnswer())
        {
            // The question text box will show the following 'correct' message...
            questionText.text = "Bonne reponse !";
            // The button image will be accessed...
            buttonImage = answerButtons[index].GetComponent<Image>();
            // The sprite shown will be the correct answer sprite...
            buttonImage.sprite = correctAnswerSprite;
            // And the score will be incremented by one.
            score.IncrementCorrectAnswers();
        }
        // Otherwise...
        else
        {
            // We create a variable containing the Get Correct Answer method from the question scriptable object.
            // The value of this variable is an integer (or number).
            correctAnswer = currentQuestion.GetCorrectAnswer();
            // We create a string variable to print the correct answer sentence on screen.
            string goodAnswer = currentQuestion.GetAnswer(correctAnswer);
            // The question text box will display a message concatenated with the string variable sentence.
            questionText.text = "Oups, la bonne reponse etait :\n" + goodAnswer;
            // The button image of the correct answer will be used...
            buttonImage = answerButtons[correctAnswer].GetComponent<Image>();
            // And the corresponding sprite will be the correct answer sprite.
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    // Everytime another question pops up...
    void GetNextQuestion()
    {
        // If the number of questions left is superior to zero...
        if (questions.Count > 0)
        {
            // All buttons are enabled...
            SetButtonState(true);
            // All buttons image are set to default...
            SetDefaultButtonSprites();
            // We search for a random question...
            GetRandomQuestion();
            // The question and answers text are displayed...
            DisplayQuestion();
            // And the number of questions seens is incremented by one.
            score.IncrementQuestionsSeen();
        }
    }

    // Random question fetch method
    void GetRandomQuestion()
    {
        // We sort the index of question randomly between the first entry in the list (0) and the last (.Count)...
        int index = Random.Range(0, questions.Count);
        // The current question is the first question picked up from the randomized index...
        currentQuestion = questions[index];

        // For security, we verify if the list of questions does contain the current question selected...
        if(questions.Contains(currentQuestion))
        {
            // Then the current question is removed in the list, so it will never pop up again.
            questions.Remove(currentQuestion);
        }
    }

    // Display Question & Answers Method.
    void DisplayQuestion()
    {
        // We call the Get Question method created in the scriptable object to get the question.
        // Then we store the question value in a variable using Text Mesh Pro to load the text on screen.
        questionText.text = currentQuestion.GetQuestion();

        // In order to print on screen all the available answers, we use a 'for loop'.
        // For every element (i) starting from the beginning (index 0) to the last of the answer buttons (answerButtons.Length),
        // the loop browses every element by one iteration or step (i++).
        for (int i = 0 ; i < answerButtons.Length ; i++)
        {
            // Once the answer buttons are imported in the script's serialized fields, we store the text value
            // of the buttons (TextMeshProUGUI) contained in the answer buttons' child component (GetComponentInChildren).
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            // We call the GetAnswer method to get the answer at each iteration of the loop.
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    // Button State set to clickable (interactable) method
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
        // The loop browses through all the buttons available...
        for (int i = 0; i < answerButtons.Length; i++)
        {
            // And find their image component that will be stored in a variable...
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            // Then we set the button image to default.
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}