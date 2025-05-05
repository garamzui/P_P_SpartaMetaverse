using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string targetSceneName;  // �̵��� �� �̸�
                                  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // �÷��̾�Tag�� ����� ����
        {
            SceneManager.LoadScene(targetSceneName);
            Debug.Log($"{targetSceneName}�� �̵�");
        }
    }
}
