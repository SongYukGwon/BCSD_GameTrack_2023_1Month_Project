using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public enum SFX
{
    BUTTON,
    DEAD,
    COIN
}

public enum BGM
{
    INGAME,
}


public class SoundManager : MonoBehaviour, ISingleton
{

    public static SoundManager Instance;

    //사운드 출력 컴포넌트 변수
    [SerializeField]
    private AudioSource sfxSource = null;
    [SerializeField]
    private AudioSource bgmSource = null;

    //사용되는 음원 리스트
    [SerializeField]
    private AudioClip[] sfxList = { };
    [SerializeField]
    private AudioClip[] bgmList = { };

    #region 싱글톤
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //브금 플레이
    #endregion
    public static void PlayBgm(BGM _bgmType)
    {
        Instance.bgmSource.loop = true;
        int bgmType = (int)_bgmType;
        Instance.bgmSource.clip = Instance.bgmList[bgmType];
        Instance.bgmSource.Play();
    }

    //효과음 플레이
    public static void PlaySfx(SFX _sfxType)
    {
        int sfxNumber = (int)_sfxType;
        Instance.sfxSource.loop = false;
        Instance.sfxSource.PlayOneShot(Instance.sfxList[sfxNumber], Instance.sfxSource.volume);
    }

    //음악 중단
    public static void StopSfx()
    {
        Instance.sfxSource.Stop();
    }
    public static void StopBgm()
    {
        Instance.bgmSource.Stop();
    }

    //음소거 함수
    public static void SetMuteSfx(bool _mute)
    {
        Instance.sfxSource.mute = _mute;
    }
    public static void SetMuteBgm(bool _mute)
    {
        Instance.bgmSource.mute = _mute;
    }


    //소리크기 변환
    public static void ChangeSfxVolume(float _value)
    {
        Instance.sfxSource.volume = _value;
    }
    public static void ChangeBgmVolume(float _value)
    {
        Instance.bgmSource.volume = _value;
    }

    //볼륨 크기 반환
    public static float GetVolumeSfx()
    {
        return Instance.sfxSource.volume;
    }
    public static float GetVolumeBgm()
    {
        return Instance.bgmSource.volume;
    }

    //음소거 여부 반환
    public static bool GetMuteSfx()
    {
        return Instance.sfxSource.mute;
    }
    public static bool GetMuteBgm()
    {
        return Instance.bgmSource.mute;
    }
}
