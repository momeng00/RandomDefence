using UnityEngine;

public class UIElement : MonoBehaviour
{
    private int sortOrder;
    public Canvas canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<Canvas>();
        canvasGroup.gameObject.SetActive(false);
    }

    public void SetSortOrder(int order)
    {
        sortOrder = order;
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.sortingOrder = sortOrder;
        }
    }

    public void Show()
    {
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.GetComponent<Canvas>();
        }
        if (canvasGroup != null)
        {
            canvasGroup.gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        if (canvasGroup != null)
        {
            canvasGroup.gameObject.SetActive(false);
        }
    }

    public bool IsTopUI()
    {
        return UIManager.Instance.GetTopUI() == this;
    }
}
