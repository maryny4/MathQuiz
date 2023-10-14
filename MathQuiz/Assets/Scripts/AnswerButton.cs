using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => GameAction.onClickAnswer?.Invoke(transform.GetSiblingIndex()));
    }
}
