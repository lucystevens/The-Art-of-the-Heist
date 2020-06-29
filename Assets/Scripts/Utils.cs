using System;

public class Utils {
    public static bool AreFloatsEqual(float fl1, float fl2, float granularity = 0.1f) {
        return Math.Abs(fl1 - fl2) < granularity;
    }
}