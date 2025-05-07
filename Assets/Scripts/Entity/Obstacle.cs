using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosy = 1f;
    public float lowPosy = -1f;

    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 9f;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public Vector3 SetRandomPlace(Vector3 lastPosition, int dnstaclCoount)
    {
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0f, 0f);
        transform.position = placePosition;

        // bottom은 항상 활성화
        bottomObject.gameObject.SetActive(true);
        bottomObject.localPosition = new Vector3(0f, 0f, 0f); // 바닥에 고정

        // top은 랜덤하게 등장 여부 결정
        bool showTop = Random.value > 0.5f;
        topObject.gameObject.SetActive(showTop);

        if (showTop)
        {
            // 살짝 겹치게 위에 배치 (2단 점프 필요할 수 있음)
            float topY = Random.Range(1f, 1.5f); // 겹침 범위 조절
            topObject.localPosition = new Vector3(0f, topY, 0f);
        }

        return placePosition;

    }
    //미니게임용 충돌 감지 점수
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.AddScore(1);
        }
    }
}
