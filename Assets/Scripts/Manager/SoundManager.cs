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

    //���� ��� ������Ʈ ����
    [SerializeField]
    private AudioSource sfxSource = null;
    [SerializeField]
    private AudioSource bgmSource = null;

    //���Ǵ� ���� ����Ʈ
    [SerializeField]
    private AudioClip[] sfxList = { };
    [SerializeField]
    private AudioClip[] bgmList = { };

    #region �̱���
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

    //��� �÷���
    #endregion
    public static void PlayBgm(BGM _bgmType)
    {
        Instance.bgmSource.loop = true;
        int bgmType = (int)_bgmType;
        Instance.bgmSource.clip = Instance.bgmList[bgmType];
        Instance.bgmSource.Play();
    }

    //ȿ���� �÷���
    public static void PlaySfx(SFX _sfxType)
    {
        int sfxNumber = (int)_sfxType;
        Instance.sfxSource.loop = false;
        Instance.sfxSource.PlayOneShot(Instance.sfxList[sfxNumber], Instance.sfxSource.volume);
    }

    //���� �ߴ�
    public static void StopSfx()
    {
        Instance.sfxSource.Stop();
    }
    public static void StopBgm()
    {
        Instance.bgmSource.Stop();
    }

    //���Ұ� �Լ�
    public static void SetMuteSfx(bool _mute)
    {
        Instance.sfxSource.mute = _mute;
    }
    public static void SetMuteBgm(bool _mute)
    {
        Instance.bgmSource.mute = _mute;
    }


    //�Ҹ�ũ�� ��ȯ
    public static void ChangeSfxVolume(float _value)
    {
        Instance.sfxSource.volume = _value;
    }
    public static void ChangeBgmVolume(float _value)
    {
        Instance.bgmSource.volume = _value;
    }

    //���� ũ�� ��ȯ
    public static float GetVolumeSfx()
    {
        return Instance.sfxSource.volume;
    }
    public static float GetVolumeBgm()
    {
        return Instance.bgmSource.volume;
    }

    //���Ұ� ���� ��ȯ
    public static bool GetMuteSfx()
    {
        return Instance.sfxSource.mute;
    }
    public static bool GetMuteBgm()
    {
        return Instance.bgmSource.mute;
    }
}
