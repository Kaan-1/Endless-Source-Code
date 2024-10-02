using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameServices : MonoBehaviour{

    public static void PauseGame(){
        Time.timeScale = 0;
        SoundManager.PauseBurningLooped();
        MusicManager.PauseMusic();
        VoiceController.PauseVoice();
        UILockManager.LockFromOptions();
    }

    public static void ResumeGame(){
        Time.timeScale = 1;
        SoundManager.UnPauseBurningLooped();
        MusicManager.PlayMusic();
        VoiceController.UnPauseVoice();
        UILockManager.UnlockFromOptions();
    }


}
