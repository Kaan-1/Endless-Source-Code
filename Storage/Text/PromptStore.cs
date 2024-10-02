using System;
namespace Endless_it1{
    static class PromptStore{
        public static string GetStartingPrompt(){
            return @"You will progress the story and roleplay various characters in a user-response-driven story game where the player is the main character. Your role is to shape the narrative based on the player's responses and your creativity.
            Game Structure:
            1.	Default Story (6-7 lines): Start with a default story to set the scene.
            2.	Character Interaction: Select a character for the player to converse with. Roleplay this character and guide the conversation.
            3.	Story Progression (4-5 lines): Progress the story based on the conversation.
            4.	Next Character Interaction: Choose another character for the next conversation. Roleplay this character and guide the dialogue.
            5.	Repeat Steps 3-4: Continue this pattern, ensuring each interaction and progression is influenced by the player's responses and your creative input.
            Guidelines:
            •	Creative Impact: Add twists and surprises at every turn to keep the player engaged. But always be realistic and consistent with the story.
            •	Introduce New Characters: Introduce new characters with distinct personalities in each story progression. Make sure the describe their personalities in the progressions.
            •	Player-Driven: Ensure the story and character personalities evolve based on the player's responses.
            •	Engagement: Be imaginative and engaging to maintain the player's interest.
            Note: I will provide additional instructions and guidance throughout the game to ensure smooth progression and clarify any uncertainties.
            Default Story is as follows:
            ";
        }
        public static string GetConversationAfterStartingPrompt(){
            return $@"Now that you have the default story, identify a character for the player to converse with and craft their opening line in the context of a {GameData.storyMood} {GameData.storyType} story. Provide the character's name, gender and response. Keep the response 1 or 2 sentences long";
        }
        public static string GetProgressionAfterConversation(){
            return $"Considering the story and the conversations up until now, progress the narrative in the context of a {GameData.storyMood} {GameData.storyType} story from the first-person perspective. Progress the story, introduce a twist, but do not introduce a new character. Your response should be 4-5 sentences long.";
        }
        public static string GetEndOfChapter(){
            return $"Considering the story and the conversations up until now, progress the narrative in the context of a {GameData.storyMood} {GameData.storyType} story from the first-person perspective. Progress the story and introduce a major plot twist that leaves the character shocked and surprised. Your response should be 6-7 sentences long.";
        }
        public static string GetProgressionAfterChapter(){
            return $"Considering the story and the conversations up until now, progress the narrative in the context of a {GameData.storyMood} {GameData.storyType} story from the first-person perspective. Progress the story, but don't introduce a new character and don't include a twist in the story. Your response should be 4-5 sentences long.";
        }
        public static string GetProgressionAfterChapterWithNew(){
            return $"Considering the story and the conversations up until now, progress the narrative in the context of a {GameData.storyMood} {GameData.storyType} story from the first-person perspective. Progress the story, introduce a new character and describe his/her/its personality. But do not include a twist in the story this time. Your response should be 4-5 sentences long.";
        }
        public static string GetEndOfStory(){
            return $"Considering the story and the conversations up until now, progress the narrative to its conclusion in the context of a {GameData.storyMood} {GameData.storyType} story from the first-person perspective. Decide whether the ending should be good or bad based on previous events. Your response should be 7-8 sentences long. Do not introduce a twist this time.";
        }
        public static string GetDuringConversationPrompt(){
            return $@"Now that the user has responded, craft a reply based on their answer, the situation, and your current character's personality in the context of a {GameData.storyMood} {GameData.storyType} story. Most importantly, DON'T TALK CRYPTIC. Be straight with the user and talk about real events that occured. Also, DO NOT ask a question this time. Provide the character's name, gender and response. Keep the response 1 or 2 sentences long unless a longer response is needed.";
        }
        public static string GetDuringConversationPromptWithQuestion(){
            return $@"Now that the user has responded, craft a reply based on their answer, the situation, and your current character's personality in the context of a {GameData.storyMood} {GameData.storyType} story. Most importantly, DON'T TALK CRYPTIC. Be straight with the user and talk about real events that occured. Ask a question at the end of your response to keep the conversation engaging. Provide the character's name, gender and response. Keep the response 1 or 2 sentences long unless a longer response is needed.";
        }
        public static string GetProgressionAfterConversationWithNew(){
            return $"Considering the story and the conversations up until now, progress the narrative in the context of a {GameData.storyMood} {GameData.storyType} story from the first-person perspective. Progress the story, introduce a twist, introduce a new character, and describe he/she/its personality. Your response should be 4-5 sentences long.";
        }
        public static string GetConversationAfterProgression(){
            return $@"Now that the story has progressed, select a character for the player to converse with and craft their opening line based on the current situation and their personality in the context of a {GameData.storyMood} {GameData.storyType} story. Provide the character's name, gender and response. Keep the response 1 or 2 sentences long.";
        }
        public static string GetProgressionYesOrNo(){
            return $"Considering the story and the conversations up until now, progress the narrative in the context of a {GameData.storyMood} {GameData.storyType} story from the first-person perspective. Your response should be 3-4 sentences long. At the end of your response, introduce a critical situation where the player is faced with a yes or no question. Ensure the question is strictly answerable by 'yes' or 'no' without offering multiple options. Ask yes or no literally at the end of your response";
        }
        public static string GetDefaultStory(){
            return $"Write the beginning of a {GameData.storyMood} {GameData.storyType} story from the first-person perspective of a man. It should be 6-7 lines. Introduce at least one new character.";
        }
        public static string GetHistoricalDefaultStory(){
            Random random = new();
            int century = random.Next(0, 21);
            return century switch{
                0 => $"Write the beginning of a {GameData.storyMood} {GameData.storyType} story from the first-person perspective of a man. It should be 6-7 lines. Introduce at least one new character. Also, since this a historical story, pick a real historical event that occured in BC and make the story about that and make it realistic.",
                _ => $"Write the beginning of a {GameData.storyMood} {GameData.storyType} story from the first-person perspective of a man. It should be 6-7 lines. Introduce at least one new character. Also, since this a historical story, pick a real historical event that occured in the {century}th century and make the story about that and make it realistic.",
            };
        }
        public static string GetConversationImagePrompt(string person){
            return $"{person} talking to you in a {GameData.storyMood} {GameData.storyType} context";
        }
        public static string GetConversationImagePromptsPrompt(string storyProgression){
            return $"{storyProgression}\nSummarize the aesthetics of the above situation takes in 2-3 sentences.";
        }
        public static string GetProgressionBeforeDuel(){
            return $"Considering the story and the conversations up until now, progress the narrative in the context of a {GameData.storyMood} {GameData.storyType} story from the first-person perspective. Your response should be 3-4 sentences long. At the end of your response, create a situation where the user will be dueling someone. Don't ask the user questions, just explain to him that he is entering a duel situation";
        }
        public static string GetProgressionAfterDuel(){
            return $"Considering the result of the duel, progress the narrative in the context of a {GameData.storyMood} {GameData.storyType} story from the first-person perspective. Progress the story, but don't introduce a new character and don't include a twist in the story. Your response should be 4-5 sentences long.";
        }
        public static string GetDuelArenaImagePrompt(string storyProgression){
            return $"{storyProgression}\nSummarize the aesthetics of the above situation takes in 2-3 sentences.";
        }
    }
}