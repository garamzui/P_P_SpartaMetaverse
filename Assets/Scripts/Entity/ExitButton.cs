using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    /// <summary>
    /// ��ư�� ������ �޼���. MainMap ������ �̵��Ѵ�.
    /// </summary>
    public void BackToMain()
    {
        GameManager.Instance.SetTopDownMode();  
       SceneManager.LoadScene("MainScene"  );
    }
}