using System;

public static class RandomGenerator
{
    private static Random random;

    static RandomGenerator()
    {
        int seed = Environment.TickCount;
        random = new Random(seed);
    }

    public static int NextInt(int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue);
    }

    public static float NextFloat(float minValue, float maxValue)
    {
        double randomValue = random.NextDouble() * (maxValue - minValue) + minValue;
        return (float)randomValue;
    }

    public static double NextDouble(double minValue, double maxValue)
    {
        return random.NextDouble() * (maxValue - minValue) + minValue;
    }
}