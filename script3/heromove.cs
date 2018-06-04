using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heromove : MonoBehaviour {
    public Animator ani;
    public ingamemanager im;
    public uimanager uiM;
    public monstermanager monsterM;
    public heroattack heroattack;
    private CharacterController control;
    private float fMoveSpeed = 15f;  // 유닛 이동 속도
    private float fRotateSpeed = 1000f; // 유닛 회전 속도
    //private Vector3 vhpmpbar;

	// Use this for initialization
	void Start () {
        //vhpmpbar = new Vector3(0, -5f, 0);
        //vhpmpbar.y = -5f;
        control = GetComponent<CharacterController>();
        im = ingamemanager.Call();
        im.movetarget.gameObject.SetActive(false); // movetarget 안보이게 초기 값
        ani = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        doPicking();
        doTargetMove();
        heroy();
        fautoattack();
        hpmpbar();
	}

    public void hpmpbar()
    {
        //uiM.hpbar.rectTransform.localPosition = transform.position + vhpmpbar;
        //uiM.mpbar.rectTransform.localPosition = transform.position + vhpmpbar + vhpmpbar;
    }

    private void doPicking()
    {
        // 우클릭시
        if (Input.GetMouseButtonDown(0) == true)
        {
            Camera cam = Camera.main;
            RaycastHit hit;
            // 네모난 화면(카메라)에서 클릭을 하면 화면 클릭 지점과 이동하는 지점이 나온다.
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //mSelectObject = hit.collider.gameObject;

            // Physics.Raycast(레이저 시작지점, 레이저 맞은 지점)
            if (Physics.Raycast(ray, out hit) == true)
            {
                switch (hit.collider.tag)
                {
                    case "land":
                        {
                            im.gring.gameObject.SetActive(false);   // 바닥 클릭시 링 사라짐
                            uiM.topimage.gameObject.SetActive(false);   // 바닥 클릭시 정보 사라짐
                        }
                        break;
                    case "monster":
                        {
                            im.gring.gameObject.SetActive(true);    // 왼쪽 클릭시 링
                            uiM.topimage.gameObject.SetActive(true);    // 왼쪽 클릭시 몬스터 이름 및 체력정보
                            for (int i = 0; i < monsterM.tmonster.Length; i++)  // 0 ~ 4 말 5 ~ 9 거미
                            {
                                if (monsterM.tmonster[i].transform.position.Equals(hit.transform.position))
                                {
                                    if (i < 5)
                                    {
                                        uiM.ttopimage.text = "장난감 말";
                                        im.ftmonsterhpbar(im.amonsterhp[i], 11);    // 몬스터 클릭시 체력 표시 (현재 체력, 기본체력)
                                        monsterM.ffring(i, 0);  // 몬스터 클릭시 링
                                        break;
                                    }
                                    if (i > 4 && i < 10)
                                    {
                                        uiM.ttopimage.text = "거미";
                                        im.ftmonsterhpbar(im.amonsterhp[i], 20);
                                        monsterM.ffring(i, 0);
                                        break;
                                    }
                                    if (i > 9 && i < 13)
                                    {
                                        uiM.ttopimage.text = "기사";
                                        im.ftmonsterhpbar(im.amonsterhp[i], 40);
                                        monsterM.ffring(i, 0);
                                        break;
                                    }
                                    if (i > 12)
                                    {
                                        uiM.ttopimage.text = "타워";
                                        im.ftmonsterhpbar(im.amonsterhp[i], 30);
                                        monsterM.ffring(i, 0);
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case "boss":
                        {
                            im.gring.gameObject.SetActive(true);
                            uiM.topimage.gameObject.SetActive(true);
                            for (int i = 0; i < monsterM.tmonster.Length; i++)   // 보스 2마리
                            {
                                if (monsterM.tboos[i].transform.position.Equals(hit.transform.position))  // 보스 몬스터 위치와 클릭위치 비교
                                {
                                    if (i == 0)
                                    {
                                        uiM.ttopimage.text = "아기 용";
                                        im.ftmonsterhpbar(im.abosshp[i], 50);
                                        monsterM.ffring(i, 1);
                                        break;
                                    }
                                    if (i == 1)
                                    {
                                        uiM.ttopimage.text = "보스";
                                        im.ftmonsterhpbar(im.abosshp[i], 100);
                                        monsterM.ffring(i, 1);
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                }
            }
        }
        
        // 좌클릭시
        if (Input.GetMouseButtonDown(1) == true)
        {
            Camera cam = Camera.main;
            RaycastHit hit;
            // 네모난 화면(카메라)에서 클릭을 하면 화면 클릭 지점과 이동하는 지점이 나온다.
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // Physics.Raycast(레이저 시작지점, 레이저 맞은 지점, 레이저 길이)
            if (Physics.Raycast(ray, out hit, Mathf.Infinity) == true && im.hp > 0)
            {
                switch (hit.collider.tag)
                {
                    case "land":
                        {
                            if (uiM.gnpcmenu.activeSelf || uiM.gitemcharacter.activeSelf || uiM.clear.activeSelf || uiM.clear1.activeSelf) // npc 메뉴 창 or 캐릭터 상태창 or clear창 떠있다면 이동안됨
                            {
                                break;
                            }
                            
                            im.binmovetarget = im.movetarget; // binmovetarget 에 먼저 넣어준다 안넣어주면 없다고 에러
                            im.binmovetarget.transform.position = hit.point;  // binmovetarget 이 위치로 이동
                            
                            im.movetarget.gameObject.SetActive(true);  // movetarget 보이게
                            heroattack.stopattack();    // 공격 하는 도중 땅 우클릭시 공격 중지
                            StopCoroutine("doTargetMoverun");
                            StartCoroutine("doTargetMoverun");  // 달리기 시작
                        }
                        break;
                    case "npc":
                        {
                            if (uiM.gnpcmenu.activeSelf || uiM.gitemcharacter.activeSelf) // npc 메뉴 창 or 캐릭터 상태창 떠있다면 이동안됨
                            {
                                break;
                            }

                            for (int i = 0; i < ingamemanager.Call().npc.Length; i++)   // npc 수만큼 반복
                            {
                                if (ingamemanager.Call().npc[i].transform.position.Equals(hit.transform.position))  // npc위치와 클릭위치 비교
                                {
                                    im.binmovetarget = ingamemanager.Call().npc[i];    // 해당 npc binmovetarget 넣어줌
                                }
                            }

                            im.binmovetarget.transform.position = hit.transform.position;  // .transform.position 안써주면 npc올라감
                            StopCoroutine("doTargetMoverun");
                            StartCoroutine("doTargetMoverun");  // 달리기 시작
                        }
                        break;
                    case "monster":
                        {
                            for (int i = 0; i < monsterM.tmonster.Length; i++)   // 몬스터 수만큼 반복
                            {
                                if (monsterM.tmonster[i] == null)
                                {
                                    continue;
                                }
                                if (monsterM.tmonster[i].transform.position.Equals(hit.transform.position))  // 몬스터 위치와 클릭위치 비교
                                {
                                    im.binmovetarget = monsterM.tmonster[i].gameObject;    // 해당 몬스터 binmovetarget 넣어줌
                                    heroattack.attack(i);
                                    ingamemanager.Call().monsternum = i;    // 우클릭 한 몬스터 번호
                                    break;
                                }
                            }

                            im.binmovetarget.transform.position = hit.transform.position;  // .transform.position 안써주면 npc올라감
                            StopCoroutine("doTargetMoverun");
                            StartCoroutine("doTargetMoverun");  // 달리기 시작
                        }
                        break;
                    case "boss":
                        {
                            for (int i = 0; i < monsterM.tmonster.Length; i++)   // 몬스터 수만큼 반복
                            {
                                if (monsterM.tboos[i] == null)
                                {
                                    continue;
                                }
                                if (monsterM.tboos[i].transform.position.Equals(hit.transform.position))  // 보스 몬스터 위치와 클릭위치 비교
                                {
                                    im.binmovetarget = monsterM.tboos[i].gameObject;    // 해당 몬스터 binmovetarget 넣어줌
                                    heroattack.bossattack(i);
                                    ingamemanager.Call().monsternum = i;    // 우클릭 한 몬스터 번호
                                    break;
                                }
                            }

                            im.binmovetarget.transform.position = hit.transform.position;  // .transform.position 안써주면 npc올라감
                            StopCoroutine("doTargetMoverun");
                            StartCoroutine("doTargetMoverun");  // 달리기 시작
                        }
                        break;
                }
            }
        }
    }

    public void fautoattack()   // 버튼 으로 누를때
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            im.keydownup(true); // 작아 진다 (클릭)
            if (im.hp > 0)
            {
                fattack();  // 자동 공격
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            im.keydownup(false);    // 커진다 (업)
        }
    }

    public void fattack()   // 자동 공격
    {
        int hit = 99;   // 보스 일반몬스터 구별
        for (int i = 0; i < monsterM.tmonster.Length; i++)
        {
            
            if (monsterM.tmonster[i] == null)   // 몬스터 생성 전 실행 안됨
            {
                continue;
            }
            
            if (i < 2 && Vector3.Distance(monsterM.tboos[i].transform.position, transform.position) < 2.5f) // 보스 히어로 거리 계산 보스는 배열 0,1 만 있다
            {
                im.binmovetarget = monsterM.tboos[i].gameObject;    // 해당 보스 몬스터 binmovetarget 넣어줌
                heroattack.bossattack(i);
                ingamemanager.Call().monsternum = i;    // 우클릭 한 몬스터 번호
                hit = 101 + i;
                break;
            }
            if (Vector3.Distance(monsterM.tmonster[i].transform.position, transform.position) < 2.5f && monsterM.tmonster[i].gameObject.activeSelf && im.deathlife[i] == true) // 몬스터 히어로 거리 계산, 몬스터 activeSelf true상태 일때
            {
                im.binmovetarget = monsterM.tmonster[i].gameObject;    // 해당 몬스터 binmovetarget 넣어줌
                heroattack.attack(i);
                ingamemanager.Call().monsternum = i;    // 우클릭 한 몬스터 번호
                hit = i;
                break;
            }
        }
        
        if (hit != 99 && hit < 100) // 일반
        {
            im.binmovetarget.transform.position = monsterM.tmonster[hit].transform.position;  // .transform.position 안써주면 npc올라감
            StopCoroutine("doTargetMoverun");
            StartCoroutine("doTargetMoverun");  // 달리기 시작
            hit = 99;
        }

        if (hit > 100)  // 보스
        {
            im.binmovetarget.transform.position = monsterM.tboos[hit - 101].transform.position;  // .transform.position 안써주면 npc올라감
            StopCoroutine("doTargetMoverun");
            StartCoroutine("doTargetMoverun");  // 달리기 시작
        }
    }

    private void doTargetMove()
    {
        if (im.binmovetarget != null)    // binmovetarget 비어있는지 확인
        {
            MoveTarget(); // 유닛 이동

            LookTarget(); // 유닛 회전
        }

        if (im.movetarget.gameObject.activeSelf)   // movetarget 활성화 되면 땅 우클릭 한것
        {
            if (im.binmovetarget != null && Vector3.Distance(im.binmovetarget.transform.position, transform.position) < 0.3f)  // 몬스터 위치와 유닛의 위치 가 0.1 이하 면 movetarget 안보이게
            {
                im.movetarget.gameObject.SetActive(false);
                im.binmovetarget = null;   // 도착시 초기화
                ani.SetFloat("heroRun", 0.0f);    // 달리기 그만
                StopCoroutine("doTargetMoverun");
            }
        }
        else if (!im.movetarget.gameObject.activeSelf) // movetarget 활성화 안되면 땅 우클릭 안한것
        {
            if (im.binmovetarget != null && Vector3.Distance(im.binmovetarget.transform.position, transform.position) < 0.75f) // npc등 이동이라서 가까이 붙으면 안됨
            {
                im.binmovetarget = null;   // 도착시 초기화
                ani.SetFloat("heroRun", 0.0f);
                StopCoroutine("doTargetMoverun");
            }
        }
    }

    private void LookTarget()
    {
        // 회전처리
        Vector3 vDir = (im.binmovetarget.transform.position - transform.position).normalized;   // 방향 벡터 산출
        vDir.y = 0;
        Quaternion q = Quaternion.LookRotation(vDir);                       // 방향벡터 > 쿼터니언
        q.x = 0f;
        Vector3 eu = q.eulerAngles;                                         // 쿼터니언 > 오일러각(0도 360도)
        eu.x = 0f;                                                          // x축 회전을 막는다.                                                        // x축 회전을 막는다.
        Quaternion fixedQ = Quaternion.Euler(eu);                           // 변경한 오일러각 > 쿼터니언
        Quaternion endQ = Quaternion.Slerp(transform.rotation, fixedQ, fRotateSpeed * Time.deltaTime); // Quaternion.Slerp(쿼터니언A, 쿼터니언B, t) A에서 B로 회전 화전속도는 t

        transform.rotation = endQ;
    }

    private void MoveTarget()
    {
        // 앞으로 이동
        Vector3 vDir = Vector3.forward * fMoveSpeed * Time.deltaTime;
        //transform.Translate(vDir);
        Vector3 vFixedMovement = transform.TransformDirection(vDir);
        vFixedMovement *= fMoveSpeed * Time.deltaTime;
        control.Move(vFixedMovement);
    }

    public void heroy()
    {
        Vector3 hero = new Vector3();
        if (im.binmovetarget == null || transform.position.y > 0.5f)    // binmovetarget 비어있는지 확인
        {
            hero = transform.position;
            hero.y = 0;
            transform.position = hero;
        }
    }

    private IEnumerator doTargetMoverun() // IEnumerator 업데이트 처럼 반복 하지만 예약을 반복적으로 걸어서 반복 실행 한다
    {
        while (true)
        {
            ani.SetFloat("heroRun", 1f); // Animator - running 가 된다

            yield return new WaitForEndOfFrame();
        }
    }
    /*
    void Update()
    {
        Vector3 direction = player.position - this.transform.position; // 추가
        if (Vector3.Distance(player.position, this.transform.position) < 5)
        //5m내 접근하면 쳐다보기
        {
            direction.y = 0; // NPC 넘어가지 않도록 Y축 고정
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.LookRotation(direction), 0.1f);
        }
    }
     */
}
