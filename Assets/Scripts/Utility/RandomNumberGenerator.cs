using UnityEngine;

public class RandomNumberGenerator : MonoBehaviour
{
    public static int FlipACoin ()
    {
        int flip = Random.Range(0, 1);
        return flip;
    }

    public static int FlipTwoCoins()
    {
        int firstFlip = Random.Range(0, 1);
        int secondFlip = Random.Range(0, 1);
        int totalFlip = firstFlip + secondFlip;
        return totalFlip;
    }

    public static int RollSixDice()
    {
        int dice = Random.Range(0, 5);
        return dice;
    }

    public static int RollTwoSixDices()
    {
        int firstDice = Random.Range(0, 5);
        int secondDice = Random.Range(0, 5);
        int totalDice = firstDice + secondDice;
        return totalDice;
    }
}
