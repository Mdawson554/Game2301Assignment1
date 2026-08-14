using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    public AudioSource bgAudioSource;
    public AudioSource ambientAudioSource;
    public AudioClip[] bgMusic;
    public AudioClip[] ambientAudio;
    
    [SerializeField] private bool autoPlayBgMusic = true; // Toggle this in Inspector to auto-start
    [SerializeField] private bool autoPlayAmbient = false;
    
    private int _bgMusicIndex;
    private int _ambientAudioIndex;
    private Coroutine _bgMusicCoroutine;
    private Coroutine _ambientAudioCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (autoPlayBgMusic && bgMusic.Length > 0)
        {
            PlayBgMusic();
        }
        if (autoPlayAmbient && ambientAudio.Length > 0)
        {
            PlayAmbientAudio();
        }
    }

    public void PlayBgMusic()
    {
        if (_bgMusicCoroutine != null)
            StopCoroutine(_bgMusicCoroutine);
        _bgMusicCoroutine = StartCoroutine(BgMusicPlaylist());
    }

    public void PlayAmbientAudio()
    {
        if (_ambientAudioCoroutine != null)
            StopCoroutine(_ambientAudioCoroutine);
        _ambientAudioCoroutine = StartCoroutine(AmbientAudioPlaylist());
    }

    private IEnumerator BgMusicPlaylist()
    {
        while (true)
        {
            if (bgMusic[_bgMusicIndex] == null)
            {
                _bgMusicIndex = (_bgMusicIndex + 1) % bgMusic.Length;
                continue;
            }
            bgAudioSource.clip = bgMusic[_bgMusicIndex];
            bgAudioSource.Play();
            yield return new WaitForSeconds(bgAudioSource.clip.length);
            _bgMusicIndex++;
            if (_bgMusicIndex >= bgMusic.Length) _bgMusicIndex = 0;
        }
    }

    private IEnumerator AmbientAudioPlaylist()
    {
        while (true)
        {
            if (ambientAudio[_ambientAudioIndex] == null)
            {
                _ambientAudioIndex = (_ambientAudioIndex + 1) % ambientAudio.Length;
                continue;
            }
            ambientAudioSource.clip = ambientAudio[_ambientAudioIndex];
            ambientAudioSource.Play();
            yield return new WaitForSeconds(ambientAudioSource.clip.length);
            _ambientAudioIndex++;
            if (_ambientAudioIndex >= ambientAudio.Length) _ambientAudioIndex = 0;
        }
    }
}