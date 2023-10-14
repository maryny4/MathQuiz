using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Riddle
{
    [SerializeField] private string riddle;
    [SerializeField] private List<string> answers;// nr. 0 is correct answer
    
    public string GetRiddle => riddle;
    public List<string> GetAnswers => answers;

    public void SetRiddle(string riddle) => this.riddle = riddle;
    public void SetAnswers(List<string> answers) => this.answers = answers;
}
