namespace Endless_it1{
    public static class ErrorTexts{

        public static string noImageAPI = "Unfortunately, the image generation services from GetImg are currently down. The game is still playable, but images corresponding to the story won't be generated. Would you like to continue?";
        public static string noVoiceAPI = "Unfortunately, the voice generation services from Google Cloud are currently down. The game is still playable, but the story and the characters won't be narrated vocally. Would you like to continue?";
        public static string noImageAndVoiceAPI = "Unfortunately, the image generation services from GetImg and voice generation services from Google Cloud are currently down. The game is still playable, but images corresponding to the story won't be generated. Also the story and the characters won't be narrated vocally. Would you like to continue?";
        public static string noTextAPI = "Unfortunately, the text generation services from OpenAI are currently down, which makes the game unplayable since the story can't be generated. I am very sorry for the inconvinience. Please come back another time.";
        public static string noInternet = "Unfortunately, the game requires an internet connection to function. Please connect to the internet and re-open the game.";
    }
}