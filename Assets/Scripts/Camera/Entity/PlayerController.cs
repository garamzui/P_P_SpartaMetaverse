using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 플레이어 전용 컨트롤러
/// - 입력 처리
/// - 이동/점프/공격 실행
/// - 무기 회전 및 캐릭터 방향 반전
/// - 애니메이션 연동
/// - 싱글톤으로 인스턴스 유지
/// </summary>
public class PlayerController : BaseController
{
    /// <summary> 전역에서 접근 가능한 인스턴스 (싱글톤) </summary>
    public static PlayerController Instance { get; private set; }

    // 입력값 저장
    private float moveX;
    private float moveY;
    private Vector2 moveInput;

    // 무기 회전 기준점 (무기 피벗)
    [SerializeField] private Transform mainPivot;
    [SerializeField] private Transform weaponPivotRight;
    [SerializeField] private Transform weaponPivotLeft;
    private Transform currentWeaponPivot;


    [Header("탑뷰 연출 점프 설정")]
    [SerializeField] private float jumpCooldown = 1f; // 점프 간격
    private float lastJumpTime = -999f; // 마지막 점프 시간 기록
    [Header("횡스크롤 물리 기반 점프 시스템")]
    [SerializeField] private Transform groundCheck;      // 바닥 판정 위치
    [SerializeField] private float groundCheckRadius = 0.1f; // 바닥 체크 범위
    [SerializeField] private LayerMask groundLayer;      // 바닥 레이어
    [SerializeField] private float jumpPower = 50f;       // 점프 힘
    [SerializeField] private int maxJumpCount = 2;
    private int currentJumpCount = 0;
    private bool wasGrounded = false;

    private bool isJumping = false;


    [Header("무기 설정")]
    [SerializeField] private Weapon weaponRight; // 오른손 무기
    [SerializeField] private Weapon weaponLeft;  // 왼손 무기
    [SerializeField] private float attackInterval = 0.2f; // 연속 공격 간격
    private float lastAttackTime = -999f; // 마지막 공격 시간
    private bool isRightHandNext = true;  // 다음 공격할 손

    // 애니메이션 제어용 컨트롤러
    private AnimatorController animatorController;

    [SerializeField] private int skillCost = 10; // 스킬 MP 소모량
    [SerializeField] private float skillCooldown = 1f;   // 스킬 쿨타임 (1초)
    private float lastSkillTime = -999f;                 // 마지막 스킬 사용 시각

    private void Start()
    {
        status.Init(this);
        PlayerUI.Instance.Init(status);
    }


    /// <summary>
    /// Awake: 컴포넌트 초기화 및 싱글톤 설정
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        animatorController = GetComponentInChildren<AnimatorController>();

        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴 방지
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스 제거
        }
    }

    /// <summary>
    /// Update: 프레임마다 호출, 입력 처리 및 애니메이션 제어 담당
    /// </summary>
    private void Update()
    {
        HandleInput();         // 이동 입력 감지
        HandleAnimation();     // 이동 애니메이션 처리
        HandleWeaponRotation(); // 무기 피벗 회전
        HandleSpriteFlip();    // 캐릭터 좌우 반전
                            
        HandleAttack();        // 공격 입력 및 연타 처리

    }

    /// <summary>
    /// FixedUpdate: 고정 시간마다 호출되며, 물리 기반 이동 처리
    /// </summary>
    private void FixedUpdate()
    {
        Vector2 input = new Vector2(moveX, moveY);


        if (!GameManager.Instance.IsSideScroll)
        {
            Move(input);
        }
        else { AutoRun(); }
            
         // BaseController의 이동 처리 호출
       
        
        
        
        
        HandleJump();   // 점프 입력 처리
        if (GameManager.Instance.IsSideScroll)
        {
            if (Mathf.Abs(rb.velocity.y) < 0.01f && currentJumpCount > 0)
            {
                currentJumpCount = 0;
                animatorController.SetRJump(false);
                Debug.Log("착지!");
            }
        }
    }

    /// <summary>
    /// 이동 키 입력을 받아 방향 벡터 계산
    /// </summary>
    private void HandleInput()
    {
        if (GameManager.Instance.IsSideScroll)
        {
            // 입력 차단 (플레이어는 자동으로 오른쪽 이동)
            moveX = 1f;   // 계속 오른쪽으로 이동

        }
        else
        {
            // 자유 입력 가능 (탑다운 모드)
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");
        }

        moveInput = new Vector2(moveX, moveY).normalized;
    }
    /// <summary>
    /// 이동 중 여부에 따라 애니메이션 파라미터 설정
    /// </summary>
    private void HandleAnimation()
    {
        bool isMoving = moveX != 0 || moveY != 0;
        animatorController.SetMove(isMoving);
    }


    /// <summary>
    /// 캐릭터 이동 방향에 따라 무기 회전 처리
    /// </summary>
    private void HandleWeaponRotation()
    {
        if (moveInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;

            // 캐릭터가 왼쪽을 보고 있을 때 회전 방향 반전
            if (moveX < 0)
                angle += 180f;

            weaponPivotRight.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            weaponPivotLeft.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    /// <summary>
    /// 캐릭터 좌우 반전 (flipX) 처리
    /// 무기에는 적용하지 않음
    /// </summary>
    private void HandleSpriteFlip()
    {
        if (moveX != 0)
            animatorController.SetFlip(moveX < 0);
        if (moveX != 0)
            mainPivot.localScale = new Vector3(moveX < 0 ? -1f : 1f, 1f, 1f); // flipX 효과
    }


    /// <summary>
    /// 점프 키 입력 감지 및 쿨타임 확인 후 점프 실행
    /// </summary>
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (GameManager.Instance.IsSideScroll)
            {
                if (currentJumpCount < maxJumpCount)
                {
                    //Vector3 v = rb.velocity;
                    //v.y +=  jumpPower;
                    //rb.velocity = v;
                    rb.velocity = new Vector2(rb.velocity.x, 0f); // Y 속도 초기화
                    rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

                    currentJumpCount++;
                    animatorController.SetRJump(true);
                    Debug.Log($"점프 {currentJumpCount}/{maxJumpCount}");
                }
            }
            else
            {
                // 탑뷰 모드: 연출 점프
                animatorController.SetJumpTrigger();
                lastJumpTime = Time.time;
                Debug.Log("연출 점프");
            }
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    /// <summary>
    /// 공격 키 유지 시 일정 간격으로 번갈아 가며 무기 사용
    /// </summary>
    private void HandleAttack()
    {
        if (Input.GetKey(KeyCode.X) && Time.time >= lastAttackTime + attackInterval)
        {
            if (isRightHandNext)
                weaponRight.Use();
            else
                weaponLeft.Use();

            isRightHandNext = !isRightHandNext;
            lastAttackTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Time.time >= lastSkillTime + skillCooldown)
            {
                if (status.UseMP(skillCost))
                {
                    // 스킬 데미지 계산
                    int damage = status.Attack * 3;
                    Debug.Log($"스킬 발동! 데미지: {damage}");


                    animatorController.SetSkillTrigger();
                    weaponLeft.UseSkill();
                    weaponRight.UseSkill();



                    isRightHandNext = !isRightHandNext;
                    lastSkillTime = Time.time;
                }
                else
                {
                    Debug.Log("마나 부족! 스킬 실패");
                }
            }
            else
            {
                Debug.Log("쿨타임 중! 발동 불가");
            }
        }
    }
}