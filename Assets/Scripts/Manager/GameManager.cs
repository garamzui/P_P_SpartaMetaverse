using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance; //싱글톤 작성
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //씬 전환하여도 파괴하지않고 사용
        }
        else
        { 
            Destroy(gameObject);   //중복생성 방지  
        }
 
    }


   


    private void OnEnable()//씬이 바뀔 때마다 자동으로 알림받도록 이벤트 등록
    {
        // 씬 로드 이벤트 등록 (씬이 바뀔 때마다 OnSceneLoaded 호출됨)
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }
    private void OnDisable() //오브젝트가 꺼지면 이벤트 해제 (안 하면 메모리 누수 위험)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)//씬 이름을 감지해서, 어떤 모드로 설정할지 결정
    {
        switch (scene.name)
        {
            case "MainMap":
                SetTopDownMode();     // 탑뷰 모드
                break;

            case "Dungeon":
                SetTopDownMode();  // 횡스크롤 모드
                break;
            case "JumpGame":
                SetSideScrollMode();
                break;
        }
    }
    private bool isSideScroll = false;                   // 현재 씬이 횡스크롤인지 판단
    public bool IsSideScroll { get; set; }

    public void SetTopDownMode()//탑뷰 모드일 때 위치랑 중력 설정
    {
        var player = PlayerController.Instance;

        if (player != null)
        {
            player.transform.position = new Vector2(0, 0);      // 시작 위치 설정
            player.GetComponent<Rigidbody2D>().gravityScale = 0f; // 중력 없음
            IsSideScroll = false;
        }
    }
    


    private void SetSideScrollMode() //횡스크롤 맵일 땐 아래처럼 중력을 강하게 주고 위치도 약간 띄워줘
    {
        var player = PlayerController.Instance;

        if (player != null)
        {
            player.transform.position = new Vector2(0, 2);      // 약간 위에서 시작
            player.GetComponent<Rigidbody2D>().gravityScale = 20f; // 중력 적용
            IsSideScroll = true;
        }
    }



   
}
