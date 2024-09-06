using System.Collections;
using System.Collections.Generic;
using UnityEngine.Android;
using ArduinoBluetoothAPI;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using NRKernal;
using NRKernal.NRExamples;

public class B_aloe_cut : MonoBehaviour
{
    //BT 통신용
    private BT_Comm bluetoothHelper;

    public GameObject GK1;

    public GameObject Up;
    public GameObject Down;

    public AudioSource Aloe;
    public AudioSource SAloe;

    public AudioSource realoe;
    public GameObject reaalo;

    public Text bn;
    public Text cbn;
    private bool isTrigger = false;

    public HandModelsManager handModelsManager;

    // Start is called before the first frame update
    void Start()
    {
        bluetoothHelper = BT_Comm.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BKnife_Cut"))
        {
            B_aloe_cut Bcut1 = gameObject.GetComponent<B_aloe_cut>();
            if (Bcut1.isActiveAndEnabled)
            {
                Aloe.Play();
                gameObject.GetComponent<Collider>().enabled = false;
                Tactile_right();

                Disable();

                Invoke("SDisable", 1f);
            }
        }
        if (other.gameObject.CompareTag("SKnife_Cut"))
        {
            gameObject.GetComponent<Collider>().enabled = false;
            handModelsManager.ToggleHandModelsGroup(0);
            Invoke("accenter", 1.2f);

            Invoke("realoee", 5.3f);
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
        GK1.SetActive(false);

        Down.SetActive(true);
        ttrue();
    }

    private void SDisable()
    {
        SAloe.Play();
        Up.SetActive(true);
    }

    private void ttrue()
    {
        isTrigger = true;

        // isTrigger가 true일 때에만 UI 변경
        if (isTrigger)
        {
            int currentCount = int.Parse(bn.text);
            currentCount++;
            bn.text = currentCount.ToString();
            cbn.text = currentCount.ToString();

            // isTrigger를 다시 false로 설정
            isTrigger = false;
        }
    }

    private void accenter()
    {
        //handModelsManager.ToggleHandModelsGroup(0);
        reaalo.SetActive(true);
        realoe.Play();
    }

    private void realoee()
    {
        reaalo.SetActive(false);
        gameObject.GetComponent<Collider>().enabled = true;
        handModelsManager.ToggleHandModelsGroup(10);
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
