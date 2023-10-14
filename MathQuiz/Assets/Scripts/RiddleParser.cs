using System;
using System.Collections.Generic;
using UnityEngine;
public static class RiddleParser
{
    public static List<Riddle> GetRiddles(TextAsset textAsset)
    {
        List<Riddle> riddlesList = new List<Riddle>();

        string[] lines = textAsset.text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        
        Riddle currentRiddle = null;
        foreach (string line in lines)
        {
            if (line.StartsWith("[riddle_"))
            {
                if (currentRiddle != null)
                {
                    riddlesList.Add(currentRiddle);
                }
                currentRiddle = new Riddle();
            }else if (currentRiddle != null)
            {
                if (line.StartsWith("riddle: "))
                {
                    currentRiddle.SetRiddle(line.Substring("riddle: ".Length).Trim());
                }
                else if (line.StartsWith("answers: "))
                {
                    string[] answers = line.Substring("answers: ".Length).Split('|');
                    currentRiddle.SetAnswers(new List<string>(answers));
                }
            }
        }

        if (currentRiddle != null)
        {
            riddlesList.Add(currentRiddle);
        }

        return riddlesList;
    }
}