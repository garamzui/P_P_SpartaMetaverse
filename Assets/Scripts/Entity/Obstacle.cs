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

        // bottom�� �׻� Ȱ��ȭ
        bottomObject.gameObject.SetActive(true);
        bottomObject.localPosition = new Vector3(0f, 0f, 0f); // �ٴڿ� ����

        // top�� �����ϰ� ���� ���� ����
        bool showTop = Random.value > 0.5f;
        topObject.gameObject.SetActive(showTop);

        if (showTop)
        {
            // ��¦ ��ġ�� ���� ��ġ (2�� ���� �ʿ��� �� ����)
            float topY = Random.Range(1f, 1.5f); // ��ħ ���� ����
            topObject.localPosition = new Vector3(0f, topY, 0f);
        }

        return placePosition;

    }
    //�̴ϰ��ӿ� �浹 ���� ����
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.AddScore(1);
        }
    }
}
