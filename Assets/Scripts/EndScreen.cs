using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    // Serialized variable to link the final score text in the inspector, and use it in the script.
    [SerializeField] TextMeshProUGUI finalScoreText;
    // We need to get access to the score component as well.
    Score score;

    // In order to make all canvases are ready to use before the game actually starts,
    // we store the reference calls in the Awake method.
    void Awake()
    {
        // At the beginning, we need a reference to the score game object.
        score = FindObjectOfType<Score>();
    }

    // Method to show the final score, using the CalculateScore method.
    public void ShowFinalScore()
    {
        finalScoreText.text = "Felicitations, votre score est de " + score.CalculateScore() + "%";
    }
}