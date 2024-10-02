using UnityEngine;

public class MusicStore : MonoBehaviour{
//happy sad
//adventure, drama, fantasy, historical, horror, mystery, real life, romantic, science fiction, thriller
    private static MusicStore Instance; //bilerek hatalı bıraktım, music'i hallet
    public AudioSource happyAdventure;
    public AudioSource happyDrama;
    public AudioSource happyFantasy;
    public AudioSource happyHistorical;
    public AudioSource happyHorror;
    public AudioSource happyMystery;
    public AudioSource happyRealLife;
    public AudioSource happyRomantic;
    public AudioSource happyScienceFiction;
    public AudioSource happyThriller;
    public AudioSource sadAdventure;
    public AudioSource sadDrama;
    public AudioSource sadFantasy;
    public AudioSource sadHistorical;
    public AudioSource sadHorror;
    public AudioSource sadMystery;
    public AudioSource sadRealLife;
    public AudioSource sadRomantic;
    public AudioSource sadScienceFiction;
    public AudioSource sadThriller;
    public AudioSource IntroSong;
    public AudioSource duel1;
    public AudioSource duel2;
    public AudioSource duel3;

    private void Awake()
    {
        // Ensure that only one instance exists and don't destroy on load
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static AudioSource GetHappyAdventure(){
        return Instance.happyAdventure;
    }

    public static AudioSource GetHappyDrama(){
        return Instance.happyDrama;
    }

    public static AudioSource GetHappyFantasy(){
        return Instance.happyFantasy;
    }

    public static AudioSource GetHappyHistorical(){
        return Instance.happyHistorical;
    }

    public static AudioSource GetHappyHorror(){
        return Instance.happyHorror;
    }

    public static AudioSource GetHappyMystery(){
        return Instance.happyMystery;
    }

    public static AudioSource GetHappyRealLife(){
        return Instance.happyRealLife;
    }

    public static AudioSource GetHappyRomantic(){
        return Instance.happyRomantic;
    }

    public static AudioSource GetHappyScienceFiction(){
        return Instance.happyScienceFiction;
    }

    public static AudioSource GetHappyThriller(){
        return Instance.happyThriller;
    }

    public static AudioSource GetSadAdventure(){
        return Instance.sadAdventure;
    }

    public static AudioSource GetSadDrama(){
        return Instance.sadDrama;
    }

    public static AudioSource GetSadFantasy(){
        return Instance.sadFantasy;
    }

    public static AudioSource GetSadHistorical(){
        return Instance.sadHistorical;
    }

    public static AudioSource GetSadHorror(){
        return Instance.sadHorror;
    }

    public static AudioSource GetSadMystery(){
        return Instance.sadMystery;
    }

    public static AudioSource GetSadRealLife(){
        return Instance.sadRealLife;
    }

    public static AudioSource GetSadRomantic(){
        return Instance.sadRomantic;
    }

    public static AudioSource GetSadScienceFiction(){
        return Instance.sadScienceFiction;
    }

    public static AudioSource GetSadThriller(){
        return Instance.sadThriller;
    }

    public static AudioSource GetIntroSong(){
        return Instance.IntroSong;
    }

    public static AudioSource GetDuel1(){
        return Instance.duel1;
    }

    public static AudioSource GetDuel2(){
        return Instance.duel2;
    }

    public static AudioSource GetDuel3(){
        return Instance.duel3;
    }
}