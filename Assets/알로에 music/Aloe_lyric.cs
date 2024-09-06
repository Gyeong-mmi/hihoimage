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
        // °¡»ç¿Í Å¸ÀÌ¹Ö ÃÊ±âÈ­
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
        aloeLyrics = new List<string> { "¾Ë·Î¿¡", "ÂßÂß »¸¾îÀÖ´Â »Ô³­¸Ó¸® ¾Ë·Î¿¡",
                                        "¿Õ°üÃ³·³ »ÏÁ·»ÏÁ· ¾Ë·Î¿¡", "¾Ë·Î¿¡ÀÇ ¼ÓÀº Åõ¸íÇØ¿ä",
                                        "À¯¸®Ã³·³ ¹ÝÂ¦¹ÝÂ¦ ºûÀÌ ³ª¿ä",
                                        "Åõ¸íÇÑ ¾Ë·Î¿¡¸¦ ¸¸Áö¸é",

                                        "¸Çµé ¸Çµé ¹Ýµé ¹Ýµé", "¸Å²ø ¸Å²ø ¹Ì²ø ¹Ì²ø",
                                        "¸Çµé ¸Çµé ¹Ýµé ¹Ýµé", "¸Å²ø ¸Å²ø ¹Ì²ø ¹Ì²ø",
                                        "¹ÝÂ¦¹ÝÂ¦ ¾Ë·Î¿¡¸¦ ¸¸Áö¸é", "¼Õ°¡¶ô »çÀÌ·Î ´Ã¾î³­´Ù",
                                        "Áøµæ Âðµæ ²ôÀºÀû", "°Å¹ÌÁÙÀÌ »ý°Ü³­´Ù",
                                        "Áøµæ Âðµæ ²ôÀºÀû", "»ÏÁ·»ÏÁ· »Ô³­¸Ó¸® ¾Ë·Î¿¡",
                                        "¹Ì²ö¹Ì²ö Åõ¸íÇÑ            ", ""};

        blackLyrics = new List<string> { "", "                                      ¾Ë·Î¿¡",
                                         "                              ¾Ë·Î¿¡", "",
                                         "¹ÝÂ¦¹ÝÂ¦ ",
                                         "",

                                         "                ¹Ýµé ¹Ýµé", "¸Å²ø ¸Å²ø                ",
                                         "                ¹Ýµé ¹Ýµé", "¸Å²ø ¸Å²ø                ",
                                         "", "",
                                         "", "",
                                         "", "                              ¾Ë·Î¿¡",
                                         "                          ¾Ë·Î¿¡", ""};

        redLyrics = new List<string> { "", "",
                                       "", "",
                                       "",
                                       "",

                                       "", "                ¹Ì²ø ¹Ì²ø",
                                       "", "                ¹Ì²ø ¹Ì²ø",
                                       "", "",
                                       "", "",
                                       "", "",
                                       "", ""};

        blueLyrics = new List<string> { "", "",
                                        "", "",
                                        "",
                                        "",

                                        "¸Çµé ¸Çµé                ", "",
                                        "¸Çµé ¸Çµé                ", "",
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
                                         "Áøµæ Âðµæ ²ôÀºÀû", "",
                                         "Áøµæ Âðµæ ²ôÀºÀû", "",
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
