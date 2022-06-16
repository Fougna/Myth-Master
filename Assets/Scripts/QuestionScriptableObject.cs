using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In order to add new questions in Unity, we use Create Asset Menu to add a new entry in the right-click menu.
[CreateAssetMenu(menuName = "Greek Mythology Question", fileName = "New Greek Mythology Question")]

public class QuestionScriptableObject : ScriptableObject
{
    // Since we might have long questions to write, we first define the box size of the text.
    // The values are (minimum lines, maximum lines).
    [TextArea(2,6)]

    // Serialized question box to fill in Unity.
    [SerializeField] string question = "Enter new question text here";

    // Serialized array of possible answers to fill in Unity.
    // Arrays allow flexibility, first we create an empty array as variable.
    // Then we assign to this variable the value of a new string array of 4 elements.
    [SerializeField] string [] answers = new string [4];

    // Serialized field where we type the number corresponding to the correct answer.
    [SerializeField] int correctAnswer;

    // To get a question, we need a Getter Method which is read-only.
    // The method must be a string (not a void), so it can return a result.
    public string GetQuestion()
    {
        return question;
    }

    // Getter Method to get the number (integer) corresponding to the correct answer.
    public int GetCorrectAnswer()
    {
        return correctAnswer;
    }

    // Getter Method to get the index of the answers array.
    public string GetAnswer(int index)
    {
        return answers [index];
    }
}