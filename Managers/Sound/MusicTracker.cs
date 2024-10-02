using System.Collections.Generic;
using Endless_it1;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class MusicTracker : MonoBehaviour{
    private ObjectStore os;


    //initalizes storyTypes and sets up objects
    public void Start(){
        if (!GameData.storyTypesInitialized){
            GameData.storyTypes = new List<string>(){
            "adventure",
            "drama",
            "fantasy",
            "history",
            "horror",
            "mystery",
            "reallife",
            "romance",
            "sciencefiction",
            "thriller"
            };
            GameData.storyTypesInitialized = true;
        }
        os = FindObjectOfType<ObjectStore>();
        os.mf.Start();
    }


    //finds the next music to be played based on the chapter and the previous story
    public async Task<AudioSource> FindNextMusic(){
        if (SceneManager.GetActiveScene().name == "StartingScene"){
            return MusicStore.GetIntroSong();
        }
        else if(SceneManager.GetActiveScene().name=="Minigame"){
            return GameData.chapter switch{
                1 => MusicStore.GetDuel1(),
                2 => MusicStore.GetDuel2(),
                3 => MusicStore.GetDuel3(),
                _ => MusicStore.GetDuel1(),
            };
        }
        string cleanedStoryMood = CleanString(GameData.storyMood);
        string cleanedStoryType = CleanString(GameData.storyType);
        bool moodIsPositive = FindStoryMoodPositivity(cleanedStoryMood);
        if (GameData.storyTypes.Contains(cleanedStoryType)){
            GameData.storyTypes.Remove(cleanedStoryType);
            GameData.currentStoryTypeForMusic = cleanedStoryType;
            GameData.currentStoryPositivityForMusic = moodIsPositive;
            return os.mf.FindAudioSource(cleanedStoryType, moodIsPositive);
        }
        else{
            if (ShouldChangeMusic()){
                var newStoryType = os.cmStory.GetNewStoryType(ToString(GameData.storyTypes));
                var newStoryMood = os.cmStory.GetNewStoryMood();
                var typeAndMood = await Task.WhenAll(newStoryType, newStoryMood);
                Debug.Log("new story type response from the API:"+typeAndMood[0]);
                Debug.Log("new story mood response from the API:"+typeAndMood[1]);
                typeAndMood[0] = CleanString(typeAndMood[0]);
                typeAndMood[1] = CleanString(typeAndMood[1]);
                bool newMoodIsPositive = FindStoryMoodPositivity(typeAndMood[1]);
                GameData.storyTypes.Remove(typeAndMood[0]);
                GameData.currentStoryTypeForMusic = typeAndMood[0];
                GameData.currentStoryPositivityForMusic = newMoodIsPositive;
                return os.mf.FindAudioSource(typeAndMood[0], newMoodIsPositive);
            }
            else{
                return os.mf.FindAudioSource(GameData.currentStoryTypeForMusic, GameData.currentStoryPositivityForMusic);
            }
        }
    }


    //determines whether the storyMood is positive or not
    private bool FindStoryMoodPositivity(string storyMood){
        if (storyMood=="happy" || storyMood=="imaginative" || storyMood=="exciting" || storyMood=="romantic" || storyMood=="blissful" || storyMood=="positive"){
            return true;
        }
        else{
            return false;
        }
    }


    private string ToString(List<string> list){
        string result = "";
        foreach (string item in list){
            result += item + "\n";
        }
        return result;
    }


    //takes out any punctuation and white space from the string, lowers all its characters
    private string CleanString(string s){
        return Regex.Replace(s.ToLower(), @"[\s\p{P}]", "");
    }


    private bool ShouldChangeMusic(){
        return os.gfc.chapterSteps.Contains(GameData.currentStep);
    }
}