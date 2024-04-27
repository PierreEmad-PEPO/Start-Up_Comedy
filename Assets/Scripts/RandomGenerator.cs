using UnityEngine;

public static class RandomGenerator
{

    static RandomGenerator()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    public static int NextInt(int minValue, int maxValue)
    {
        return Random.Range(minValue, maxValue);
    }

    public static float NextFloat(float minValue, float maxValue)
    {
        float randomValue = Random.Range(minValue, maxValue);
        return randomValue;
    }

    public static double NextDouble(double minValue, double maxValue)
    {
        return Random.value * (maxValue - minValue) + minValue;
    }
}