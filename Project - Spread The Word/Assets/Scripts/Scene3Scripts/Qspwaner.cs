using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Qspwaner : MonoBehaviour
{
    public GameObject wordPrefab;
    public TMP_InputField inputField;

    public string AnswerWord;
    private float xOffset = 1.2f;
    private float yOffset = 1.5f;
    public int wordCount = 5;
    public int rowNumber = 0;
    private static string[] wordList = {
    "robin", "three", "jazzy", "grade", "cloud",
    "tarty", "rusty", "weird", "momma", "proud",
    "feels", "plays", "pizza", "crate", "sleet",
    "waxed", "smash", "tasty", "tray", "cream"
};


public static string GetRandomWord ()
	{
		int randomIndex = Random.Range(0, wordList.Length);
		string randomWord = wordList[randomIndex];

		return randomWord;
	}
public void SpawnLetters (int _rowNumber)
{
    string UserWord = inputField.text;

    for (int i = 0; i < wordCount; i++)
    {
        Vector3 position = new(transform.position.x + i * xOffset, transform.position.y - (_rowNumber * yOffset), transform.position.z);
        GameObject wordObj = Instantiate(wordPrefab, position, Quaternion.identity);
        TextMeshProUGUI textMesh = wordObj.GetComponentInChildren<TextMeshProUGUI>();

        // Check if the TextMeshPro component was found and if the user word is not empty
        if (textMesh != null && i < UserWord.Length)
        {
            textMesh.text = UserWord[i].ToString();
            ChangePrefabColor(wordObj, UserWord, AnswerWord, i);
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found in prefab children");
        }
    }
}

public void ChangePrefabColor(GameObject prefab, string userWord, string answerWord, int index)
{
    if (prefab.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
    {
        // Default color is red
        Color newColor = Color.red;

        // If the letter is in the answer word
        if (answerWord.Contains(userWord[index]))
        {
            // If the letter is at the same position in the answer word
            if (answerWord[index] == userWord[index])
            {
                newColor = Color.green;
            }
            else
            {
                newColor = Color.yellow;
            }
        }

        // Set the color of the SpriteRenderer component
        spriteRenderer.color = newColor;

        // If the Light2D component is found in the children, set its color
        Light2D light2D = prefab.GetComponentInChildren<Light2D>();
        if (light2D != null)
        {
            light2D.color = newColor;
        }
        else
        {
            Debug.LogError("Light2D component not found in prefab or its children");
        }
    }
    else
    {
        Debug.LogError("SpriteRenderer component not found in prefab");
    }
}
    void Start()
    {
    // //      for (int i = 0; i < 4; i++)
    // // {
    // //     SpawnLetters(i);
    // // }
    //     SpawnLetters(rowNumber);
    AnswerWord = GetRandomWord();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SpawnLetters(rowNumber);
            rowNumber++;
        }
    }
}
