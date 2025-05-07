using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 3; // ��濡 �̿�� ���ҽ��� �̵��� ���� (ȭ�� ��ȯ �б��������� ���ҽ� ���߿� �����)
    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround"))
        {
            // ���� �浹�� BG
            GameObject currentBG = collision.gameObject;

            // ��� BG �ҷ�����
            GameObject[] allBGs = GameObject.FindGameObjectsWithTag("BackGround");

            float maxX = float.MinValue;

            foreach (GameObject bg in allBGs)
            {
                if (bg == currentBG) continue; // �ڽ��� ����
                float bgRightEdge = bg.transform.position.x;
                if (bgRightEdge > maxX)
                {
                    maxX = bgRightEdge;
                }
            }

            // ���� ��� (BoxCollider ����)
            BoxCollider2D col = currentBG.GetComponent<BoxCollider2D>();
            float width = col.size.x * currentBG.transform.localScale.x;

            // ��Ȯ�� ���� ��ġ ���
            Vector3 newPos = currentBG.transform.position;
            newPos.x = maxX + width;
            currentBG.transform.position = newPos;

            Debug.Log($"BG [{currentBG.name}] �� ���ġ: {newPos.x}");
        }

        // ��ֹ� ó��
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

}
