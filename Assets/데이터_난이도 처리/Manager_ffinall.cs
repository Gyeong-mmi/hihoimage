using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Application = UnityEngine.Application;

public class Manager_ffinall : CLASS_XmlData
{
    public static Manager_ffinall instance = null;

    public static List<DialogueData> OriginDataList;
    private List<DialogueData> NewDataList;

    private string filePath;
    private DialogueData Student_data;
    public bool Is_datasaved = false;

    public GameObject final_active;

    [Header("[DATA RESULT PAGE COMPONENT]")]
    public GameObject Graph_chart;
    public GameObject Prefab_SD;
    public Transform Panel_Left_Content;
    public Slider ProgressBar_OX;
    public Slider ProgressBar_SW;

    public GameObject DataText_group;
    //���� public ���� �ʿ�
    private UnityEngine.UI.Text test_Name;
    private UnityEngine.UI.Text text_ID;
    private UnityEngine.UI.Text test_Time;
    private UnityEngine.UI.Text text_Data_1;
    private UnityEngine.UI.Text text_Data_2;
    private GameObject text_None;
    private UnityEngine.UI.Text text_Date_0;
    private UnityEngine.UI.Text text_Date_1;
    private UnityEngine.UI.Text text_Date_2;
    private UnityEngine.UI.Text text_Date_3;
    private UnityEngine.UI.Text text_Date_4;

    //
    private Stack<DialogueData> Recent_data = new Stack<DialogueData>();
    private Stack<string> Recent_result_1 = new Stack<string>();
    private Stack<string> Recent_result_2 = new Stack<string>();

    //private List<GameObject> Textlist = new Stack<string>();
    private List<Text> Textlist = new List<Text>();

    // Result ������ ������, ���� ��� ������ �ʿ�
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Init_Text();
        filePath = Path.Combine(Application.persistentDataPath, "RESULT.xml");
        Check_XmlFile("RESULT");

        if (filePath != null)
        {
            Recent_data.Clear();
            Debug.Log(Application.dataPath);
            OriginDataList = Read();
            NewDataList = Read();

            for (int i = 0; i < OriginDataList.Count; ++i)
            {
                DialogueData item = OriginDataList[i];

                GameObject myInstance = Instantiate(Prefab_SD, Panel_Left_Content);
                myInstance.GetComponent<UI_button_SD>().Result_num = i;
                myInstance.GetComponent<UI_button_SD>().Student = item.Name;
            }
            Write();
        }
    }

    private void Update()
    {
        if (final_active.activeSelf)
        {
            Init_Text();
            filePath = Path.Combine(Application.persistentDataPath, "RESULT.xml");
            Check_XmlFile("RESULT");

            if (filePath != null)
            {
                Recent_data.Clear();
                OriginDataList = NewDataList;
                NewDataList = Read();

                for (int i = OriginDataList.Count; i < NewDataList.Count; ++i)
                {
                    DialogueData item = NewDataList[i];

                    GameObject myInstance = Instantiate(Prefab_SD, Panel_Left_Content);
                    myInstance.GetComponent<UI_button_SD>().Result_num = i;
                    myInstance.GetComponent<UI_button_SD>().Student = item.Name;
                }
                Write();
            }
        }
    }

    public override void Write()
    {
        if (Is_datasaved)
        {
            NewDataList.Add(Student_data);
            Is_datasaved = false;
        }

        XmlDocument Document = new XmlDocument();
        XmlElement ItemListElement = Document.CreateElement("Result_data");
        Document.AppendChild(ItemListElement);

        foreach (DialogueData data in NewDataList)
        {
            XmlElement ItemElement = Document.CreateElement("Result_data");
            ItemElement.SetAttribute("ID", data.ID);
            ItemElement.SetAttribute("Name", data.Name);
            //ItemElement.SetAttribute("Birthdate", data.Birth_date);
            ItemElement.SetAttribute("Date", data.Date);
            ItemElement.SetAttribute("Session", data.Session);
            ItemElement.SetAttribute("Data_1", data.Data_1);
            ItemElement.SetAttribute("Data_2", data.Data_2);
            ItemListElement.AppendChild(ItemElement);
        }
        Document.Save(filePath);
    }

    public List<DialogueData> Read()
    {
        XmlDocument Document = new XmlDocument();
        Document.Load(filePath);
        XmlElement ItemListElement = Document["Result_data"];
        List<DialogueData> ItemList = new List<DialogueData>();

        foreach (XmlElement ItemElement in ItemListElement.ChildNodes)
        {
            DialogueData Item = new DialogueData();
            Item.ID = ItemElement.GetAttribute("ID");
            Item.Name = ItemElement.GetAttribute("Name");
            //Item.Birth_date = ItemElement.GetAttribute("Birthdate");
            Item.Date = ItemElement.GetAttribute("Date");
            Item.Session = ItemElement.GetAttribute("Session");
            Item.Data_1 = ItemElement.GetAttribute("Data_1");
            Item.Data_2 = ItemElement.GetAttribute("Data_2");

            ItemList.Add(Item);
        }
        return ItemList;
    }

    public void Add_data(DialogueData data)
    {
        Is_datasaved = true;
        Student_data = data;
    }

    public void Refresh_data()
    {
        if (NewDataList.Count != OriginDataList.Count)
        {
            //�ʱ� start���� �����س� �����հ� ���� ���� ������
            //�������� ���� ��ȣ��ŭ �߰� ���� �ʿ�
            for (int i = OriginDataList.Count; i < NewDataList.Count; ++i)
            {
                DialogueData item = NewDataList[i];
                //Debug.Log(string.Format("DATA [{0}] : ({1}, {2}, {3}, {4}, {5}, {6}, {7})",
                //    i, item.ID, item.Name, item.Birth_date, item.Date, item.Session, item.Data_1, item.Data_2));

                GameObject myInstance = Instantiate(Prefab_SD, Panel_Left_Content);
                myInstance.GetComponent<UI_button_SD>().Result_num = i;
                myInstance.GetComponent<UI_button_SD>().Student = item.Name;
            }
        }
    }

    public void Change_result(int num)
    {
        Recent_data.Clear();
        Recent_result_1.Clear();
        Recent_result_2.Clear();

        DialogueData Item = NewDataList[num];

        test_Name.text = Item.Name;
        text_ID.text = Item.ID;
        test_Time.text = Item.Date;
        text_Data_1.text = Item.Data_1;
        text_Data_2.text = Item.Data_2;

        //�ֱ� �÷��� ������
        ProgressBar_OX.value = Int32.Parse(Item.Data_1) * 0.1f;
        ProgressBar_SW.value = Int32.Parse(Item.Data_2) * 0.1f;

        //�ֱ� 3ȸ�� ������ ����, ���⼭ �̻��� �����Ͱ� �� ���ɼ��� ����
        foreach (DialogueData data in NewDataList)
        {
            if (data.ID == Item.ID)
            {
                Recent_data.Push(data);
            }
        }

        //Debug.Log("ã�Ƴ� ������ ��" + Recent_data.Count);
        //1,2 -> ������ ���� /3,4,5 -> �׷���
        int Num_Recent_stack = Recent_data.Count;

        if (Num_Recent_stack > 2)
        {
            //��¥ �ؽ�Ʈ ����, ������ 3,4�� ����ó��
            if (Num_Recent_stack == 3 || Num_Recent_stack == 4)
            {
                text_Date_4.text = "-";
                text_Date_3.text = "-";
            }

            //5�� max ���� ������ �ð�ȭ
            for (int i = 0; i < 5; i++)
            {
                //���� �ֱ� ������ ������� pop/ ������ 1,2 ����
                if (i < Num_Recent_stack)
                {
                    Item = Recent_data.Pop();
                    Recent_result_1.Push(Item.Data_1);
                    Recent_result_2.Push(Item.Data_2);

                    //Textlist[Num_Recent_stack - i+1].GetComponent<Text>().text = Item.Date;
                    //Textlist[5 - (i + 1)].text = Item.Date;
                    //Textlist[5 - (i+1)].text = Item.Date;
                    Textlist[i].text = Item.Date;

                }
                else
                {
                    Textlist[i].text = "-";
                }
            }

            //Graph value
            Graph_chart.GetComponent<MultipleGraphDemo>().Add_Data(Recent_result_1, Recent_result_2);
            text_None.SetActive(false);
            Graph_chart.SetActive(true);
        }

        else if (Num_Recent_stack == 2)
        {
            //��¥ �ؽ�Ʈ ����, ������ 3,4�� ����ó��
            if (Num_Recent_stack == 2 || Num_Recent_stack == 3 || Num_Recent_stack == 4)
            {
                text_Date_4.text = "-";
                text_Date_3.text = "-";
                text_Date_2.text = "-";
            }

            //5�� max ���� ������ �ð�ȭ
            for (int i = 0; i < 5; i++)
            {
                //���� �ֱ� ������ ������� pop/ ������ 1,2 ����
                if (i < Num_Recent_stack)
                {
                    Item = Recent_data.Pop();
                    Recent_result_1.Push(Item.Data_1);
                    Recent_result_2.Push(Item.Data_2);

                    //Textlist[Num_Recent_stack - i+1].GetComponent<Text>().text = Item.Date;
                    //Textlist[5 - (i + 1)].text = Item.Date;
                    //Textlist[5 - (i+1)].text = Item.Date;
                    Textlist[Num_Recent_stack - i - 1].text = Item.Date;

                }
                else
                {
                    Textlist[i].text = "-";
                }
            }

            //Graph value
            Graph_chart.GetComponent<MultipleGraphDemo>().Add_Data(Recent_result_1, Recent_result_2);
            text_None.SetActive(false);
            Graph_chart.SetActive(true);
        }

        else
        {
            //��¥ �ؽ�Ʈ �ʱ�ȭ �ʿ�
            for (int i = 0; i < 5; i++)
            {
                Textlist[i].text = "";
            }

            Graph_chart.SetActive(false);
            text_None.SetActive(true);
        }
    }
    public void Setting_ButtonSD()
    {

    }

    public DialogueData Get_Listdata(int num)
    {
        return OriginDataList[num];
    }

    public void Init_Text()
    {
        test_Name = DataText_group.transform.GetChild(0).gameObject.GetComponent<Text>();
        text_ID = DataText_group.transform.GetChild(1).gameObject.GetComponent<Text>();
        test_Time = DataText_group.transform.GetChild(2).gameObject.GetComponent<Text>();
        text_Data_1 = DataText_group.transform.GetChild(3).gameObject.GetComponent<Text>();
        text_Data_2 = DataText_group.transform.GetChild(4).gameObject.GetComponent<Text>();
        text_None = DataText_group.transform.GetChild(5).gameObject;
        text_Date_0 = DataText_group.transform.GetChild(6).gameObject.GetComponent<Text>();
        text_Date_1 = DataText_group.transform.GetChild(7).gameObject.GetComponent<Text>();
        text_Date_2 = DataText_group.transform.GetChild(8).gameObject.GetComponent<Text>();
        text_Date_3 = DataText_group.transform.GetChild(9).gameObject.GetComponent<Text>();
        text_Date_4 = DataText_group.transform.GetChild(10).gameObject.GetComponent<Text>();

        Textlist.Add(text_Date_0);
        Textlist.Add(text_Date_1);
        Textlist.Add(text_Date_2);
        Textlist.Add(text_Date_3);
        Textlist.Add(text_Date_4);
    }
}
