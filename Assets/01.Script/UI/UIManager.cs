using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;
    private Stack<UIElement> uiStack = new Stack<UIElement>();
    private int currentSortOrder = 0;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject); 
        }
    }
    public void ShowUI(UIElement uiElement)
    {
        if (uiStack.Count>0)
        {
            UIElement UIe = uiStack.Pop();
            UIe.Hide();
        }

        uiElement.SetSortOrder(currentSortOrder++);
        uiElement.Show();

        uiStack.Push(uiElement);
    }

    public void HideUI(UIElement uiElement)
    {
        if (uiStack.Contains(uiElement))
        {
            uiStack.Pop();
            uiElement.Hide();

            if (uiStack.Count > 0)
            {
                uiStack.Peek().SetSortOrder(currentSortOrder--);
            }
        }
    }

    public UIElement GetTopUI()
    {
        return uiStack.Count > 0 ? uiStack.Peek() : null;
    }
}
