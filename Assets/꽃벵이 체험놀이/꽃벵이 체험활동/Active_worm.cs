using ArduinoBluetoothAPI;
using NRKernal.NRExamples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Active_worm : MonoBehaviour
{
    public GameObject worm1;
    public GameObject worm2;
    public GameObject worm3;
    public GameObject collider1;
    public GameObject collider2;
    public GameObject collider3;
    private bool active = false;
    public HandModelsManager handModelsManager;
    public GameObject leaf1;
    public GameObject leaf2;
    public GameObject leaf3;
    public int num = 0;

    private BT_Comm bluetoothHelper;
    private void Start()
    {
        num = 0;
        bluetoothHelper = BT_Comm.Instance;
    }
    private void Update()
    {
        if (Check_leafCol.CheckLeaf1 == true)
        {
            handModelsManager.ToggleHandModelsGroup(12);
            worm1.SetActive(true);
            collider1.SetActive(false);

            char temp_mode_stx = '<';
            char temp_mode_etx = '>';
            byte[] af = BitConverter.GetBytes(temp_mode_stx);
            byte[] a = BitConverter.GetBytes('4');
            byte[] al = BitConverter.GetBytes(temp_mode_etx);
            //byte[] b = BitConverter.GetBytes(53);
            byte[] c = BitConverter.GetBytes('0');

            byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x51, c[0], 0xFE };
            bluetoothHelper.SendData(bytestosend);

            leaf1.GetComponent<Active_worm>().num = 1;
            leaf2.GetComponent<Active_worm>().num = 1;
            leaf3.GetComponent<Active_worm>().num = 1;

            Check_leafCol.CheckLeaf1 = false;
        }
        if (Check_leafCol.CheckLeaf2 == true)
        {
            handModelsManager.ToggleHandModelsGroup(12);
            worm2.SetActive(true);
            collider2.SetActive(false);

            char temp_mode_stx = '<';
            char temp_mode_etx = '>';
            byte[] af = BitConverter.GetBytes(temp_mode_stx);
            byte[] a = BitConverter.GetBytes('4');
            byte[] al = BitConverter.GetBytes(temp_mode_etx);
            //byte[] b = BitConverter.GetBytes(53);
            byte[] c = BitConverter.GetBytes('0');

            byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x51, c[0], 0xFE };
            bluetoothHelper.SendData(bytestosend);

            leaf1.GetComponent<Active_worm>().num = 2;
            leaf2.GetComponent<Active_worm>().num = 2;
            leaf3.GetComponent<Active_worm>().num = 2;

            Check_leafCol.CheckLeaf2 = false;
        }
        if (Check_leafCol.CheckLeaf3 == true)
        {
            handModelsManager.ToggleHandModelsGroup(12);
            worm3.SetActive(true);
            collider3.SetActive(false);

            char temp_mode_stx = '<';
            char temp_mode_etx = '>';
            byte[] af = BitConverter.GetBytes(temp_mode_stx);
            byte[] a = BitConverter.GetBytes('4');
            byte[] al = BitConverter.GetBytes(temp_mode_etx);
            //byte[] b = BitConverter.GetBytes(53);
            byte[] c = BitConverter.GetBytes('0');

            byte[] bytestosend = { af[0], a[0], al[0], 0xFF, 0x51, c[0], 0xFE };
            bluetoothHelper.SendData(bytestosend);

            leaf1.GetComponent<Active_worm>().num = 3;
            leaf2.GetComponent<Active_worm>().num = 3;
            leaf3.GetComponent<Active_worm>().num = 3;

            Check_leafCol.CheckLeaf3 = false;
        }
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("leaf_worm"))
        {

        }
    }
}
