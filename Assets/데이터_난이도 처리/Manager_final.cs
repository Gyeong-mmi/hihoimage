using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Manager_final : CLASS_XmlData
{
    public static Manager_ResultInDetail instance = null;

    public List<Result_IndetailData> OriginDataList;
    public List<Result_IndetailData> NewDataList;
    public string filePath;

    public GameObject low;
    public GameObject mid;
    public GameObject hard;
    public GameObject final_active;
    public GameObject ffinall;

    private List<string> String_Data_attribute = new List<string>();

    [Header("[RESULT IN DETAIL INFORMATION]")]
    [SerializeField]
    public string ID;
    public string Name;
    public string Session;
    public string Date;
    public List<string> Data_Indetail = new List<string>();

    //꽃벵이
    float[] worm_AT = new float[] {
         9.02f, 9.69f, 10.83f, 14f,
         27.5f, 28.7f, 29.2f, 29.8f,
         30.85f, 31.35f, 33f, 35.5f,
         39f, 39.5f, 40.5f, 44f,
         49.5f, 50.02f, 51.13f, 55.46f,
         56.05f, 56.53f
    };
    float[] worm_IAT = new float[] {
         9.02f, 9.69f, 10.83f, 17f,
         28.2f, 28.7f, 29.2f, 30.3f,
         30.85f, 31.35f, 35f, 37.5f,
         39f, 39.5f, 40.5f, 47f,
         49.5f, 50.02f, 51.13f, 55.46f,
         56.05f, 56.53f
    };

    //당근
    float[] carrot_AT = new float[] {
         12.7f, 17f, 19.8f, 20.4f,
         21f, 21.4f, 24f, 24.4f,
         24.8f, 25f, 25.2f, 25.6f,
         25.8f, 28.6f, 29.2f, 29.8f,
         30.2f, 32.8f, 33.2f, 33.6f,
         33.8f, 34f, 34.4f, 34.6f,
         43.2f, 46.6f, 46.9f, 47.2f,
         51.4f, 51.7f, 52f
    };
    float[] carrot_IAT = new float[] {
         12.7f, 17f, 19.8f, 20.4f,
         21f, 21.4f, 24f, 24.4f,
         24.8f, 25f, 25.2f, 25.6f,
         25.8f, 28.6f, 29.2f, 29.8f,
         30.2f, 32.8f, 33.2f, 33.6f,
         33.8f, 34f, 34.4f, 34.6f,
         43.2f, 46.6f, 46.9f, 47.2f,
         51.4f, 51.7f, 52f
    };

    //옥수수
    float[] corn_AT = new float[] {
         9f, 9.5f, 10f, 10.5f,
         11f, 11.5f, 12.1f, 12.6f,
         13.2f, 13.8f, 14.4f, 14.8f,
         16.5f, 16.7f, 16.9f, 17.6f,
         18.1f, 18.7f, 19.2f, 25.2f,
         25.5f, 25.7f, 28.4f, 29f,
         29.5f, 30f, 33.5f, 38f,
         42.5f, 42.8f, 43.1f, 43.8f,
         44.3f, 44.9f, 45.4f, 46f,
         46.5f, 48f, 48.6f, 50.9f,
         52.5f, 53f, 55.2f
    };
    float[] corn_IAT = new float[] {
         9f, 9.5f, 10f, 10.5f,
         11f, 11.5f, 12.1f, 12.6f,
         13.2f, 13.8f, 14.4f, 14.8f,
         16.5f, 16.7f, 16.9f, 17.6f,
         18.1f, 18.7f, 19.2f, 25.2f,
         25.5f, 25.7f, 28.4f, 29f,
         29.5f, 30f, 34.5f, 39f,
         42.5f, 42.8f, 43.1f, 43.8f,
         44.3f, 44.9f, 45.4f, 46f,
         46.5f, 48f, 48.6f, 50.9f,
         52.5f, 53f, 55.2f
    };

    //알로에
    float[] aloe_AT = new float[] {
         13.38f, 13.65f, 13.97f, 17f,
         17.61f, 18.21f, 25.38f, 26.03f,
         33.75f, 34.28f, 34.91f, 35.49f,
         36.09f, 36.73f, 37.35f, 37.95f,
         38.58f, 39.12f, 39.79f, 40.33f,
         41.33f, 41.54f, 42.08f, 42.68f,
         50.59f, 60.13f, 69.75f, 70.39f,
         71.01f, 74.63f, 75.16f, 75.76f
    };
    float[] aloe_IAT = new float[] {
         13.38f, 13.65f, 13.97f, 17f,
         17.61f, 18.21f, 25.38f, 26.03f,
         33.75f, 34.28f, 34.91f, 35.49f,
         36.09f, 36.73f, 37.35f, 37.95f,
         38.58f, 39.12f, 39.79f, 40.33f,
         41.33f, 41.54f, 42.08f, 42.68f,
         57.21f, 66.86f, 69.75f, 70.39f,
         71.01f, 74.63f, 75.16f, 75.76f
    };

    void Start()
    {
        Init_RID();

        // RESULT_INDETAIL 파일 경로 설정
        filePath = Path.Combine(Application.persistentDataPath, "RESULT_INDETAIL.xml");
        Check_XmlFile("RESULT_INDETAIL");

        if (filePath != null)
        {
            // RESULT_INDETAIL 파일 읽기
            OriginDataList = Read();
            NewDataList = Read();  //새로 추가

            // 필터링된 데이터 리스트 생성
            List<Result_IndetailData> filteredDataList = FilterData(OriginDataList);
            List<Result_IndetailData> filteredDataList2 = FilterData2(filteredDataList);
            List<Result_IndetailData> LastDataList1 = FilterData3(filteredDataList);
            List<Result_IndetailData> LastDataList2 = FilterData4(filteredDataList2);
            List<Result_IndetailData> FinalDataList = FilterData5(LastDataList1, LastDataList2);

            // RESULT 파일에 필터링된 데이터 쓰기
            WriteToXml(FinalDataList);
            ffinall.SetActive(true);
        }
    }

    private void Update()
    {
        if (final_active.activeSelf)
        {
            Init_RID();

            // RESULT_INDETAIL 파일 경로 설정
            filePath = Path.Combine(Application.persistentDataPath, "RESULT_INDETAIL.xml");
            Check_XmlFile("RESULT_INDETAIL");

            if (filePath != null)
            {
                // RESULT_INDETAIL 파일 읽기
                //OriginDataList = Read();
                OriginDataList = NewDataList; //새로 추가
                Debug.Log("오리진 갯수" + OriginDataList.Count);
                NewDataList = Read(); //새로 추가
                Debug.Log("뉴 갯수" + NewDataList.Count);

                // 필터링된 데이터 리스트 생성
                List<Result_IndetailData> filteredDataList = FilterData(OriginDataList);
                List<Result_IndetailData> filteredDataList2 = FilterData2(filteredDataList);
                List<Result_IndetailData> LastDataList1 = FilterData3(filteredDataList);
                List<Result_IndetailData> LastDataList2 = FilterData4(filteredDataList2);
                List<Result_IndetailData> FinalDataList = FilterData5(LastDataList1, LastDataList2);

                // RESULT_score2 파일에 필터링된 데이터 쓰기
                WriteToXml(FinalDataList);
                final_active.SetActive(false);
                ffinall.SetActive(true);
            }
        }
    }

    public override void Write()
    {
        XmlDocument Document = new XmlDocument();
        XmlElement ItemListElement = Document.CreateElement("Result_Indetail_data");
        Document.AppendChild(ItemListElement);

        foreach (Result_IndetailData data in NewDataList)
        {
            XmlElement ItemElement = Document.CreateElement("Result_Indetail_data");
            ItemElement.SetAttribute("ID", data.ID);
            ItemElement.SetAttribute("Name", data.Name);
            ItemElement.SetAttribute("Date", data.Date);
            ItemElement.SetAttribute("Session", data.Session);

            for (int i = 0; i < data.Data.Count; i++)
            {
                ItemElement.SetAttribute(String_Data_attribute[i], data.Data[i]);
            }
            ItemListElement.AppendChild(ItemElement);
        }
        Document.Save(filePath);
    }

    public List<Result_IndetailData> Read()
    {
        XmlDocument Document = new XmlDocument();
        Document.Load(filePath);
        XmlElement ItemListElement = Document["Result_Indetail_data"];
        List<Result_IndetailData> ItemList = new List<Result_IndetailData>();

        foreach (XmlElement ItemElement in ItemListElement.ChildNodes)
        {
            Result_IndetailData Item = new Result_IndetailData();
            Item.ID = ItemElement.GetAttribute("ID");
            Item.Name = ItemElement.GetAttribute("Name");
            Item.Date = ItemElement.GetAttribute("Date");
            Item.Session = ItemElement.GetAttribute("Session");

            for (int i = 0; i < 2000; i++)
            {
                if (string.IsNullOrEmpty(ItemElement.GetAttribute(String_Data_attribute[i])))
                {
                    //Debug.Log("Data Empty" + i);
                }
                else
                {
                    Item.Data.Add(ItemElement.GetAttribute(String_Data_attribute[i]));
                    //Debug.Log("Data_attribute : " + String_Data_attribute[i] + "item : " + Item.Data[i]);
                }
            }
            //Debug.Log("Data count : " + Item.Data.Count);
            ItemList.Add(Item);
        }
        return ItemList;
    }

    public void Add_RIDdata(float data_1 = -999)   //idx+데이터 3가지
    {
        string Data_merged;

        if (data_1 == -999) return;

        Data_merged = data_1.ToString();

        //데이터 리스트에 추가
        Data_Indetail.Add(Data_merged);
    }

    List<Result_IndetailData> FilterData(List<Result_IndetailData> originalDataList)
    {
        List<Result_IndetailData> filteredDataList = new List<Result_IndetailData>();

        float diff = 0;
        if (low.GetComponent<btn_low>().isLow && !mid.GetComponent<btn_mid>().isMid && !hard.GetComponent<btn_hard>().isHard)
        {
            diff = 0.3f;
        }
        else if (!low.GetComponent<btn_low>().isLow && mid.GetComponent<btn_mid>().isMid && !hard.GetComponent<btn_hard>().isHard)
        {
            diff = 0.2f;
        }
        else if (!low.GetComponent<btn_low>().isLow && !mid.GetComponent<btn_mid>().isMid && hard.GetComponent<btn_hard>().isHard)
        {
            diff = 0.1f;
        }
        else
        {
            diff = 0.3f;
        }

        Debug.Log("diff: " + diff);

        // 여기서 조건에 따라 필터링된 데이터를 구합니다.
        foreach (Result_IndetailData data in originalDataList)
        {
            // 필터링된 데이터 리스트에 추가
            Result_IndetailData filteredData = new Result_IndetailData();
            filteredData.ID = data.ID;
            filteredData.Name = data.Name;
            filteredData.Date = data.Date;
            filteredData.Session = data.Session;

            if (data.Session == "0")
            {
                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(',');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        float dV;
                        if (float.TryParse(dataValues[1], out dV))
                        {
                            for (int j = 0; j < 22; j++)
                            {
                                if (dV >= worm_AT[j] - diff && dV <= worm_IAT[j] + diff)
                                {
                                    // 필터링된 데이터 추가
                                    filteredData.Data.Add(i + ":" + (j + 1) + ":" + data.Data[i]);
                                    break; // 필터링 조건에 맞는 경우에만 추가하고 다음 데이터로 이동
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
            }

            else if (data.Session == "1")
            {
                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(',');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        float dV;
                        if (float.TryParse(dataValues[1], out dV))
                        {
                            for (int j = 0; j < 31; j++)
                            {
                                if (dV >= carrot_AT[j] - diff && dV <= carrot_IAT[j] + diff)
                                {
                                    // 필터링된 데이터 추가
                                    filteredData.Data.Add(i + ":" + (j + 1) + ":" + data.Data[i]);
                                    break; // 필터링 조건에 맞는 경우에만 추가하고 다음 데이터로 이동
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
            }

            else if (data.Session == "2")
            {
                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(',');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        float dV;
                        if (float.TryParse(dataValues[1], out dV))
                        {
                            for (int j = 0; j < 43; j++)
                            {
                                if (dV >= corn_AT[j] - diff && dV <= corn_IAT[j] + diff)
                                {
                                    // 필터링된 데이터 추가
                                    filteredData.Data.Add(i + ":" + (j + 1) + ":" + data.Data[i]);
                                    break; // 필터링 조건에 맞는 경우에만 추가하고 다음 데이터로 이동
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
            }

            else if (data.Session == "3")
            {
                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(',');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        float dV;
                        if (float.TryParse(dataValues[1], out dV))
                        {
                            for (int j = 0; j < 32; j++)
                            {
                                if (dV >= aloe_AT[j] - diff && dV <= aloe_IAT[j] + diff)
                                {
                                    // 필터링된 데이터 추가
                                    filteredData.Data.Add(i + ":" + (j + 1) + ":" + data.Data[i]);
                                    break; // 필터링 조건에 맞는 경우에만 추가하고 다음 데이터로 이동
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
            }

            // 필터링된 데이터가 있는 경우에만 리스트에 추가
            if (filteredData.Data.Count > 0)
            {
                filteredDataList.Add(filteredData);
            }
        }

        return filteredDataList;
    }

    List<Result_IndetailData> FilterData2(List<Result_IndetailData> originalDataList)
    {
        List<Result_IndetailData> filteredDataList = new List<Result_IndetailData>();

        // 평균 값을 저장할 변수
        float sum0 = 0;
        int count0 = 0;
        float sum1 = 0;
        int count1 = 0;
        float sum2 = 0;
        int count2 = 0;
        float sum3 = 0;
        int count3 = 0;

        float diff1 = 0;
        float diff2 = 0;

        if (low.GetComponent<btn_low>().isLow && !mid.GetComponent<btn_mid>().isMid && !hard.GetComponent<btn_hard>().isHard)
        {
            diff1 = 0.1f;
            diff2 = -0.1f;
        }
        else if (!low.GetComponent<btn_low>().isLow && mid.GetComponent<btn_mid>().isMid && !hard.GetComponent<btn_hard>().isHard)
        {
            diff1 = 0f;
            diff2 = 0f;
        }
        else if (!low.GetComponent<btn_low>().isLow && !mid.GetComponent<btn_mid>().isMid && hard.GetComponent<btn_hard>().isHard)
        {
            diff1 = -0.1f;
            diff2 = 0.1f;
        }
        else
        {
            diff1 = 0f;
            diff2 = 0f;
        }

        foreach (Result_IndetailData data in originalDataList)
        {
            // 필터링된 데이터 리스트에 추가
            Result_IndetailData filteredData = new Result_IndetailData();
            filteredData.ID = data.ID;
            filteredData.Name = data.Name;
            filteredData.Date = data.Date;
            filteredData.Session = data.Session;

            if (data.Session == "0")
            {
                sum0 = 0;
                count0 = 0;
                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(',');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        float dV;
                        if (float.TryParse(dataValues[2], out dV))
                        {
                            // 리스트에 파싱된 값을 추가
                            sum0 += dV;
                            count0++;
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
            }
            else if (data.Session == "1")
            {
                sum1 = 0;
                count1 = 0;
                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(',');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        float dV;
                        if (float.TryParse(dataValues[2], out dV))
                        {
                            // 리스트에 파싱된 값을 추가
                            sum1 += dV;
                            count1++;
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
            }
            else if (data.Session == "2")
            {
                sum2 = 0;
                count2 = 0;
                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(',');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        float dV;
                        if (float.TryParse(dataValues[2], out dV))
                        {
                            // 리스트에 파싱된 값을 추가
                            sum2 += dV;
                            count2++;
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
            }
            else if (data.Session == "3")
            {
                sum3 = 0;
                count3 = 0;
                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(',');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        float dV;
                        if (float.TryParse(dataValues[2], out dV))
                        {
                            // 리스트에 파싱된 값을 추가
                            sum3 += dV;
                            count3++;
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
            }
        }

        // 평균 값 계산
        float average0 = sum0 / count0;
        float average1 = sum1 / count1;
        float average2 = sum2 / count2;
        float average3 = sum3 / count3;

        Debug.Log("평균0: " + average0);
        Debug.Log("평균1: " + average1);
        Debug.Log("평균2: " + average2);
        Debug.Log("평균3: " + average3);

        // 평균 값과 각 데이터의 값을 비교하여 필터링된 데이터를 저장
        foreach (Result_IndetailData data in originalDataList)
        {
            Result_IndetailData filteredData = new Result_IndetailData();
            filteredData.ID = data.ID;
            filteredData.Name = data.Name;
            filteredData.Date = data.Date;
            filteredData.Session = data.Session;

            if (data.Session == "0")
            {
                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(':');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        string[] subValues = dataValues[2].Split(',');
                        int dV1;
                        float dV2;
                        if (int.TryParse(dataValues[1], out dV1))
                        {
                            float.TryParse(subValues[2], out dV2);
                            if (dV1 == 4 || dV1 == 5 || dV1 == 8 || dV1 == 11 || dV1 == 12 || dV1 == 16)
                            {
                                // 평균 값과 비교하여 데이터를 저장
                                if (dV2 < average0 + diff1)
                                {
                                    // 필터링된 데이터 추가
                                    filteredData.Data.Add(i + "/" + data.Data[i]);
                                }
                            }
                            else if (dV1 == 6 || dV1 == 7 || dV1 == 9 || dV1 == 10 || dV1 == 20 || dV1 == 21 || dV1 == 22)
                            {
                                // 평균 값과 비교하여 데이터를 저장
                                if (dV2 > average0 + diff2)
                                {
                                    // 필터링된 데이터 추가
                                    filteredData.Data.Add(i + "/" + data.Data[i]);
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
            }
            else if (data.Session == "1")
            {
                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(':');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        string[] subValues = dataValues[2].Split(',');
                        int dV1;
                        float dV2;
                        if (int.TryParse(dataValues[1], out dV1))
                        {
                            float.TryParse(subValues[2], out dV2);
                            if (dV1 == 7 || dV1 == 8 || dV1 == 9 || dV1 == 10 || dV1 == 14 || dV1 == 15 || dV1 == 16 || dV1 == 17 || dV1 == 18 || dV1 == 19 || dV1 == 20 || dV1 == 21)
                            {
                                // 평균 값과 비교하여 데이터를 저장
                                if (dV2 < average1 + diff1)
                                {
                                    // 필터링된 데이터 추가
                                    filteredData.Data.Add(i + "/" + data.Data[i]);
                                }
                            }
                            else if (dV1 == 3 || dV1 == 4 || dV1 == 5 || dV1 == 6 || dV1 == 11 || dV1 == 12 || dV1 == 13 || dV1 == 22 || dV1 == 23 || dV1 == 24)
                            {
                                // 평균 값과 비교하여 데이터를 저장
                                if (dV2 > average1 + diff2)
                                {
                                    // 필터링된 데이터 추가
                                    filteredData.Data.Add(i + "/" + data.Data[i]);
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
            }
            else if (data.Session == "2")
            {
                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(':');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        string[] subValues = dataValues[2].Split(',');
                        int dV1;
                        float dV2;
                        if (int.TryParse(dataValues[1], out dV1))
                        {
                            float.TryParse(subValues[2], out dV2);
                            if (dV1 == 1 || dV1 == 2 || dV1 == 23 || dV1 == 32 || dV1 == 33)
                            {
                                // 평균 값과 비교하여 데이터를 저장
                                if (dV2 < average2 + diff1)
                                {
                                    // 필터링된 데이터 추가
                                    filteredData.Data.Add(i + "/" + data.Data[i]);
                                }
                            }
                            else if (dV1 == 7 || dV1 == 8 || dV1 == 26 || dV1 == 36 || dV1 == 37)
                            {
                                // 평균 값과 비교하여 데이터를 저장
                                if (dV2 > average2 + diff2)
                                {
                                    // 필터링된 데이터 추가
                                    filteredData.Data.Add(i + "/" + data.Data[i]);
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
            }
            else if (data.Session == "3")
            {
                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(':');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        string[] subValues = dataValues[2].Split(',');
                        int dV1;
                        float dV2;
                        if (int.TryParse(dataValues[1], out dV1))
                        {
                            float.TryParse(subValues[2], out dV2);
                            if (dV1 == 9 || dV1 == 10 || dV1 == 17 || dV1 == 18 || dV1 == 25 || dV1 == 26)
                            {
                                // 평균 값과 비교하여 데이터를 저장
                                if (dV2 < average3 + diff1)
                                {
                                    // 필터링된 데이터 추가
                                    filteredData.Data.Add(i + "/" + data.Data[i]);
                                }
                            }
                            else if (dV1 == 15 || dV1 == 16 || dV1 == 23 || dV1 == 24)
                            {
                                // 평균 값과 비교하여 데이터를 저장
                                if (dV2 > average3 + diff2)
                                {
                                    // 필터링된 데이터 추가
                                    filteredData.Data.Add(i + "/" + data.Data[i]);
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
            }

            // 필터링된 데이터가 있는 경우에만 리스트에 추가
            if (filteredData.Data.Count > 0)
            {
                filteredDataList.Add(filteredData);
            }
        }


        return filteredDataList;
    }

    List<Result_IndetailData> FilterData3(List<Result_IndetailData> originalDataList)
    {
        List<Result_IndetailData> filteredDataList = new List<Result_IndetailData>();

        // 여기서 조건에 따라 필터링된 데이터를 구합니다.
        foreach (Result_IndetailData data in originalDataList)
        {
            // 필터링된 데이터 리스트에 추가
            Result_IndetailData filteredData = new Result_IndetailData();
            filteredData.ID = data.ID;
            filteredData.Name = data.Name;
            filteredData.Date = data.Date;
            filteredData.Session = data.Session;

            if (data.Session == "0")
            {
                HashSet<int> checkedValues = new HashSet<int>(); // 이미 확인된 dV1 값을 저장하는 집합
                HashSet<int> uniqueValues = new HashSet<int>(); // 유일한 dV1 값을 저장하는 집합
                int count = 0; // dV1이 있는 개수를 세기 위한 변수
                int score = 0;

                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(':');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        string[] subValues = dataValues[2].Split(',');
                        int dV1; //dv1이 22번까지 있는지 확인하기
                        if (int.TryParse(dataValues[1], out dV1))
                        {
                            // 이미 확인된 값이 아니면서 1부터 22 사이의 값이면 count 증가
                            if (!checkedValues.Contains(dV1) && dV1 >= 1 && dV1 <= 22)
                            {
                                if (!uniqueValues.Contains(dV1))
                                {
                                    uniqueValues.Add(dV1); // 유일한 값으로 추가
                                    count++; // 해당 값이 있으면 count 증가
                                }
                                checkedValues.Add(dV1); // 이미 확인된 값으로 추가
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
                score = ((count * 100) / 22);
                //filteredData.Data.Add("score: " + score);
                filteredData.Data.Add(score.ToString());
            }
            else if (data.Session == "1")
            {
                HashSet<int> checkedValues = new HashSet<int>(); // 이미 확인된 dV1 값을 저장하는 집합
                HashSet<int> uniqueValues = new HashSet<int>(); // 유일한 dV1 값을 저장하는 집합
                int count = 0; // dV1이 있는 개수를 세기 위한 변수
                int score = 0;

                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(':');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        string[] subValues = dataValues[2].Split(',');
                        int dV1; //dv1이 22번까지 있는지 확인하기
                        if (int.TryParse(dataValues[1], out dV1))
                        {
                            // 이미 확인된 값이 아니면서 1부터 22 사이의 값이면 count 증가
                            if (!checkedValues.Contains(dV1) && dV1 >= 1 && dV1 <= 31)
                            {
                                if (!uniqueValues.Contains(dV1))
                                {
                                    uniqueValues.Add(dV1); // 유일한 값으로 추가
                                    count++; // 해당 값이 있으면 count 증가
                                }
                                checkedValues.Add(dV1); // 이미 확인된 값으로 추가
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
                score = ((count * 100) / 31);
                //filteredData.Data.Add("score: " + score);
                filteredData.Data.Add(score.ToString());
            }
            else if (data.Session == "2")
            {
                HashSet<int> checkedValues = new HashSet<int>(); // 이미 확인된 dV1 값을 저장하는 집합
                HashSet<int> uniqueValues = new HashSet<int>(); // 유일한 dV1 값을 저장하는 집합
                int count = 0; // dV1이 있는 개수를 세기 위한 변수
                int score = 0;

                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(':');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        string[] subValues = dataValues[2].Split(',');
                        int dV1; //dv1이 22번까지 있는지 확인하기
                        if (int.TryParse(dataValues[1], out dV1))
                        {
                            // 이미 확인된 값이 아니면서 1부터 22 사이의 값이면 count 증가
                            if (!checkedValues.Contains(dV1) && dV1 >= 1 && dV1 <= 43)
                            {
                                if (!uniqueValues.Contains(dV1))
                                {
                                    uniqueValues.Add(dV1); // 유일한 값으로 추가
                                    count++; // 해당 값이 있으면 count 증가
                                }
                                checkedValues.Add(dV1); // 이미 확인된 값으로 추가
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
                score = ((count * 100) / 43);
                //filteredData.Data.Add("score: " + score);
                filteredData.Data.Add(score.ToString());
            }
            else if (data.Session == "3")
            {
                HashSet<int> checkedValues = new HashSet<int>(); // 이미 확인된 dV1 값을 저장하는 집합
                HashSet<int> uniqueValues = new HashSet<int>(); // 유일한 dV1 값을 저장하는 집합
                int count = 0; // dV1이 있는 개수를 세기 위한 변수
                int score = 0;

                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(':');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        string[] subValues = dataValues[2].Split(',');
                        int dV1; //dv1이 22번까지 있는지 확인하기
                        if (int.TryParse(dataValues[1], out dV1))
                        {
                            // 이미 확인된 값이 아니면서 1부터 22 사이의 값이면 count 증가
                            if (!checkedValues.Contains(dV1) && dV1 >= 1 && dV1 <= 32)
                            {
                                if (!uniqueValues.Contains(dV1))
                                {
                                    uniqueValues.Add(dV1); // 유일한 값으로 추가
                                    count++; // 해당 값이 있으면 count 증가
                                }
                                checkedValues.Add(dV1); // 이미 확인된 값으로 추가
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
                score = ((count * 100) / 32);
                //filteredData.Data.Add("score: " + score);
                filteredData.Data.Add(score.ToString());
            }

            // 필터링된 데이터가 있는 경우에만 리스트에 추가
            if (filteredData.Data.Count > 0)
            {
                filteredDataList.Add(filteredData);
            }
        }

        return filteredDataList;
    }

    List<Result_IndetailData> FilterData4(List<Result_IndetailData> originalDataList)
    {
        List<Result_IndetailData> filteredDataList = new List<Result_IndetailData>();

        // 여기서 조건에 따라 필터링된 데이터를 구합니다.
        foreach (Result_IndetailData data in originalDataList)
        {
            // 필터링된 데이터 리스트에 추가
            Result_IndetailData filteredData = new Result_IndetailData();
            filteredData.ID = data.ID;
            filteredData.Name = data.Name;
            filteredData.Date = data.Date;
            filteredData.Session = data.Session;

            if (data.Session == "0")
            {
                HashSet<int> checkedValues = new HashSet<int>(); // 이미 확인된 dV1 값을 저장하는 집합
                HashSet<int> uniqueValues = new HashSet<int>(); // 유일한 dV1 값을 저장하는 집합
                int count = 0; // dV1이 있는 개수를 세기 위한 변수
                int score = 0;

                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(':');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        string[] subValues = dataValues[2].Split(',');
                        int dV1; //dv1이 22번까지 있는지 확인하기
                        if (int.TryParse(dataValues[1], out dV1))
                        {
                            // 이미 확인된 값이 아니면서 1부터 22 사이의 값이면 count 증가
                            if (!checkedValues.Contains(dV1) && (dV1 == 4 || dV1 == 5 || dV1 == 6 || dV1 == 7 || dV1 == 8 || dV1 == 9 || dV1 == 10 || dV1 == 11 || dV1 == 12 || dV1 == 16 || dV1 == 20 || dV1 == 21 || dV1 == 22))
                            {
                                if (!uniqueValues.Contains(dV1))
                                {
                                    uniqueValues.Add(dV1); // 유일한 값으로 추가
                                    count++; // 해당 값이 있으면 count 증가
                                }
                                checkedValues.Add(dV1); // 이미 확인된 값으로 추가
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
                score = ((count * 100) / 13);
                //filteredData.Data.Add("score: " + score);
                filteredData.Data.Add(score.ToString());
            }
            else if (data.Session == "1")
            {
                HashSet<int> checkedValues = new HashSet<int>(); // 이미 확인된 dV1 값을 저장하는 집합
                HashSet<int> uniqueValues = new HashSet<int>(); // 유일한 dV1 값을 저장하는 집합
                int count = 0; // dV1이 있는 개수를 세기 위한 변수
                int score = 0;

                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(':');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        string[] subValues = dataValues[2].Split(',');
                        int dV1; //dv1이 22번까지 있는지 확인하기
                        if (int.TryParse(dataValues[1], out dV1))
                        {
                            // 이미 확인된 값이 아니면서 1부터 22 사이의 값이면 count 증가
                            if (!checkedValues.Contains(dV1) && (dV1 == 3 || dV1 == 4 || dV1 == 5 || dV1 == 6 || dV1 == 7 || dV1 == 8 || dV1 == 9 || dV1 == 10 || dV1 == 11 || dV1 == 12 || dV1 == 13 || dV1 == 14 || dV1 == 15 || dV1 == 16 || dV1 == 17 || dV1 == 18 || dV1 == 19 || dV1 == 20 || dV1 == 21 || dV1 == 22 || dV1 == 23 || dV1 == 24))
                            {
                                if (!uniqueValues.Contains(dV1))
                                {
                                    uniqueValues.Add(dV1); // 유일한 값으로 추가
                                    count++; // 해당 값이 있으면 count 증가
                                }
                                checkedValues.Add(dV1); // 이미 확인된 값으로 추가
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
                score = ((count * 100) / 22);
                //filteredData.Data.Add("score: " + score);
                filteredData.Data.Add(score.ToString());
            }
            else if (data.Session == "2")
            {
                HashSet<int> checkedValues = new HashSet<int>(); // 이미 확인된 dV1 값을 저장하는 집합
                HashSet<int> uniqueValues = new HashSet<int>(); // 유일한 dV1 값을 저장하는 집합
                int count = 0; // dV1이 있는 개수를 세기 위한 변수
                int score = 0;

                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(':');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        string[] subValues = dataValues[2].Split(',');
                        int dV1; //dv1이 22번까지 있는지 확인하기
                        if (int.TryParse(dataValues[1], out dV1))
                        {
                            // 이미 확인된 값이 아니면서 1부터 22 사이의 값이면 count 증가
                            if (!checkedValues.Contains(dV1) && (dV1 == 1 || dV1 == 2 || dV1 == 7 || dV1 == 8 || dV1 == 23 || dV1 == 26 || dV1 == 32 || dV1 == 33 || dV1 == 36 || dV1 == 37))
                            {
                                if (!uniqueValues.Contains(dV1))
                                {
                                    uniqueValues.Add(dV1); // 유일한 값으로 추가
                                    count++; // 해당 값이 있으면 count 증가
                                }
                                checkedValues.Add(dV1); // 이미 확인된 값으로 추가
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
                score = ((count * 100) / 10);
                //filteredData.Data.Add("score: " + score);
                filteredData.Data.Add(score.ToString());
            }
            else if (data.Session == "3")
            {
                HashSet<int> checkedValues = new HashSet<int>(); // 이미 확인된 dV1 값을 저장하는 집합
                HashSet<int> uniqueValues = new HashSet<int>(); // 유일한 dV1 값을 저장하는 집합
                int count = 0; // dV1이 있는 개수를 세기 위한 변수
                int score = 0;

                for (int i = 0; i < data.Data.Count; i++)
                {
                    string[] dataValues = data.Data[i].Split(':');

                    // 데이터의 형식이 올바른지 확인
                    if (dataValues.Length >= 2)
                    {
                        string[] subValues = dataValues[2].Split(',');
                        int dV1; //dv1이 22번까지 있는지 확인하기
                        if (int.TryParse(dataValues[1], out dV1))
                        {
                            // 이미 확인된 값이 아니면서 1부터 22 사이의 값이면 count 증가
                            if (!checkedValues.Contains(dV1) && (dV1 == 9 || dV1 == 10 || dV1 == 15 || dV1 == 16 || dV1 == 17 || dV1 == 18 || dV1 == 23 || dV1 == 24 || dV1 == 25 || dV1 == 26))
                            {
                                if (!uniqueValues.Contains(dV1))
                                {
                                    uniqueValues.Add(dV1); // 유일한 값으로 추가
                                    count++; // 해당 값이 있으면 count 증가
                                }
                                checkedValues.Add(dV1); // 이미 확인된 값으로 추가
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to parse data value: " + dataValues[1]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid data format: " + data.Data[i]);
                    }
                }
                score = ((count * 100) / 10);
                //filteredData.Data.Add("score: " + score);
                filteredData.Data.Add(score.ToString());
            }

            // 필터링된 데이터가 있는 경우에만 리스트에 추가
            if (filteredData.Data.Count > 0)
            {
                filteredDataList.Add(filteredData);
            }
        }

        return filteredDataList;
    }

    List<Result_IndetailData> FilterData5(List<Result_IndetailData> originalDataList1, List<Result_IndetailData> originalDataList2)
    {
        List<Result_IndetailData> filteredDataList = new List<Result_IndetailData>();

        // originalDataList1의 데이터를 기준으로 필터링
        foreach (Result_IndetailData data1 in originalDataList1)
        {
            // originalDataList2에서 동일한 Date를 갖는 데이터를 찾음
            Result_IndetailData data2 = originalDataList2.FirstOrDefault(d => d.Date == data1.Date);

            // Date가 동일한 데이터가 없으면 스킵
            if (data2 == null)
                continue;

            // 필터링된 데이터 리스트에 추가
            Result_IndetailData filteredData = new Result_IndetailData();
            filteredData.ID = data1.ID;
            filteredData.Name = data1.Name;
            filteredData.Date = data1.Date;
            filteredData.Session = data1.Session;

            // originalDataList1의 Data 값을 먼저 추가
            foreach (var dataValue in data1.Data)
            {
                filteredData.Data.Add(dataValue);
            }

            // originalDataList2의 Data 값을 추가
            foreach (var dataValue in data2.Data)
            {
                filteredData.Data.Add(dataValue);
            }

            // 필터링된 데이터가 있는 경우에만 리스트에 추가
            if (filteredData.Data.Count > 0)
            {
                filteredDataList.Add(filteredData);
            }
        }

        return filteredDataList;
    }


    void WriteToXml(List<Result_IndetailData> dataList)
    {
        // RESULT_A 파일 경로 설정
        string filePathA = Path.Combine(Application.persistentDataPath, "RESULT.xml");

        // XML 파일 생성
        XmlDocument document = new XmlDocument();
        XmlElement itemListElement = document.CreateElement("Result_data");
        document.AppendChild(itemListElement);

        // 데이터 리스트를 순회하면서 XML에 추가
        foreach (Result_IndetailData data in dataList)
        {
            XmlElement itemElement = document.CreateElement("Result_data");
            itemElement.SetAttribute("ID", data.ID);
            itemElement.SetAttribute("Name", data.Name);
            itemElement.SetAttribute("Date", data.Date);
            itemElement.SetAttribute("Session", data.Session);

            // Data 리스트에 있는 내용을 필터링된 세션 뒤에 추가
            for (int i = 0; i < data.Data.Count; i++)
            {
                string dataKey = "Data_" + (i + 1);
                itemElement.SetAttribute(dataKey, data.Data[i]);
            }

            itemListElement.AppendChild(itemElement);
        }

        // XML 파일 저장
        document.Save(filePathA);
    }

    void AppendToXml(List<Result_IndetailData> dataList)
    {
        // 기존 결과 파일의 경로
        string filePathA = Path.Combine(Application.persistentDataPath, "RESULT.xml");

        // 기존 결과 파일이 없으면 새로 생성
        if (!File.Exists(filePathA))
        {
            Debug.LogError("RESULT.xml 파일이 존재하지 않습니다. 파일을 생성하는 대신 WriteToXml 메소드를 호출하세요.");
            return;
        }

        // 기존 XML 파일 불러오기
        XmlDocument document = new XmlDocument();
        document.Load(filePathA);

        // Root 노드 가져오기
        XmlElement rootElement = document.DocumentElement;

        // 데이터 리스트를 순회하면서 XML에 추가
        foreach (Result_IndetailData data in dataList)
        {
            XmlElement itemElement = document.CreateElement("Result_data");
            itemElement.SetAttribute("ID", data.ID);
            itemElement.SetAttribute("Name", data.Name);
            itemElement.SetAttribute("Date", data.Date);
            itemElement.SetAttribute("Session", data.Session);

            // Data 리스트에 있는 내용을 필터링된 세션 뒤에 추가
            for (int i = 0; i < data.Data.Count; i++)
            {
                string dataKey = "Data_" + (i + 1);
                itemElement.SetAttribute(dataKey, data.Data[i]);
            }

            rootElement.AppendChild(itemElement);
        }

        // 수정된 XML 파일 저장
        document.Save(filePathA);
    }


    public void Init_RID()
    {

        for (int i = 0; i < 5000; i++)
        {
            String_Data_attribute.Add("Data_" + (i + 1).ToString());
            //Debug.Log("Data_attribute : " + String_Data_attribute[i]);
        }
    }
}
