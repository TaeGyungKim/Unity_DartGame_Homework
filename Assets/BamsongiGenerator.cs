using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BamsongiGenerator : MonoBehaviour
{
    private float power = 0;
    public float power_plus = 500.0f;
    public GameObject bamsongi_prefab;
    private GameObject bamsongi;
    Vector3 shooting_ray;

    //바람
    float rnd_X, rnd_Y;
    Vector3 wind;

    //점수
    float distance = 0;
    int score = 0;
    bool score_check = false;
    bool collision_check = false;
    //카운트로 엔딩 분기 체크
    int shoot_count = 0;
    bool ending_check = false;
   

    private void Start()
    {
        Debug.Log("Game Start");
        score = 0;

    }

    // Update is called once per frame
    void Update()
    {
       if(shoot_count >= 5) {
            ending_check = true;

            if(Input.GetKeyDown(KeyCode.R))
                 SceneManager.LoadScene("main");
        }

       //밤송이 생성하면서
       //풍향 풍속 부여 및
       // 충돌 및 점수 부여 플래그 초기화
        if (!ending_check && Input.GetMouseButtonDown(0))
        {
            //밤송이 생성
            bamsongi = Instantiate(bamsongi_prefab);

            collision_check = false;
            score_check = false;

            //무작위 풍향 풍속 부여
            rnd_X = Random.Range(-10.0f, 10.0f);
            rnd_Y = Random.Range(-5.0f, 5.0f);
            wind = new Vector3(rnd_X, rnd_Y, 0);
            bamsongi.GetComponent<Rigidbody>().AddForce(wind);

  
           



        }

        //마우스 누르는 동안 날아갈 힘을 가함
        if (Input.GetMouseButton(0))
        {
            power += power_plus * Time.deltaTime;


        }

        //마우스를 누르면 발사할 스크린 좌표 결정
        //밤송이 발사 및 카운트
        if (!ending_check && Input.GetMouseButtonUp(0))
        {//new Vector3(0, power, power)

            //add 2022-11-30
            //스크린 좌표에서 ray를 쏴서 물체의 좌표를 산출
            Ray screen_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Vector3
            shooting_ray = screen_ray.direction;
            // bamsongi.GetComponent<BamsongiCtrl>().Shoot(shooting_ray * power);

            bamsongi.GetComponent<BamsongiCtrl>().Shoot(shooting_ray * power);
            power = 0;
            shoot_count++; //카운트
            Destroy(bamsongi, 3.0f); //발사 3초후 삭제
        }


        if (bamsongi != null)
            collision_check = bamsongi.GetComponent<BamsongiCtrl>().CollisonCheck();

        if (collision_check)
        {
            
            wind = new Vector3(0f, 0f, 0f);


            if (!score_check)
            {
                distance = bamsongi.GetComponent<BamsongiCtrl>().Distance();

                if (distance < 0.4f) score += 100;
                else if (distance < 0.8f) score += 80;
                else if (distance < 1.2f) score += 60;
                else if (distance < 1.6f) score += 40;
                else if (distance < 2.0f) score += 20;

                score_check = true;
            }
        }
            

    }

    private void OnGUI()
    {
        if (wind != new Vector3(0f, 0f, 0f))
            GUI.Label(new Rect(40, Screen.height - 20, 256, 32), wind.ToString() +
                " 방향으로 바람 부는 중");

        GUI.Label(new Rect(40, Screen.height - 100, 256, 32), "발사 횟수 :" + shoot_count.ToString());

        if(ending_check)
            GUI.Label(new Rect(Screen.width / 2 - 50, 80, 140, 20), "Press 'R' to Continue!!");

        GUI.Label(new Rect(Screen.width * 5 / 6, 40, 140, 20), "점수 : "+score.ToString());

    }

}