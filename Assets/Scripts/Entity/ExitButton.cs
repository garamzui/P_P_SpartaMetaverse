using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    /// <summary>
    /// 버튼에 연결할 메서드. MainMap 씬으로 이동한다.
    /// </summary>
    public void LoadMainMap()
    {
        SceneManager.LoadScene("MainScene");
    }
}