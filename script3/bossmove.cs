using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossmove : MonoBehaviour {
    public ingamemanager im;
    private float fMoveSpeed = 2f;  // 몬스터 이동 속도
    private float fRotateSpeed = 1000f; // 몬스터 회전 속도
    public float timer; // 트리거 2번 발생 해서 시간으로 조절
    public Animation ain;
    public float dietimer;  // 몬스터 빨리 사라지는거 시간으로 조절
    public float attackspeed;
    public bool attackbool;

	// Use this for initialization
	void Start () {
        ain = GetComponentInChildren<Animation>();
        im = ingamemanager.Call();
        dietimer = 10f; // 초기 값
	}
	
	// Update is called once per frame
	void Update () {
        dietimer += Time.deltaTime; // 시간체크
        attackspeed += Time.deltaTime; // 시간체크
        dieSetActive();
        bossmovemove();   // 용 이동
        if (timer < 3)
        {
            timer += Time.deltaTime; // 시간체크
        }
        bossattack();
	}

    public void bossattack()  // 용 공격
    {
        if (attackspeed >= 1 && ain.IsPlaying("sj001_skill2") && im.hp > 0) // 1초에 한번 데미지 및 플래그
        {
            im.hpdown(20);  // 데미지 10
            attackspeed = 0;    // 시간 초기화
        }
        else if (im.hp <= 0)
        {
            ain.Play("sj001_wait");
            hpreset();
        }
    }

    public void bossmovemove()
    {
        if (Vector3.Distance(im.hero.transform.position, transform.position) < 2.5f && Vector3.Distance(im.hero.transform.position, transform.position) > 0.6f) // 몬스터 캐릭터 거리(몬스터 다가감)
        {
            ain.Play("sj001_run");
            LookTarget();
            MoveTarget();
        }

        if (Vector3.Distance(im.hero.transform.position, transform.position) < 0.6f)
        {
            ain.Play("sj001_skill2");
        }
    }

    private void LookTarget()
    {
        // 회전처리
        Vector3 vDir = (im.hero.transform.position - transform.position).normalized;   // 방향 벡터 산출
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
        transform.Translate(vDir);
    }

    // 트리거 지역 진입시
    void OnTriggerEnter(Collider _col)
    {
        // im.attackbool == true heroattack - 공격 모션 실행시 true (몬스터 붙을때 트리거 실행 방지)
        // timer >= im.attackspeed 한번에 트리거 2번 발생 해서 시간으로 조절 (공격 속도 빨라지면 시간 줄여야 된다)
        if (_col.tag == "sword" && im.attackbool == true && timer >= im.attackspeed && im.binmovetarget == null && 0 == im.monsternum) // im.binmovetarget == null 캐릭터 지나가는데 트리거 발생 방지, 현재 몬스터 순번 우클릭 몬스터 번호 비교
        {
            horsehp(im.attackskill()); // 데미지 스킬 (공격횟수3 1번공격1)
            im.sattack();
            timer = 0f; // 공격 하면 0으로 초기화
        }

        if (_col.tag == "fire") // 스킬 불
        {
            horsehp(20);
        }
    }

    public void horsehp(int i)
    {
        im.abosshp[0] = im.abosshp[0] - im.fcritical(i);  // 크리티컬 im.fcritical(i)
        im.fmonsterhpbar(im.abosshp[0], 50, false); // 몬스터 공격할때 정보 표시 (현재체력, 기본체력, 죽는거 체크)
        if (im.abosshp[0] <= 0)
        {
            die();
            ingamemanager.Call().diemonster();  // 공격 중지
            ingamemanager.Call().resetcho(0);   // 보스 죽으면 리셋
            dietimer = 0f;
        }
    }

    public void die()
    {
        hpreset();
        ain.Play("sj001_die");   // 죽는 모션
    }

    public void hpreset()   // 죽으면 다시 hp 복구 안하면 hp줄어든 체로 부활
    {
        im.abosshp[0] = 50; // 체력 리셋
        im.fmonsterhpbar(0, 50, true);  // 몬스터 죽을때 정보 사라짐
        im.gring.SetActive(false);
    }

    public void dieSetActive()
    {
        if (dietimer > 0.9f && dietimer < 1.5f) // 죽는 모션을 위한 시간 벌기 0.9 이상 1.5 이하
        {
            gameObject.SetActive(false);    // 몬스터 사라짐
            dietimer = 10f; // 10으로 초기화
        }
    }
}
