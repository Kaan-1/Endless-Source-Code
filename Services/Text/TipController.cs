public static class TipController{
    private static readonly string[] tips = {"You can take action by putting your response between asterisks\ne.g. *opens the chest and sees an old map*", 
                                        "You can manually introduce characters by calling their name in the dialogue\ne.g. What about you Ms Redwood?"};
    private static int currentTip = 0;

    public static string GetTip(){
        string tip = $"Tip: {tips[currentTip]}";
        currentTip++;
        if (currentTip == tips.Length){
            currentTip = 0;
        }
        return tip;
    }
}
