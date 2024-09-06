using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class worm_lyric : MonoBehaviour
{
    public Text lyricsText;
    public Text blackText;
    public Text greenText;
    public Text pinkText;
    public AudioSource wormAudio;

    private List<string> wormLyrics;
    private List<string> blackLyrics;
    private List<string> greenLyrics;
    private List<string> pinkLyrics;

    private List<float> wormTimings;

    void Start()
    {
        // ����� Ÿ�̹� �ʱ�ȭ
        InitializeLyrics();

        if (wormAudio == null)
        {
            wormAudio = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        float currentTime = wormAudio.time;

        for (int i = 0; i < wormTimings.Count; i++)
        {
            if (currentTime == wormTimings[i] || currentTime < wormTimings[i + 1])
            {
                lyricsText.text = wormLyrics[i];
                blackText.text = blackLyrics[i];
                greenText.text = greenLyrics[i];
                pinkText.text = pinkLyrics[i];
                break;
            }
        }
    }

    void InitializeLyrics()
    {
        wormLyrics = new List<string> { "�ɺ���", "���� �ɺ� �ɺ� �ɺ�����",
                                        "���� ���� õõ�� ����", "�ٸ��� �ʹ� �ʹ� ª��",
                                        "� �ִ� �ޱ��ޱ� �ָ�����",
                                        "",

                                        "", "��Ʋ ��Ʋ �޿��- - ��",
                                        "��Ʋ ��Ʋ �޿��- -", "���� �ɺ� �ɺ� �ɺ�����",
                                        "���� ���� õõ�� ����", "���� �ɺ� �ɺ� �ɺ�����",
                                        "���� ���� ���� ��     ��     ��", ""};

        blackLyrics = new List<string> { "", "        �ɺ� �ɺ� �ɺ�����",
                                         "", "", "", "", "", "", "",
                                         "        �ɺ� �ɺ� �ɺ�����",
                                         "", "        �ɺ� �ɺ� �ɺ�����", "", ""};

        greenLyrics = new List<string> { "", "",
                                         "                 õõ�� ����",
                                         "", "", "���� ����                  ",
                                         "���� ����                  ",
                                         "                 �޿��- - ��",
                                         "                 �޿��- -",
                                         "", "                 õõ�� ����",
                                         "", "", ""};

        pinkLyrics = new List<string> { "", "", "", "", "",
                                        "                     ��      ��",
                                        "                     ��      ��",
                                        "", "", "", "", "",
                                        "                         ��     ��     ��", ""};

        wormTimings = new List<float> { 0f, 8.2f,
                                        12.7f, 19.2f,
                                        23.5f,
                                        27.5f,

                                        29.8f, 32.1f,
                                        34.4f, 38.7f,
                                        43.2f, 49.4f,
                                        53.4f, 57.4f};
    }
}
