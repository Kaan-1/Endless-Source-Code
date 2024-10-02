using Endless_it1;
using GoogleTextToSpeech.Scripts.Example;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class VoiceController : MonoBehaviour{
    public GameObject storytellerTTS;
    public GameObject manTTS;
    public GameObject womanTTS;
    public GameObject otherTTS;
    private static TextToSpeechExample storytellerScriptTTS;
    private static TextToSpeechExample manScriptTTS;
    private static TextToSpeechExample womanScriptTTS;
    private static TextToSpeechExample otherScriptTTS;
    private static AudioSource storytellerAudioTTS;
    private static AudioSource manAudioTTS;
    private static AudioSource womanAudioTTS;
    private static AudioSource otherAudioTTS;
    public Slider voiceSlider;
    private static string lastSynthesis;
    private static Dictionary<string, AudioSource> AudioSources;


    //sets up the TextToSpeechExample objects and it's components
    //connects them with the voiceslider
    void Start(){
        voiceSlider.value = GameData.voiceVolume;
        if (SceneManager.GetActiveScene().name == "Scene0"){
            storytellerScriptTTS = storytellerTTS.GetComponent<TextToSpeechExample>();
            manScriptTTS = manTTS.GetComponent<TextToSpeechExample>();
            womanScriptTTS = womanTTS.GetComponent<TextToSpeechExample>();
            otherScriptTTS = otherTTS.GetComponent<TextToSpeechExample>();
            storytellerAudioTTS = storytellerTTS.GetComponent<AudioSource>();
            manAudioTTS = manTTS.GetComponent<AudioSource>();
            womanAudioTTS = womanTTS.GetComponent<AudioSource>();
            otherAudioTTS = otherTTS.GetComponent<AudioSource>();
            voiceSlider.onValueChanged.AddListener(SetVolume);
            storytellerAudioTTS.volume = GameData.voiceVolume;
            manAudioTTS.volume = GameData.voiceVolume;
            womanAudioTTS.volume = GameData.voiceVolume;
            otherAudioTTS.volume = GameData.voiceVolume;
            AudioSources = new Dictionary<string, AudioSource>{
                { "storyteller", storytellerAudioTTS },
                { "woman", womanAudioTTS },
                { "man", manAudioTTS },
                { "other", otherAudioTTS }
            };
        }
        else if (SceneManager.GetActiveScene().name == "StartingScene"){
            voiceSlider.onValueChanged.AddListener(SetVolume);
        }
    }


    //synthesizes text based on the gaender
    public static void SynthesizeText(string text, string gender=""){
        gender = gender.Replace(" ", "");
        if (gender==""){
            storytellerScriptTTS.SynthesizeText(text);
            lastSynthesis = "storyteller";
        }
        else if (gender=="Woman" || gender=="woman" || gender=="Female" || gender=="female"){
            womanScriptTTS.SynthesizeText(text);
            lastSynthesis = "woman";
        }
        else if (gender=="Man" || gender=="man" || gender=="Male" || gender=="male"){
            manScriptTTS.SynthesizeText(text);
            lastSynthesis = "man";
        }
        else{
            otherScriptTTS.SynthesizeText(text);
            lastSynthesis = "other";
        }
    }


    private static void SetVolume(float volume){
        GameData.voiceVolume = volume;
        if (SceneManager.GetActiveScene().name == "Scene0"){
            storytellerAudioTTS.volume = volume;
            manAudioTTS.volume = volume;
            womanAudioTTS.volume = volume;
            otherAudioTTS.volume = volume;
        }
    }


    public static void StopVoice(){
        if (lastSynthesis != null && AudioSources.TryGetValue(lastSynthesis, out var audioSource)){
            audioSource.Stop();
        }
    }


    //plays the last synthesised text
    public static void PlayVoice(){
        if (lastSynthesis != null && AudioSources.TryGetValue(lastSynthesis, out var audioSource)){
            audioSource.Play();
        }
    }

    public static void PauseVoice(){
        if (lastSynthesis != null && AudioSources.TryGetValue(lastSynthesis, out var audioSource)){
            audioSource.Pause();
        }
    }

    public static void UnPauseVoice(){
        if (lastSynthesis != null && AudioSources.TryGetValue(lastSynthesis, out var audioSource)){
            audioSource.UnPause();
        }
    }
}
