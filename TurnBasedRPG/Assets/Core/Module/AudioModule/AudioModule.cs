using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 音频模块
/// </summary>
public class AudioModule : SingletonAutoMono<AudioModule>
{

    private string audioPath = "Audio";

    /// <summary>
    /// 背景音乐
    /// </summary>
    public AudioSource bgAudioSource { get; private set; }

    /// <summary>
    /// 效果音乐
    /// </summary>
    public AudioSource efferAudioSource { get; private set; }

    /// <summary>
    /// 音乐列表
    /// </summary>
    private List<AudioClip> audioClips { get; set; } = new List<AudioClip>();
    /// <summary>背景音乐的音量大小</summary>
    public float bgValue { get; private set; } = 1f;
    /// <summary>音效的音量大小</summary>
    public float efferSoundValue { get; private set; } = 1f;

    protected override void Awake()
    {
        base.Awake();
        LoadAllAudio();
        bgAudioSource = gameObject.AddComponent<AudioSource>();
        efferAudioSource = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// 加载所有的Audio
    /// </summary>
    private void LoadAllAudio()
    {
        audioClips.AddRange(Resources.LoadAll<AudioClip>(audioPath));
    }

    //****************************************背景音乐****************************************
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name">背景音乐的名称</param>
    public void PlayBGMusic(string name)
    {
        AudioClip audioClip = audioClips.Find((audioClip) => { return audioClip.name.Equals(name); });
        bgAudioSource.clip = audioClip;
        bgAudioSource.loop = true;
        bgAudioSource.volume = bgValue;
        bgAudioSource.Play();
    }

    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void PauseBGMusic()
    {
        bgAudioSource.Pause();
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBGMusic()
    {
        bgAudioSource.Stop();
    }

    /// <summary>
    /// 改变背景音乐 音量大小
    /// </summary>
    /// <param name="v"></param>
    public void ChangeBGValue(float v)
    {
        bgValue = v;
        bgAudioSource.volume = bgValue;
    }

    //****************************************音乐效果****************************************

    /// <summary>
    /// 播放音效
    /// </summary>
    public void PlayEfferSound(string name, bool isLoop)
    {
        AudioClip audioClip = audioClips.Find((audioClip) => { return audioClip.name.Equals(name); });
        efferAudioSource.clip = audioClip;
        efferAudioSource.loop = isLoop;
        efferAudioSource.volume = efferSoundValue;
        efferAudioSource.Play();
    }

    /// <summary>
    /// 改变音效声音大小
    /// </summary>
    /// <param name="value"></param>
    public void ChangeEfferSoundValue(float value)
    {
        efferSoundValue = value;
    }

    /// <summary>
    /// 停止音效
    /// </summary>
    public void StopEfferSound(AudioSource source)
    {
        //if (soundList.Contains(source))
        //{
        //    soundList.Remove(source);
        //    source.Stop();
        //    GameObject.Destroy(source);
        //}
    }
}
