using System.Collections;
using UnityEngine;

public enum SFXType
{
    Arrow,
    Die,
    Attack,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;


    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource sfxAudioSource;

    public AudioClip bgmClip;   // 배경음

    public AudioClip[] soundClip;   // 효과음


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //PlayBGM();

    }


    void PlayBGM()
    {
        bgmAudioSource.Play();
    }

    public void PlaySFX(SFXType type)
    {
        if ((int)type >= soundClip.Length)
            return;

        sfxAudioSource.PlayOneShot(soundClip[(int)type]);
    }

    IEnumerator BGMChange()
    {
        float vol = bgmAudioSource.volume;

        float timer = 0f;
        float duration = 5f;

        while (vol > 0)
        {
            timer += Time.deltaTime;
            bgmAudioSource.volume = Mathf.Lerp(vol, 0, timer / duration);
            yield return null;
        }

        bgmAudioSource.Stop();

        // 새로운 bgm으로 바꿔서
        // 점점 볼륨을 높임
    }

}
