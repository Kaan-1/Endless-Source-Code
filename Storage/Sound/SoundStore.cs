using System.Collections.Generic;
using UnityEngine;

public class SoundStore : MonoBehaviour{
    private static SoundStore Instance;
    public AudioSource emptySound;
    public AudioSource mouseClick;
    public AudioSource enterClick;
    public AudioSource countdownClick;
    public AudioSource countdownHorn;
    public AudioSource damage;
    public AudioSource burningLooped;
    public AudioSource hit;
    private List<AudioSource> allSounds;

    private void Start()
    {
        // Ensure that only one instance exists and don't destroy on load
        if (Instance == null)
        {
            Instance = this;
            allSounds = new List<AudioSource> {mouseClick, enterClick, countdownClick, countdownHorn, damage, burningLooped, hit};
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static List<AudioSource> GetAllSounds(){
        return Instance.allSounds;
    }

    public static AudioSource GetEmptySound(){
        return Instance.emptySound;
    }

    public static AudioSource GetMouseClick(){
        return Instance.mouseClick;
    }

    public static AudioSource GetEnterClick(){
        return Instance.enterClick;
    }

    public static AudioSource GetCountdownClick(){
        return Instance.countdownClick;
    }

    public static AudioSource GetCountdownHorn(){
        return Instance.countdownHorn;
    }

    public static AudioSource GetDamage(){
        return Instance.damage;
    }

    public static AudioSource GetBurningLooped(){
        return Instance.burningLooped;
    }

    public static AudioSource GetHit(){
        return Instance.hit;
    }
}