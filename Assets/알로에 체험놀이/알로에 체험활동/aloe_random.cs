using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aloe_random : MonoBehaviour
{
    public GameObject GK1;
    public GameObject GK2;
    public GameObject GK3;

    public GameObject bale1;
    public GameObject bale2;
    public GameObject bale3;

    public GameObject gk1;
    public GameObject gk2;
    public GameObject gk3;

    public GameObject sale1;
    public GameObject sale2;
    public GameObject sale3;

    public GameObject bclone1;
    public GameObject bclone2;
    public GameObject bclone3;
    public GameObject sclone1;
    public GameObject sclone2;
    public GameObject sclone3;

    private int preIndex1 = -1;
    private int preIndex2 = -1;
    private int preIndex3 = -1;
    private int preIndex4 = -1;
    private int preIndex5 = -1;


    // Start is called before the first frame update
    void Start()
    {
        ActivateRrandomObject();
    }

    void ActivateRrandomObject()
    {
        // 모든 오브젝트를 비활성화합니다.
        DeactivateAllObjects();

        // 랜덤으로 선택된 인덱스에 해당하는 오브젝트를 활성화합니다.
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, 6);
        } while (randomIndex == preIndex1 || randomIndex == preIndex2 || randomIndex == preIndex3 || randomIndex == preIndex4 || randomIndex == preIndex5);

        // 선택한 인덱스에 해당하는 오브젝트를 활성화합니다.
        ActivateObject(randomIndex);

        // 이전에 선택한 인덱스를 기억합니다.
        preIndex5 = preIndex4;
        preIndex4 = preIndex3;
        preIndex3 = preIndex2;
        preIndex2 = preIndex1;
        preIndex1 = randomIndex;
    }


    void DeactivateAllObjects()
    {
        GK1.SetActive(false);
        GK2.SetActive(false);
        GK3.SetActive(false);
        gk1.SetActive(false);
        gk2.SetActive(false);
        gk3.SetActive(false);

        bale1.SetActive(false);
        bale2.SetActive(false);
        bale3.SetActive(false);
        sale1.SetActive(false);
        sale2.SetActive(false);
        sale3.SetActive(false);
    }

    void ActivateObject(int index)
    {
        switch (index)
        {
            case 0:
                GK1.SetActive(true);
                bclone1.SetActive(false);
                bale1.SetActive(true);

                break;
            case 1:
                GK2.SetActive(true);
                bclone2.SetActive(false);
                bale2.SetActive(true);
                break;
            case 2:
                GK3.SetActive(true);
                bclone3.SetActive(false);
                bale3.SetActive(true);
                break;
            case 3:
                gk1.SetActive(true);
                sclone1.SetActive(false);
                sale1.SetActive(true);
                break;
            case 4:
                gk2.SetActive(true);
                sclone2.SetActive(false);
                sale2.SetActive(true);
                break;
            case 5:
                gk3.SetActive(true);
                sclone3.SetActive(false);
                sale3.SetActive(true);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 모든 오브젝트가 비활성화된 경우에만 새로운 오브젝트를 활성화합니다.
        if (!GK1.activeSelf && !GK2.activeSelf && !GK3.activeSelf && !gk1.activeSelf && !gk2.activeSelf && !gk3.activeSelf)
        {
            ActivateRrandomObject();
        }
    }
}
