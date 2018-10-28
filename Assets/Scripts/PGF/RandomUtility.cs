using System;

public static class RandomUtility {

    static Random random;

    public static float RandFloat(float min, float max)
    {
        if(random == null) { random = new Random(); }

        return (float)((random.NextDouble() + 1.0) / 2.0) * (max - min) + min;
    }
}
