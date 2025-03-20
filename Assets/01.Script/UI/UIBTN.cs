using UnityEngine;

public class UIBTN : MonoBehaviour
{
    public UIElement UI;
    
    public void OnUI()
    {
        if (!UI.gameObject.activeSelf)
        {
            UIManager.Instance.ShowUI(UI);
        }
        else
        {
            UIManager.Instance.HideUI(UI);
        }
        
    }
}