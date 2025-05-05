using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 플레이어 전용 컨트롤러
/// 입력을 받아 이동 처리 및 싱글톤으로 인스턴스를 유지함
/// </summary>
public class PlayerController : BaseController
{
    public static PlayerController Instance { get; private set; }

    private float moveX;
    private float moveY;

    [SerializeField] private float jumpCooldown = 1f;  // 점프 쿨타임 1초
    private float lastJumpTime = -999f;
    //무기 프리팹 자리
    [Header("무기")]
    [SerializeField] private Weapon weaponRight;
    [SerializeField] private Weapon weaponLeft;

    private bool isRightHandNext = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isRightHandNext)
                weaponRight.Use();
            else
                weaponLeft.Use();

            isRightHandNext = !isRightHandNext;
        }
    }

    private AnimatorController animatorController;

    /// <summary> 싱글톤 및 DontDestroy 처리 </summary>

    protected override void Awake()
    {
        animatorController = GetComponentInChildren<AnimatorController>();

        base.Awake();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 이동 시 파괴 방지
        }
        else
        {
            Destroy(gameObject);  // 중복 방지
        }

       
    }

    private void LateUpdate()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        // 횡스크롤 맵이라면 Y축 입력을 무시 (위아래 이동 불가)
        if (GameManager.Instance.IsSideScroll)
        {
            moveY = 0f;
        }

        // 이동 애니메이션 트리거
        animatorController.SetMove(moveX != 0 || moveY != 0);

        // 좌우 방향 반전 처리
        if (moveX != 0)
        {
            animatorController.SetFlip(moveX < 0);
        }

        // 점프 입력 및 쿨타임 검사
        if (Input.GetKeyDown(KeyCode.C) && Time.time >= lastJumpTime + jumpCooldown)
        {
            animatorController.SetJumpTrigger();       // 점프 애니메이션 실행
            lastJumpTime = Time.time;                  // 쿨타임 갱신
        }
    }

    /// <summary> 이동 처리 (물리 연산 기반 이동) </summary>
    private void FixedUpdate()
    {
        Vector2 input = new Vector2(moveX, moveY);
        Move(input);  // BaseController의 Move 호출
    }

   
}