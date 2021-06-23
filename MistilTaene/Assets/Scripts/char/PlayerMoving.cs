using System.Collections;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public static PlayerMoving Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    Rigidbody2D rigid;
    Animator animator;
    CapsuleCollider2D collidor;
    public float moveInput;
    public float moveSpeed;
    public float jumpOwer;
    public Vector2 movedir;
    public Vector3 movement;
    public bool IsJumping = false;
    public bool Isfloor;
    public bool isBorder;
    public bool headbak;
    public bool badak;
    private bool CanRuns = false; //s
    private bool CanRund = false; //d
    public bool candownjump;
    public bool downjumping;
    public float checkRun = 0.5f; //더블클릭 체크
    public bool Running; //달리기 상태 확인
    public bool isflip = false;
    public bool canmove = true;
    public bool iswall = false;
    public Vector3 wallpos;
    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        collidor = gameObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        keycheck();
        if (iswall)
            transform.position = wallpos;
        else
        {
            if (canmove)
                Move();
        }
            stoptowall();
       
    }
    private void Update()
    {
        Jumps();
    }
    void stoptowall() //벽박기,엔티티 충돌판정
    {
        //레이 쏘기 
        Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.up * 0.5f, Color.red);
        //Debug.DrawRay(transform.position + new Vector3(-1f, 1.4f, 0), transform.right * 2f, Color.red);
        headbak = Physics2D.Raycast(transform.position + new Vector3(0, 1f, 0), transform.up, 0.5f, LayerMask.GetMask("BADAK"));
        //headbak = Physics2D.Raycast(transform.position + new Vector3(-1f,1.4f, 0), transform.right, 2f, LayerMask.GetMask("BADAK"));

        Debug.DrawRay(transform.position + new Vector3(0, -2f, 0), -transform.up * 0.5f, Color.red);
        //Debug.DrawRay(transform.position + new Vector3(-1, -1.4f, 0), transform.right * 2f, Color.red);
        badak = Physics2D.Raycast(transform.position + new Vector3(0, -2f, 0), -transform.up, 0.5f, LayerMask.GetMask("BADAK"));
        //badak = Physics2D.Raycast(transform.position + new Vector3(-1, -1.4f, 0), transform.right, 2f, LayerMask.GetMask("BADAK"));

        if (!isflip)
        {
            Debug.DrawRay(transform.position + new Vector3(0, 1, 0), transform.right * 1f, Color.green);
            isBorder = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), transform.right, 1f, LayerMask.GetMask("wall"));
        }
        else
        {
            Debug.DrawRay(transform.position + new Vector3(0, 1, 0), -transform.right * 1f, Color.green);
            isBorder = Physics2D.Raycast(transform.position, -transform.right + new Vector3(0, 1, 0), 1f, LayerMask.GetMask("wall"));
        }
    }
    public void Move()

    {
        moveInput = Input.GetAxis("Horizontal");
        if (moveInput != 0)
        {
            animator.SetBool("ismove", true);
            if (Running)
            {
                animator.SetBool("isrun", true);
                moveSpeed = 8f;
            }
            else
            {
                animator.SetBool("isrun", false);
                moveSpeed = 4f;
            }
            if (isBorder)
            {
                moveSpeed = 0f;
            }
            
            Vector3 moveVelocity = new Vector3(moveInput, 0, 0) * moveSpeed * Time.deltaTime;
            transform.position += moveVelocity;

            if (moveInput < 0)
            {
                transform.localScale = new Vector3(-0.4f, 0.4f, 5);
                isflip = true;
            }
            else
            {
                transform.localScale = new Vector3(0.4f, 0.4f, 5);
                isflip = false;
            }
        }
        else
        {
            animator.SetBool("ismove", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) //무언가에 박았다면
    {
        //animator.SetBool("isground", true); //땅판정 참
        animator.SetBool("isjump", false); //점프판정 거짓

        //점프가 가능한 상태로 만듦
        IsJumping = false; //점프중 거짓
        Isfloor = true;
        if (other.gameObject.CompareTag("downjump"))
        {
            candownjump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        candownjump = false;
    }
    void Jumps()
    {


        //점프중일때 그리고 머가리가 벽에 박았을때 콜라이더 off
        //그중에서 바닥에 닿았을때는 콜라이더 on
        //스페이스 키를 누르면 점프
        if (Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.DownArrow) == false)//스페이스바 눌렸다면
        {
            iswall = false;
            //바닥에 있으면 점프를 실행
            if (!IsJumping) //점프하고있진않다면 공격하고 있지 않다면
            {


                rigid.velocity = Vector2.zero;
                Vector2 jumpvel = new Vector2(0, jumpOwer);
                animator.SetBool("isground", false); //땅바닥 판정 거짓

                IsJumping = true; //점프 판정 참
                rigid.AddForce(jumpvel, ForceMode2D.Impulse);
                //점프 시작
                animator.SetBool("isjump", true); //점프 애니메이션 시작
                Isfloor = false;

            }
            //공중에 떠있는 상태이면 점프하지 못하도록 리턴
            else
            {
                if (isBorder && Input.GetKeyDown(KeyCode.C))
                {
                    iswall = true;
                    print("벽");
                    Isfloor = true;
                    IsJumping = false;
                    wallpos = transform.position;
                }
                return;
            }
        }
        else if (Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.DownArrow) == true && badak)
        {
            if (candownjump)
            {
                canmove = false;
                collidor.enabled = false;
                downjumping = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.DownArrow) == true && iswall)
            iswall = false;


        if (headbak == false && badak && downjumping == false) //바닥에있고 머리 안박고있음
        {
            if (collidor.enabled == false) //콜라이더 꺼져있는데 머리가 안박혀있음
            {
                collidor.enabled = true; //콜라이더 키기
            }
        }

        if (downjumping) //하향점중
        {
            //collidor.enabled = true;
            if (headbak == true&&badak ==false) //바닥에안닿앗고 머리박음 
            {
                canmove = true;
                collidor.enabled = true;
                downjumping = false;//하향끝
            }
        }
       

        if (headbak && IsJumping == true) //머리 박았어요? 점프중임
        {
            collidor.enabled = false; //콜라이더 끄기
        }


    }
    public void keycheck()
    {
        if (Isfloor)
        {
            if (Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.RightArrow) == false)
            {
                Running = false;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow)) // s키를 떼서 트리거 시작
                CanRuns = true;
            if (CanRuns == true)
            {
                if (Input.GetKey(KeyCode.LeftArrow))//달리기 모드 진입
                {
                    Running = true;
                }
                checkRun -= Time.deltaTime;//타이
                if (checkRun <= 0)
                { // 만약 checkRun이 0보다 작아지면
                    CanRuns = false;  // 달리는 판정을 false로 바꿔준다.
                    checkRun = 0.5f;
                    // 그리고 checkRun의 시간은 0.5로 돌려준다.
                }

            }
            if (Input.GetKeyUp(KeyCode.RightArrow)) // d키를 떼서 트리거 시작
                CanRund = true;
            if (CanRund == true)
            {
                if (Input.GetKey(KeyCode.RightArrow))//달리기 모드 진입
                {
                    Running = true;
                }
                checkRun -= Time.deltaTime;//타이
                if (checkRun <= 0)
                { // 만약 checkRun이 0보다 작아지면
                    CanRund = false;  // 달리는 판정을 false로 바꿔준다.
                    checkRun = 0.5f;
                    // 그리고 checkRun의 시간은 0.5로 돌려준다.
                }

            }
        }
    }
    

}