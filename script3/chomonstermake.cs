using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chomonstermake : MonoBehaviour {
    public monstermanager monsterM;
    public horsemove horsemove;
    public spidermove spidermove;
    public knightmove knightmove;
    public towerattack towerattack;
    public Vector3 vhorse;  // 몬스터 생성 위치 지정

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void monstermake(int z)
    {
        Vector3 vtknight = new Vector3();
        if (monsterM.tmonster[0] == null)
        {
            for (int i = z; i < monsterM.tmonster.Length; i++) // 말, 거미 몬스터 처음 생성 (0~4말 5~9 거미)
            {
                if (i < 5)
                {
                    monsterM.bingameobject = monsterM.horse;    // 말 몬스터 -> 빈오브젝트
                    monsterM.bintransform = monsterM.horsepo;   // 말 몬스터위치 -> 빈트렌스폼
                    horsemove.monsternum = i;
                    ingamemanager.Call().amonsterhp[i] = 11;
                }
                else if(i > 4 && i < 10)
                {
                    monsterM.bingameobject = monsterM.spider;   // 거미 몬스터 -> 빈오브젝트
                    monsterM.bintransform = monsterM.hspider;   // 거미 몬스터위치 -> 빈트렌스폼
                    spidermove.monsternum = i;
                    ingamemanager.Call().amonsterhp[i] = 20;
                }

                if (i > 9 && i < 13)
                {
                    monsterM.bingameobject = monsterM.knight;    // 기사 몬스터 -> 빈오브젝트
                    monsterM.bintransform = monsterM.tknight;   // 기사 몬스터위치 -> 빈트렌스폼
                    knightmove.monsternum = i;
                    ingamemanager.Call().amonsterhp[i] = 40;
                }
                else if (i > 12)
                {
                    monsterM.bingameobject = monsterM.tower;   // 타워 몬스터 -> 빈오브젝트
                    monsterM.bintransform = monsterM.ttower;   // 타워 몬스터위치 -> 빈트렌스폼
                    towerattack.monsternum = i;
                    ingamemanager.Call().amonsterhp[i] = 30;
                }

                monsterM.tmonster[i] = Instantiate(monsterM.bingameobject).transform;   // 생성
                monsterM.tmonster[i].transform.position = monsterM.bintransform.transform.position; // 위치값
                monsterM.memorypo[i] = monsterM.tmonster[i].transform.position; // 던전 재도전시 사용할 위치값
                ingamemanager.Call().deathlife[i] = true;   // 몬스터 죽음삶 체크 죽는모션 공격 방지

                if (i < 10) // 말, 거미
                {
                    if (i % 2 != 0)
                    {
                        vhorse.x = 1.7f;  // 위, 아래
                        monsterM.bintransform.transform.position = monsterM.bintransform.transform.position + vhorse;   // 같은 자리 안생기게
                    }
                    else
                    {
                        vhorse.x = -1.7f;
                        monsterM.bintransform.transform.position = monsterM.bintransform.transform.position + vhorse;   // 같은 자리 안생기게
                    }
                }

                if (i > 9 && i < 13)    // 기사
                {
                    if (i % 2 != 0)
                    {
                        vhorse.x = 1.7f;  // 위, 아래
                        monsterM.bintransform.transform.position = monsterM.bintransform.transform.position + vhorse;   // 같은 자리 안생기게
                        if (i == 11)
                        {
                            monsterM.bintransform.transform.position = vtknight + vhorse;   // 기사 12를 앞라인으로
                        }
                    }
                    else
                    {
                        vhorse.x = -1.7f;
                        monsterM.bintransform.transform.position = monsterM.bintransform.transform.position + vhorse;   // 같은 자리 안생기게
                        if (i == 10)
                        {
                            vtknight = monsterM.bintransform.transform.position;    // 기사 12를 위한 자리 저장
                            monsterM.bintransform.transform.position = monsterM.tknight1.transform.position;   // 기사 11을 뒤로 보내기 위해
                        }
                    }
                }

                if (i > 12 && i < 15)   // 포탑 1라인 3개
                {
                    vhorse.x = Random.Range(-1f, 1f);  // 위, 아래
                    monsterM.bintransform.transform.position = monsterM.bintransform.transform.position + vhorse;
                }
                else if(i == 15)
                {
                    vtknight = monsterM.ttower1.transform.position;
                    monsterM.bintransform.transform.position = vtknight;
                }

                if (i > 15) // 포탑 2라인 4개
                {
                    vhorse.x = Random.Range(-1f,1f);  // 위, 아래
                    monsterM.bintransform.transform.position = monsterM.bintransform.transform.position + vhorse;
                }
                

                LookTarget(i);  // 몬스터 회전
                if (i == 7)
                {
                    vhorse.z = 0.1f;   // 마지막 거미
                }
                else if(i < 13 && i != 7)
                {
                    vhorse.z = 1f;  // 왼쪽으로
                }

                if (i > 11 && i < 13)
                {
                    vhorse.z = 1.5f;  // 왼쪽으로
                }
                else if (i > 14)
                {
                    vhorse.z = 1.2f;
                }
            }

            monsterM.tboos[0] = Instantiate(monsterM.boss1).transform;  // 보스
            monsterM.tboos[0].transform.position = monsterM.boss1po.transform.position;
            ingamemanager.Call().abosshp[0] = 50;

            monsterM.tboos[1] = Instantiate(monsterM.skull).transform;  // 보스
            monsterM.tboos[1].transform.position = monsterM.tskull.transform.position;
            ingamemanager.Call().abosshp[1] = 100;
        }
        else
        {
            for (int i = z; i < monsterM.tmonster.Length; i++)  // 던전 재도전
            {
                if (z == 0 && i > 9)
                {
                    break;  // 던전 1 일때 던전 2 몬스터 생성 안되게
                }

                monsterM.tmonster[i].gameObject.SetActive(true);
                monsterM.tmonster[i].transform.position = monsterM.memorypo[i]; // 던전 생성할때 만든것

                LookTarget(i);  // 몬스터 회전
                if (i == 7)
                {
                    vhorse.z = 0.1f;   // 마지막 거미
                }
                else
                {
                    vhorse.z = 1f;  // 왼쪽으로
                }
            }
            
            if (z == 0)
            {
                monsterM.tboos[0].gameObject.SetActive(true);   // 던전 1 보스
                monsterM.tboos[0].transform.position = monsterM.boss1po.transform.position;
            }
            else
            {
                monsterM.tboos[1].gameObject.SetActive(true);   // 던전 2보스
                monsterM.tboos[1].transform.position = monsterM.tskull.transform.position;
            }
            
        }
    }

    private void LookTarget(int i)  // 몬스터 회전
    {
        float xrandom = Random.Range(0.0f, 100.0f);
        float yrandom = Random.Range(0.0f, 100.0f);

        Vector3 vrandom = new Vector3(xrandom, 0, yrandom);
        // 회전처리
        Vector3 vDir = (vrandom - monsterM.tmonster[i].transform.position).normalized;   // 방향 벡터 산출
        Quaternion q = Quaternion.LookRotation(vDir);                       // 방향벡터 > 쿼터니언
        q.x = 0f;
        Vector3 eu = q.eulerAngles;                                         // 쿼터니언 > 오일러각(0도 360도)
        eu.x = 0f;                                                          // x축 회전을 막는다.                                                        // x축 회전을 막는다.
        Quaternion fixedQ = Quaternion.Euler(eu);                           // 변경한 오일러각 > 쿼터니언
        Quaternion endQ = Quaternion.Slerp(transform.rotation, fixedQ, 35.0f * Time.deltaTime); // Quaternion.Slerp(쿼터니언A, 쿼터니언B, t) A에서 B로 회전 화전속도는 t

        monsterM.tmonster[i].transform.rotation = endQ;
    }
}
