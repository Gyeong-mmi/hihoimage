using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using System;

public class Trigger_Bhorse : MonoBehaviour
{
    private Animator _eatAnim;
    private bool _eating = false;
    private Animator _shakeAnim;
    private bool _shaking = false;

    public AudioSource Horse;
    public AudioSource recarrot;
    public GameObject recarro;

    private bool vibrationActive = false; // 진동 상태를 추적하기 위한 변수

    //BT 통신용
    private BT_Comm bluetoothHelper;

    // Start is called before the first frame update
    void Start()
    {
        bluetoothHelper = BT_Comm.Instance;

        // Animator 컴포넌트를 가져와서 변수에 할당
        _eatAnim = GetComponent<Animator>();
        _shakeAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("B_Carrot"))
        {
            gameObject.GetComponent<Collider>().enabled = false;

            Horse.Play();
            _eating = true;
            _shaking = false;
            _eatAnim.SetBool("Eating", _eating);
            _shakeAnim.SetBool("Shaking", _shaking);
            if (!vibrationActive)
            {
                vibrationActive = true;
                Tactile_right();
                Invoke("OFF", 1f); // 진동을 끄는 시간을 1초 후로 조정
            }
        }

        if (other.gameObject.CompareTag("S_Carrot"))
        {
            gameObject.GetComponent<Collider>().enabled = false;
            _eating = false;
            _shaking = true;
            _eatAnim.SetBool("Eating", _eating);
            _shakeAnim.SetBool("Shaking", _shaking);
            Invoke("disapp", 4f);
            Invoke("accenter", 4.8f);
            Invoke("recca", 9f);
        }
    }

    private void disapp()
    {
        recarrot.Play();
    }

    private void accenter()
    {
        recarro.SetActive(true);
        Transform parent = transform.parent;
        Random_Horse script = parent.GetComponent<Random_Horse>();
        script.enabled = false;
    }

    private void recca()
    {
        recarro.SetActive(false);
        gameObject.GetComponent<Collider>().enabled = true;
        Transform parent = transform.parent;
        Random_Horse script = parent.GetComponent<Random_Horse>();
        script.enabled = true;
    }

    private void OFF()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        Tactile_off();
        vibrationActive = false; // 진동 상태를 리셋
    }

    public void Tactile_right()
    {
        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('2');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x52, 0x00, 0xFE };

        bluetoothHelper.SendData(bytestosend);
    }

    public void Tactile_off()
    {
        char temp_mode_stx = '<';
        char temp_mode_etx = '>';
        byte[] af = BitConverter.GetBytes(temp_mode_stx);
        byte[] a = BitConverter.GetBytes('2');
        byte[] al = BitConverter.GetBytes(temp_mode_etx);

        byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x00, 0x00, 0xFE };

        bluetoothHelper.SendData(bytestosend);
    }
}