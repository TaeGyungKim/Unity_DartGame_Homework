using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamsongiCtrl : MonoBehaviour
{
    float distance;
    private bool check = false;

    //float rnd_X, rnd_Y;
    //Vector3 wind;
    //bool is_shot = false;
    //private float power = 0;
    //public float power_plus = 100.0f;


    // Start is called before the first frame update
    void Start()
    {
        check = false;
        GetComponent<Rigidbody>().isKinematic = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody>().AddForce(wind);

        //!is_shot &&
        //if ( Input.GetKeyUp(KeyCode.Mouse0))
        //{
        // GetComponent<Rigidbody>().isKinematic = false;
        //  if (timer > 0.05 && !is_shot)
        // {
        //     Shoot(new Vector3(0, 500, 600));
        //     is_shot = true;
        // }
        //}
    }
    public void Shoot(Vector3 dir)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(dir);
    }

    private void OnCollisionEnter(Collision collision)
    {
        check = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<ParticleSystem>().Play();
        //과녁 중앙 위치가 (0, 6.5)
        //거리 측정 = 피타고라스의 정리 Sqrt( (밤송이.x)^2 - (밤송이.y)^2)
        Vector3 collided_position = transform.position;

        
        distance = collided_position.x * collided_position.x + 
            (collided_position.y - 6.5f) * (collided_position.y - 6.5f);

        distance = Mathf.Sqrt(distance);
        Debug.Log(collided_position);
        Debug.Log(distance);

    }

    private void OnGUI()
    {

    }

    public bool CollisonCheck()
    {
        return check;
    }
    public float Distance()
    {
        return distance;
    }


}
