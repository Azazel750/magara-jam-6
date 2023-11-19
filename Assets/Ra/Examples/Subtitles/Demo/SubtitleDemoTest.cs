using Ra.Subtitles;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleDemoTest : MonoBehaviour
{
    public Text text;
    public AudioSource audioSource;
    public DefaultAsset subtitleData;
    public AudioClip audioClip;
    private void Start()
    {
        Say(audioClip, subtitleData);
    }

    public void Say(AudioClip clip, DefaultAsset subtitle)
    {
        SubtitleMaker.Say(text, audioSource, clip, subtitle);
    }
}
