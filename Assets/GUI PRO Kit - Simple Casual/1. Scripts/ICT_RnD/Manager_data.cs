
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Application = UnityEngine.Application;

// 각 column에 해당하는 데이터 작성
/*public class DialogueData
{
    [XmlAttribute]
    public string ID;
    [XmlAttribute]
    public string Name;
    [XmlAttribute]
    public string Birth_date;
    [XmlAttribute]
    public string Date;
    [XmlAttribute]
    public string Session;
    [XmlAttribute]
    public string Data_1;
    [XmlAttribute]
    public string Data_2;
}*/

public class Manager_data : MonoBehaviour
{
    public static Manager_data instance = null;

    public static List<DialogueData> OriginDataList;
    private List<DialogueData> NewDataList;

    private string filePath;
    private DialogueData Student_data;
    public bool Is_datasaved = false;


    [Header ("[DATA RESULT PAGE COMPONENT]")]
    public GameObject Graph_chart;
    public GameObject Prefab_SD;
    public Transform Panel_Left_Content;
    public Slider ProgressBar_OX;
    public Slider ProgressBar_SW;

    public GameObject DataText_group;
    //추후 public 삭제 필요
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
    private TextAsset XmlFilepath;

    // Start is called before the first frame update
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
        filePath = "Data/RESULT";
        XmlFilepath = Resources.Load<TextAsset>(filePath);


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
    
    public void Write()
    {
        //저장된 DataList를 저장된 Filepath에 저장
        //최종적으로 write 함수가 호출이 되지 않으면 저장이 되지 않음
        if (Is_datasaved)
        {
            NewDataList.Add(Student_data);
            Debug.Log("SAVED DATA WRITE");
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
            ItemElement.SetAttribute("Birthdate", data.Birth_date);
            ItemElement.SetAttribute("Date", data.Date);
            ItemElement.SetAttribute("Session", data.Session);
            ItemElement.SetAttribute("Data_1", data.Data_1);
            ItemElement.SetAttribute("Data_2", data.Data_2);
            ItemListElement.AppendChild(ItemElement);
        }
        Document.LoadXml(XmlFilepath.ToString());

    }

    public List<DialogueData> Read()
    {
        //저장된 filepath에서 xml파일 로드
        XmlDocument Document = new XmlDocument();
        Document.LoadXml(XmlFilepath.ToString());
        XmlElement ItemListElement = Document["Result_data"];
        List<DialogueData> ItemList = new List<DialogueData>();

        foreach (XmlElement ItemElement in ItemListElement.ChildNodes)
        {
            DialogueData Item = new DialogueData();
            Item.ID = ItemElement.GetAttribute("ID");
            Item.Name = ItemElement.GetAttribute("Name");
            Item.Birth_date = ItemElement.GetAttribute("Birthdate");
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
        if(NewDataList.Count != OriginDataList.Count)
        {
            //초기 start에서 생성해낸 프리팹과 수가 맞지 않으면
            //생성되지 않은 번호만큼 추가 생성 필요
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

        //최근 플레이 데이터
        ProgressBar_OX.value = Int32.Parse(Item.Data_1) * 0.1f;
        ProgressBar_SW.value = Int32.Parse(Item.Data_2) * 0.1f;

        //최근 3회차 데이터 추출, 여기서 이상의 데이터가 들어갈 가능성이 있음
        foreach (DialogueData data in NewDataList)
        {
            if (data.ID == Item.ID)
            {
                Recent_data.Push(data);
            }
        }

        //Debug.Log("찾아낸 데이터 수" + Recent_data.Count);
        //1,2 -> 데이터 없음 /3,4,5 -> 그래프
        int Num_Recent_stack = Recent_data.Count;

        if (Num_Recent_stack > 2)
        {
            //날짜 텍스트 리셋, 데이터 3,4개 예외처리
            if(Num_Recent_stack == 3 || Num_Recent_stack == 4)
            {
                text_Date_4.text = "-";
                text_Date_3.text = "-";
            }

            //5개 max 기준 데이터 시각화
            for (int i = 0; i < 5; i++)
            {
                //가장 최근 데이터 순서대로 pop/ 데이터 1,2 저장
                if (i < Num_Recent_stack)
                {
                    Item = Recent_data.Pop();
                    Recent_result_1.Push(Item.Data_1);
                    Recent_result_2.Push(Item.Data_2);

                    //Textlist[Num_Recent_stack - i+1].GetComponent<Text>().text = Item.Date;
                    Textlist[5 - (i+1)].text = Item.Date;

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
