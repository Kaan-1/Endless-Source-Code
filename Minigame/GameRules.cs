using Endless_it1;

public static class GameRules
{

    public static bool GetBallsCollideWithBoundaries(){
        return GameData.chapter switch
        {
            1 => false,
            2 => true,
            3 => true,
            _ => false,
        };
    }

    public static float GetNPCHealth(){
        return GameData.chapter switch
        {
            1 => 1500f, //1500 //50
            2 => 2000f, //2000 //200
            3 => 3000f, //3000 //300
            _ => 100f,
        };
    }

    public static int GetNPCDamage(){
        return GameData.chapter switch
        {
            1 => 20,
            2 => 12,
            3 => 10,
            _ => 10,
        };
    }

    public static float GetNPCSpeed(){
        return GameData.chapter switch
        {
            1 => 3f,
            2 => 4f,
            3 => 4.5f,
            _ => 1f,
        };
    }

    public static float GetNPCShootingFrequency(){
        return GameData.chapter switch
        {
            1 => 0.75f,
            2 => 0.7f,
            3 => 0.6f,
            _ => 10f,
        };
    }

    public static float GetNPCBallSpeed(){
        return GameData.chapter switch
        {
            1 => 20f,
            2 => 17.5f,
            3 => 20f,
            _ => 3f,
        };
    }

    public static float GetBlitzForce(){
        return GameData.chapter switch
        {
            1 => 10f,
            2 => 15f,
            3 => 20f,
            _ => 5f,
        };
    }

    public static float GetBlitzFreq(){
        return GameData.chapter switch
        {
            1 => 0.3f,
            2 => 0.5f,
            3 => 0.45f,
            _ => 100f,
        };
    }
}
