using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SignManager : MonoBehaviour
{
    private static SignManager _instance;
    public static SignManager Instance => _instance;

    [SerializeField] private GameObject signPanel;  // �˶�â �г�
    [SerializeField] private TMP_Text signText;

    private bool isSignActive = false;


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
    private void Start()
    {
        signPanel.SetActive(false);
    }

    public void ShowSign(string message, float duration = 2f)
    {
        if (isSignActive) return; // �̹� �˶��� Ȱ��ȭ�Ǿ� ������ ���� ����� ����

        signPanel.SetActive(true);
        signText.text = message;
        isSignActive = true;

        StartCoroutine(CloseSign(duration));
        
    }
    private IEnumerator CloseSign(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (isSignActive)
        {
            signPanel.SetActive(false);
            isSignActive = false;
        }
    }
}
