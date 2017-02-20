using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Run,
    Jump,
    D_Jump,
    Death
}

public class Player_Ctrl : MonoBehaviour {

    public PlayerState PS;
    public float Jump_Power = 500f;

    public AudioClip[] Sound;

    void Update()
    {
        GetComponent<Rigidbody>().WakeUp();
        if (Input.GetKeyDown(KeyCode.Space) && PS != PlayerState.Death)
        {

            if (PS == PlayerState.Jump)
            {
                D_Jump();
            }
            if(PS == PlayerState.Run)
            {
                Jump();
            }

         }
    }

    void Jump()
    {
        PS = PlayerState.Jump;
        GetComponent<Rigidbody>().AddForce(new Vector3(0, Jump_Power, 0));
        SoundPlay(2);
    }
    void D_Jump()
    {
        PS = PlayerState.D_Jump;
        GetComponent<Rigidbody>().AddForce(new Vector3(0, Jump_Power, 0));
        SoundPlay(2);
    }
    void Run()
    {
        PS = PlayerState.Run;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(PS != PlayerState.Run && PS != PlayerState.Death)
        {
            Run();
        }
    }

    void CoinGet()
    {
        SoundPlay(0);
    }

    void GameOver()
    {
        PS = PlayerState.Death;
        SoundPlay(1);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Coin")
        {
            Destroy(other.gameObject);
        }

        if(other.gameObject.name == "DeathZone" && PS != PlayerState.Death)
        {
            GameOver();
        }
    }

    void SoundPlay(int Num)
    {
        GetComponent<AudioSource>().clip = Sound[Num];
        GetComponent<AudioSource>().Play();
    }



}
