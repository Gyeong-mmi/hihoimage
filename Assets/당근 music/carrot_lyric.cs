using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class carrot_lyric : MonoBehaviour
{
    public Text lyricsText;
    public Text blackText;
    public Text redText;
    public Text blueText;
    public AudioSource carrotAudio;

    private List<string> carrotLyrics;
    private List<string> blackLyrics;
    private List<string> redLyrics;
    private List<string> blueLyrics;

    private List<float> carrotTimings;

    void Start()
    {
        // ����� Ÿ�̹� �ʱ�ȭ
        InitializeLyrics();

        if (carrotAudio == null)
        {
            carrotAudio = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        float currentTime = carrotAudio.time;

        for (int i = 0; i < carrotTimings.Count; i++)
        {
            if (currentTime == carrotTimings[i] || currentTime < carrotTimings[i + 1])
            {
                lyricsText.text = carrotLyrics[i];
                blackText.text = blackLyrics[i];
                redText.text = redLyrics[i];
                blueText.text = blueLyrics[i];
                break;
            }
        }
    }

    void InitializeLyrics()
    {
        carrotLyrics = new List<string> { "���", "���� ���� �����",
                                        "�̾ƺ��ƿ�      ", "���� ���� �����",
                                        "�̾ƺ��ƿ�      ",
                                        "���� ����� �ڸ��� ���� ����",

                                        "�� �� �����", "",
                                        "������ ����� �ڸ���", "�� �� �� ��",
                                        "�� �� �����", "",
                                        "�ƻ� �ƻ� ��ٵ�", "���� ���� ��ٵ�",
                                        "��� ���� ����", "���� �־��     ",
                                        "���� ����� ����           ",
                                        "���� ����� ���� ����           ", ""};

        blackLyrics = new List<string> { "", "",
                                         "                  ��!", "",
                                         "                  ��!", "",
                                         "", "", "", "",
                                         "", "", "", "", "", "                    ��!",
                                         "                            ���ƿ�",
                                         "                                   ���ƿ�", ""};

        redLyrics = new List<string> { "", "",
                                       "", "",
                                       "", "                               ���� ����",
                                       "", "                ������", "", "",
                                       "", "                �ƻ��",
                                       "", "", "", "", "", "", ""};

        blueLyrics = new List<string> { "", "", "", "",
                                        "", "", "", "���� ����            ", 
                                        "", "�� �� �� ��",
                                        "", "�ƻ� �ƻ�            ", "", "", "", "",
                                        "", "", ""};

        carrotTimings = new List<float> { 0f, 8.5f,
                                        10.7f, 12.9f,
                                        15.1f,
                                        17.3f,

                                        21.5f, 23.7f,
                                        26.2f, 28.4f,
                                        30.6f, 32.8f,
                                        35.0f, 37.2f,
                                        39.4f, 41.6f,
                                        43.8f, 47.8f, 51.8f};
    }
}
