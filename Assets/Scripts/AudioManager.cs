using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder (-100)]
public class AudioManager : SingletonMonoBehaviour<AudioManager> {
    private Dictionary<string, AudioSource> BGMSource = new Dictionary<string, AudioSource> ();
    private AudioSource SESource = null;
    private Dictionary<string, AudioClip> SEClip = new Dictionary<string, AudioClip> ();

    protected override void Awake () {
        base.Awake ();

        RegisterAudioData ();
    }

    private void RegisterAudioData () {
        object[] bgmData = Resources.LoadAll ("Audio/BGM");
        object[] seData = Resources.LoadAll ("Audio/SE");

        foreach (AudioClip bgm in bgmData) {
            var audioSource = gameObject.AddComponent<AudioSource> ();
            BGMSource[bgm.name] = audioSource;
            BGMSource[bgm.name].clip = bgm;
        }

        foreach (AudioClip se in seData) {
            SEClip[se.name] = se;
        }

        SESource = gameObject.AddComponent<AudioSource> ();
    }

    /// <summary>
    /// seをならす
    /// </summary>
    public void PlaySE (string name, float volume = 1) {
        if (!SEClip.ContainsKey (name)) return;

        SESource.PlayOneShot (SEClip[name] as AudioClip, volume);
    }

    /// <summary>
    /// bgmをならす
    /// </summary>
    public void PlayBGM (string name, bool isLoop = true, float volume = 1) {
        if (!BGMSource.ContainsKey (name)) return;
        if (BGMSource[name].isPlaying) return;

        BGMSource[name].loop = isLoop;
        BGMSource[name].volume = volume;
        BGMSource[name].Play ();
    }

    /// <summary>
    /// bgmを止める
    /// </summary>
    public void StopBGM (string name) {
        if (!BGMSource.ContainsKey (name)) return;
        if (!BGMSource[name].isPlaying) return;

        BGMSource[name].Stop ();
    }

    public float[] GetBGMSpectrumData (string name, int numSumples) {
        if (!BGMSource.ContainsKey (name)) return null;

        float[] spectrum = new float[numSumples];
        BGMSource[name].GetSpectrumData (spectrum, 0, FFTWindow.BlackmanHarris);
        return spectrum;
    }

    /// <summary>
    /// 指定した拍からどれくらいずれているかを取得(秒)
    /// </summary>
    public float GetGapFromBeat (string name, float targetBeat) {
        if (!BGMSource.ContainsKey (name)) return -1;

        float bpm = ConvertNameToBPM (name);
        if (bpm == -1) return -1;

        return Mathf.Abs(BGMSource[name].time - 1 / (bpm / 60) * targetBeat);
    }

    /// <summary>
    /// 指定した曲の1拍の長さを取得(秒)
    /// </summary>
    public float GetBeatLength (string name) {
        if (!BGMSource.ContainsKey (name)) return -1;

        float bpm = ConvertNameToBPM (name);
        if (bpm == -1) return -1;

        return (bpm / 60);
    }

    /// <summary>
    /// 指定した曲の現在の再生時間を取得
    /// </summary>
    public float GetTime (string name) {
        if (!BGMSource.ContainsKey (name)) return -1;
        return BGMSource[name].time;
    }

    private float ConvertNameToBPM (string name) {
        string[] nameSplit = name.Split ('_');
        string bpm = nameSplit[1];
        if (bpm is null) return -1;
        return int.Parse (bpm);
    }
}