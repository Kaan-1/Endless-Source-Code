using System.Collections.Generic;
using Endless_it1;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour{
    public Slider volumeSlider;


    //sets up the volume
    void Start(){
        foreach (AudioSource sound in SoundStore.GetAllSounds()){
            if (sound!=null){sound.volume = GameData.soundVolume;}
        }
        volumeSlider.value = GameData.soundVolume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }


    //gets called from unity
    public static void PlayMouseClick(){
        SoundStore.GetMouseClick().Play();
    }


    //gets called from unity
    public static void PlayEnterClick(){
        SoundStore.GetEnterClick().Play();
    }


    public static void PlayCountdownClick(){
        SoundStore.GetCountdownClick().Play();
    }


    public static void PlayCountdownHorn(){
        SoundStore.GetCountdownHorn().Play();
    }


    public static void PlayDamage(){
        SoundStore.GetDamage().Play();
    }


    public static void PlayBurningLooped(){
        SoundStore.GetBurningLooped().Play();
    }
    
    public static void StopBurningLooped(){
        SoundStore.GetBurningLooped().Stop();
    }


    public static void PlayHit(){
        SoundStore.GetHit().Play();
    }


    public static void SetVolume(float volume){
        GameData.soundVolume = volume;
        foreach (AudioSource sound in SoundStore.GetAllSounds()){
            if (sound!=null){sound.volume = volume;}
        }
    }

    public static void PauseBurningLooped(){
        SoundStore.GetBurningLooped().Pause();
    }

    public static void UnPauseBurningLooped(){
        SoundStore.GetBurningLooped().UnPause();
    }
}
