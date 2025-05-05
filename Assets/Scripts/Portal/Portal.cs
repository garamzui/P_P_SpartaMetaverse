using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string targetSceneName;  // 이동할 씬 이름
                                  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // 플레이어Tag가 닿았을 때만
        {
            SceneManager.LoadScene(targetSceneName);
            Debug.Log($"{targetSceneName}로 이동");
        }
    }
}
