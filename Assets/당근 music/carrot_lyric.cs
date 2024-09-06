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
        // 가사와 타이밍 초기화
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
        carrotLyrics = new List<string> { "당근", "길쭉 길쭉 당근을",
                                        "뽑아보아요      ", "세모 세모 당근을",
                                        "뽑아보아요      ",
                                        "익힌 당근을 자르면 쓰윽 쓰윽",

                                        "한 입 베어물면", "",
                                        "딱딱한 당근을 자르면", "통 통 통 통",
                                        "한 입 베어물면", "",
                                        "아삭 아삭 당근도", "물컹 물컹 당근도",
                                        "모두 정말 정말", "맛이 있어요     ",
                                        "나는 당근이 정말           ",
                                        "나는 당근이 정말 정말           ", ""};

        blackLyrics = new List<string> { "", "",
                                         "                  쏙!", "",
                                         "                  쑥!", "",
                                         "", "", "", "",
                                         "", "", "", "", "", "                    예!",
                                         "                            좋아요",
                                         "                                   좋아요", ""};

        redLyrics = new List<string> { "", "",
                                       "", "",
                                       "", "                               쓰윽 쓰윽",
                                       "", "                무울컹", "", "",
                                       "", "                아사삭",
                                       "", "", "", "", "", "", ""};

        blueLyrics = new List<string> { "", "", "", "",
                                        "", "", "", "물컹 물컹            ", 
                                        "", "통 통 통 통",
                                        "", "아삭 아삭            ", "", "", "", "",
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
