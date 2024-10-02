
using System.Collections;
using System.Collections.Generic;

namespace Endless_it1{
    public static class GameData{
        public static bool internetWorks;
        public static bool textAPIWorks;
        public static bool imageAPIWorks;
        public static bool voiceAPIWorks;
        public static string storyMood;
        public static string storyType;
        public static float soundVolume = 1f;
        public static float musicVolume = 1f;
        public static float voiceVolume = 1f;
        public static int chapter = 1;
        public static ArrayList story;
        public static int currentStep;
        public static bool duels = true;
        public static bool paused;
        public static bool storyTypesInitialized=false;
        public static List<string> storyTypes;
        public static string currentStoryTypeForMusic;
        public static bool currentStoryPositivityForMusic;
        public static bool escToggleAllowed;
    }
}