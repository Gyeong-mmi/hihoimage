using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Aloe_lyric : MonoBehaviour
{
    public Text lyricsText;
    public Text blackText;
    public Text blueText;
    public Text redText;
    public Text greenText;
    public AudioSource aloeAudio;

    private List<string> aloeLyrics;
    private List<string> blackLyrics;
    private List<string> redLyrics;
    private List<string> blueLyrics;
    private List<string> greenLyrics;

    private List<float> aloeTimings;

    void Start()
    {
        // ����� Ÿ�̹� �ʱ�ȭ
        InitializeLyrics();

        if (aloeAudio == null)
        {
            aloeAudio = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        float currentTime = aloeAudio.time;

        for (int i = 0; i < aloeTimings.Count; i++)
        {
            if (currentTime == aloeTimings[i] || currentTime < aloeTimings[i + 1])
            {
                lyricsText.text = aloeLyrics[i];
                blackText.text = blackLyrics[i];
                redText.text = redLyrics[i];
                blueText.text = blueLyrics[i];
                greenText.text = greenLyrics[i];
                break;
            }
        }
    }

    void InitializeLyrics()
    {
        aloeLyrics = new List<string> { "�˷ο�", "���� �����ִ� �Գ��Ӹ� �˷ο�",
                                        "�հ�ó�� �������� �˷ο�", "�˷ο��� ���� �����ؿ�",
                                        "����ó�� ��¦��¦ ���� ����",
                                        "������ �˷ο��� ������",

                                        "�ǵ� �ǵ� �ݵ� �ݵ�", "�Ų� �Ų� �̲� �̲�",
                                        "�ǵ� �ǵ� �ݵ� �ݵ�", "�Ų� �Ų� �̲� �̲�",
                                        "��¦��¦ �˷ο��� ������", "�հ��� ���̷� �þ��",
                                        "���� ��� ������", "�Ź����� ���ܳ���",
                                        "���� ��� ������", "�������� �Գ��Ӹ� �˷ο�",
                                        "�̲��̲� ������            ", ""};

        blackLyrics = new List<string> { "", "                                      �˷ο�",
                                         "                              �˷ο�", "",
                                         "��¦��¦ ",
                                         "",

                                         "                �ݵ� �ݵ�", "�Ų� �Ų�                ",
                                         "                �ݵ� �ݵ�", "�Ų� �Ų�                ",
                                         "", "",
                                         "", "",
                                         "", "                              �˷ο�",
                                         "                          �˷ο�", ""};

        redLyrics = new List<string> { "", "",
                                       "", "",
                                       "",
                                       "",

                                       "", "                �̲� �̲�",
                                       "", "                �̲� �̲�",
                                       "", "",
                                       "", "",
                                       "", "",
                                       "", ""};

        blueLyrics = new List<string> { "", "",
                                        "", "",
                                        "",
                                        "",

                                        "�ǵ� �ǵ�                ", "",
                                        "�ǵ� �ǵ�                ", "",
                                        "", "",
                                        "", "",
                                        "", "",
                                        "", ""};

        greenLyrics = new List<string> { "", "",
                                         "", "",
                                         "",
                                         "",

                                         "", "",
                                         "", "",
                                         "", "",
                                         "���� ��� ������", "",
                                         "���� ��� ������", "",
                                         "", ""};

        aloeTimings = new List<float> { 0f, 9f,
                                        14.3f, 18.8f,
                                        23.8f,
                                        28.3f,

                                        33.3f, 35.8f,
                                        38.3f, 40.8f,
                                        43.3f, 47.9f,
                                        50.4f, 57.4f,
                                        59.9f, 66.9f,
                                        71.9f, 76.9f};
    }
}
