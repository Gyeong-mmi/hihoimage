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
        // °¡»ç¿Í Å¸ÀÌ¹Ö ÃÊ±âÈ­
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
        wormLyrics = new List<string> { "²Éº¬ÀÌ", "³ª´Â ²Éº¬ ²Éº¬ ²Éº¬ÀÌÁö",
                                        "³ª´Â ¾ÆÁÖ ÃµÃµÈ÷ ±â¾î°¡Áö", "´Ù¸®´Â ³Ê¹« ³Ê¹« Âª¾Æ",
                                        "µî¿¡ ÀÖ´Â ÂÞ±¼ÂÞ±¼ ÁÖ¸§À¸·Î",
                                        "",

                                        "", "²ÞÆ² ²ÞÆ² ÂÞ¿ì¿ì- - ¿í",
                                        "²ÞÆ² ²ÞÆ² ÂÞ¿ì¿ì- -", "³ª´Â ²Éº¬ ²Éº¬ ²Éº¬ÀÌÁö",
                                        "³ª´Â ¾ÆÁÖ ÃµÃµÈ÷ ±â¾î°¡Áö", "³ª´Â ²Éº¬ ²Éº¬ ²Éº¬ÀÌÁö",
                                        "³ª´Â ²¿¹° ²¿¹° ²É     º¬     ÀÌ", ""};

        blackLyrics = new List<string> { "", "        ²Éº¬ ²Éº¬ ²Éº¬ÀÌÁö",
                                         "", "", "", "", "", "", "",
                                         "        ²Éº¬ ²Éº¬ ²Éº¬ÀÌÁö",
                                         "", "        ²Éº¬ ²Éº¬ ²Éº¬ÀÌÁö", "", ""};

        greenLyrics = new List<string> { "", "",
                                         "                 ÃµÃµÈ÷ ±â¾î°¡Áö",
                                         "", "", "²¿¹° ²¿¹°                  ",
                                         "²¿¹° ²¿¹°                  ",
                                         "                 ÂÞ¿ì¿ì- - ¿í",
                                         "                 ÂÞ¿ì¿ì- -",
                                         "", "                 ÃµÃµÈ÷ ±â¾î°¡Áö",
                                         "", "", ""};

        pinkLyrics = new List<string> { "", "", "", "", "",
                                        "                     Âß      Âß",
                                        "                     Âß      Âß",
                                        "", "", "", "", "",
                                        "                         ²É     º¬     ÀÌ", ""};

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
