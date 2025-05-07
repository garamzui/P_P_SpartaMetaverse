using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 3; // 배경에 이용된 리소스들 이동한 갯수 (화면 전환 분기점때문에 리소스 갯추와 맞춰둠)
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
            // 현재 충돌된 BG
            GameObject currentBG = collision.gameObject;

            // 모든 BG 불러오기
            GameObject[] allBGs = GameObject.FindGameObjectsWithTag("BackGround");

            float maxX = float.MinValue;

            foreach (GameObject bg in allBGs)
            {
                if (bg == currentBG) continue; // 자신은 제외
                float bgRightEdge = bg.transform.position.x;
                if (bgRightEdge > maxX)
                {
                    maxX = bgRightEdge;
                }
            }

            // 넓이 계산 (BoxCollider 기준)
            BoxCollider2D col = currentBG.GetComponent<BoxCollider2D>();
            float width = col.size.x * currentBG.transform.localScale.x;

            // 정확한 다음 위치 계산
            Vector3 newPos = currentBG.transform.position;
            newPos.x = maxX + width;
            currentBG.transform.position = newPos;

            Debug.Log($"BG [{currentBG.name}] → 재배치: {newPos.x}");
        }

        // 장애물 처리
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

}
