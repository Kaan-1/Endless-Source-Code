using UnityEngine;
using Endless_it1;

public class MusicFinder : MonoBehaviour{

    private ObjectStore os;

    public void Start(){
        os = FindObjectOfType<ObjectStore>();
    }

    public AudioSource FindAudioSource(string cleanedStoryType, bool moodIsPositive){
        if (GameData.storyMood == null){
            return MusicStore.GetIntroSong();
        }
        return cleanedStoryType switch{
            "adventure" => moodIsPositive ? MusicStore.GetHappyAdventure() : MusicStore.GetSadAdventure(),
            "drama" => moodIsPositive ? MusicStore.GetHappyDrama() : MusicStore.GetSadDrama(),
            "fantasy" => moodIsPositive ? MusicStore.GetHappyFantasy() : MusicStore.GetSadFantasy(),
            "history" => moodIsPositive ? MusicStore.GetHappyHistorical() : MusicStore.GetSadHistorical(),
            "horror" => moodIsPositive ? MusicStore.GetHappyHorror() : MusicStore.GetSadHorror(),
            "mystery" => moodIsPositive ? MusicStore.GetHappyMystery() : MusicStore.GetSadMystery(),
            "reallife" => moodIsPositive ? MusicStore.GetHappyRealLife() : MusicStore.GetSadRealLife(),
            "romance" => moodIsPositive ? MusicStore.GetHappyRomantic() : MusicStore.GetSadRomantic(),
            "sciencefiction" => moodIsPositive ? MusicStore.GetHappyScienceFiction() : MusicStore.GetSadScienceFiction(),
            "thriller" => moodIsPositive ? MusicStore.GetHappyThriller() : MusicStore.GetSadThriller(),
            _ => MusicStore.GetIntroSong(),
        };
    }
}