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
        // 가사와 타이밍 초기화
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
        cornLyrics = new List<string> { "옥수수", "",
                                        "", "",
                                        "",
                                        "",

                                        "옥수수는 옥수수는", "너무 너무 너무 맛있죠",
                                        "", "생으로 먹으면",
                                        "", "삶아서 먹으면",
                                        "알알이 톡 토도도도독", "튀겨서 먹으면",
                                        "팝콘이 팡 파파파파팡", "옥수수의 다양한",
                                        "식감은 너무           ",
                                        "", "",
                                        "울퉁 불퉁 옥수수는", "너무 맛있죠",
                                        "울퉁 불퉁 옥수수는", "너무 맛있죠", ""};

        blackLyrics = new List<string> { "", "                올록 볼록",
                                        "올통 볼통                ", "올록 볼록 올록 볼록해",
                                        "옥수수!",
                                        "울퉁 불퉁 울퉁 불퉁",

                                        "", "",
                                        "옥수수!", "",
                                        "오독 오도독   ", "",
                                        "                토도도도독", "",
                                        "                파파파파팡", "",
                                        "                    재밌어",
                                        "                올통 볼통", "",
                                        "울퉁 불퉁               ", "        맛있죠",
                                        "울퉁 불퉁               ", "        맛있죠", ""};

        redLyrics = new List<string> { "", "",
                                        "                올통 볼통", "",
                                        "",
                                        "",

                                        "", "",
                                        "", "",
                                        "                            오도독", "",
                                        "", "",
                                        "", "",
                                        "",
                                        "", "울룩 불룩해",
                                        "", "",
                                        "", "", ""};

        blueLyrics = new List<string> { "", "올록 볼록                ",
                                        "", "",
                                        "",
                                        "",

                                        "", "",
                                        "", "",
                                        "오독                               ", "",
                                        "", "",
                                        "", "",
                                        "",
                                        "올록 볼록                ", "",
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
