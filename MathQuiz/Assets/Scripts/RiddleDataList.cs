using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Riddle/RiddleDataList")]
public class RiddleDataList : ScriptableObject
{
    
    [SerializeField] private TextAsset riddleDateBase;
    [SerializeField] private List<Riddle> riddles;

    private int previousRiddle = -1;

    public Riddle GetRandomRiddle()
    {
        int randomRiddle = Random.Range(0, riddles.Count);
        
        while (randomRiddle == previousRiddle)
            randomRiddle = Random.Range(0, riddles.Count);

        previousRiddle = randomRiddle;
        return riddles[randomRiddle];
    }

    [ContextMenu("Load Riddles")]
    private void LoadRiddles()
    {
        if (riddleDateBase == null)
        {
            Debug.LogError("riddleDateBase (txt) is null"); //ToDo dodać sprawdzanie na poprawność pliku
            return;
        }
        riddles.Clear();
        riddles = RiddleParser.GetRiddles(riddleDateBase);
        CheckForErrors();
    }
    
    [ContextMenu("Check For Errors")]
    private void CheckForErrors()
    {
        if (riddles.Count == 0)
        {
            Debug.LogError("Riddles list is empty");
            return;
        }
        for (int i = 0; i < riddles.Count; i++)
        {
            if (riddles[i] != null)
            {
                string riddleError = String.IsNullOrEmpty(riddles[i].GetRiddle) ? " : riddleEmpty" : "";
                string answersError = "";
        
                foreach (var x in riddles[i].GetAnswers)
                    if (String.IsNullOrEmpty(x)) answersError = " : AnswersEmpty";
                
                if(riddles[i].GetAnswers.Count != 4)
                    answersError = " : wrongAnswers count != 4";

                if(riddleError == "" && answersError == "") return;
                Debug.LogError(i + riddleError + answersError);
            }
            else
            {
                Debug.LogError("Riddle nr: " + i + " is NULL");
            }
        }
    }
}
