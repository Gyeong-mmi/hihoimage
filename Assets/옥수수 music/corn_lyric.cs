using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class corn_lyric : MonoBehaviour
{
    public Text lyricsText;
    public Text blackText;
    public Text redText;
    public Text blueText;
    public AudioSource cornAudio;

    private List<string> cornLyrics;
    private List<string> blackLyrics;
    private List<string> redLyrics;
    private List<string> blueLyrics;

    private List<float> cornTimings;

    void Start()
    {
        // ����� Ÿ�̹� �ʱ�ȭ
        InitializeLyrics();

        if (cornAudio == null)
        {
            cornAudio = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        float currentTime = cornAudio.time;

        for (int i = 0; i < cornTimings.Count; i++)
        {
            if (currentTime == cornTimings[i] || currentTime < cornTimings[i + 1])
            {
                lyricsText.text = cornLyrics[i];
                blackText.text = blackLyrics[i];
                redText.text = redLyrics[i];
                blueText.text = blueLyrics[i];
                break;
            }
        }
    }

    void InitializeLyrics()
    {
        cornLyrics = new List<string> { "������", "",
                                        "", "",
                                        "",
                                        "",

                                        "�������� ��������", "�ʹ� �ʹ� �ʹ� ������",
                                        "", "������ ������",
                                        "", "��Ƽ� ������",
                                        "�˾��� �� �䵵������", "Ƣ�ܼ� ������",
                                        "������ �� ����������", "�������� �پ���",
                                        "�İ��� �ʹ�           ",
                                        "", "",
                                        "���� ���� ��������", "�ʹ� ������",
                                        "���� ���� ��������", "�ʹ� ������", ""};

        blackLyrics = new List<string> { "", "                �÷� ����",
                                        "���� ����                ", "�÷� ���� �÷� ������",
                                        "������!",
                                        "���� ���� ���� ����",

                                        "", "",
                                        "������!", "",
                                        "���� ������   ", "",
                                        "                �䵵������", "",
                                        "                ����������", "",
                                        "                    ��վ�",
                                        "                ���� ����", "",
                                        "���� ����               ", "        ������",
                                        "���� ����               ", "        ������", ""};

        redLyrics = new List<string> { "", "",
                                        "                ���� ����", "",
                                        "",
                                        "",

                                        "", "",
                                        "", "",
                                        "                            ������", "",
                                        "", "",
                                        "", "",
                                        "",
                                        "", "��� �ҷ���",
                                        "", "",
                                        "", "", ""};

        blueLyrics = new List<string> { "", "�÷� ����                ",
                                        "", "",
                                        "",
                                        "",

                                        "", "",
                                        "", "",
                                        "����                               ", "",
                                        "", "",
                                        "", "",
                                        "",
                                        "�÷� ����                ", "",
                                        "", "",
                                        "", "", ""};

        cornTimings = new List<float> { 0f, 8.8f,
                                        10.8f, 12.8f,
                                        16.3f,
                                        17.3f,

                                        19.3f, 21.8f,
                                        24.8f, 25.9f,
                                        28.3f, 30.5f,
                                        32.7f, 34.9f,
                                        36.9f, 39.1f,
                                        41.3f,
                                        43.5f, 45.5f,
                                        47.5f, 49.5f,
                                        51.5f, 53.7f, 55.7f};
    }
}
