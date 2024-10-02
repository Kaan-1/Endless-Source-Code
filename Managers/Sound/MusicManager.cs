using System.Collections;
using Endless_it1;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour{
    private static AudioSource CurrentAudioSource;
    public Slider volumeSlider;
    private ObjectStore os;


    //starts musictracker, sets up volume slider for music
    void Awake(){
        os = FindObjectOfType<ObjectStore>();
        os.mt.Start();
        volumeSlider.value = GameData.musicVolume;
        if (SceneManager.GetActiveScene().name == "Scene0"){
            volumeSlider.onValueChanged.AddListener(OnVolumeChange);
            SetCurrentAudioSource();
        }
        else if (SceneManager.GetActiveScene().name == "StartingScene"){
            SetCurrentAudioSource(false);
        }
        else if (SceneManager.GetActiveScene().name == "Minigame"){
            SetCurrentAudioSource();
        }
    }


    //plays music if it hasn't been played, unpauses it it was unpaused
    public static void PlayMusic(){
        if (CurrentAudioSource.time==0){
            CurrentAudioSource.Play();
        }
        else{
            CurrentAudioSource.UnPause();
        }
    }


    public static void PauseMusic(){
        CurrentAudioSource.Pause();
    }


    //finds the next music to play and saves it as the AudioSource
    //connects new music with the volume slider
    public async void SetCurrentAudioSource(bool playOnStart=true){
        if (CurrentAudioSource != null){
            StartFadeOut(1f);
        }
        AudioSource audioSource = await os.mt.FindNextMusic();
        CurrentAudioSource = audioSource;
        if (volumeSlider != null){
            CurrentAudioSource.volume = volumeSlider.value;
            volumeSlider.onValueChanged.AddListener(OnVolumeChange);
            CurrentAudioSource.volume = GameData.musicVolume;
        }
        if (playOnStart){
            PlayMusic();
        }
    }


    void OnVolumeChange(float value){
        GameData.musicVolume = value;
        CurrentAudioSource.volume = value;
    }


    public void StartFadeOut(float duration){
        StartCoroutine(FadeOutRoutine(CurrentAudioSource, duration));
    }


    //routine for fading out the music
    private IEnumerator FadeOutRoutine(AudioSource audioSource, float duration){
        float startVolume = audioSource.volume;
        float elapsed = 0f;
        while (elapsed < duration){
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
            yield return null;
        }
        audioSource.volume = 0f;
    }
}
