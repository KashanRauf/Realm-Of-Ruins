using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPattern : MonoBehaviour
{
    // Static, heavily parameterized functions will be used to process bullet paths
    // These functions will calculate the updated position as if they are shot in a default direction
    // The BulletManager will take the Vector2 and transform to match
    /* Terminology
     *   Spokes : Sets of bullets fired in the same path (e.g. shoot 10 bullets forward -> 1 spoke, shoot 10 bullets front and back -> 2 spokes)
     *     Path : The path bullets travel along throughout their lifetime
     * Lifetime : The duration/timeframe that a bullet exists for
     * Function : Mathematical functions that determine bullet path, functions of time (e.g. a straight line is defined by position = slope * time + origin)
     *   Length : The number of bullets in a spoke
     *     Head : The first bullet in a spoke
     *  Spacing : Angle between spokes
     *    delay : spawnDelay is the time between bullet spawns in a spoke
     *    Speed : Multiplier of time in path functions
     *    Delta : Change in something (e.g. angleDelta -> spoke angles change)
     *  Pattern : The culmination of these parts
     */

    public static Vector2 ProcessPattern(int patternID, float time)
    {
        switch (patternID)
        {
            case 0:
                return FirstPattern(time);
            case 1:
                return SecondPattern(time);
            case 2:
                return ThirdPattern(time);
            default:
                Debug.Log("Invalid pattern");
                return Vector2.zero;
        }
    }

    // Pattern index 0: Straight line spokes (kind of fan shaped with multiple?)
    private static Vector2 FirstPattern(float time)
    {
        return Vector2.right * time * 1.5f;
    }

    // Pattern index 1: Wavy spokes
    private static Vector2 SecondPattern(float time)
    {
        return new Vector2(2 * time, 0.5f * Mathf.Sin(1.5f * time));
    }

    // Pattern index 2: Push: Lots of interesting forms depending on the speed, angle, and lifetime
    private static Vector2 ThirdPattern(float time)
    {
        return new Vector2(Mathf.Cos(time)*time, Mathf.Sin(time));
    }

    // Pattern index 3: Expanding ring (nvm just use first with hella spokes)
    private static Vector3 FourthPattern(float time)
    {
        return new Vector2(Mathf.Tan(time), Mathf.Tan(time));
    }
}


/* References
 * https://www.youtube.com/watch?v=sj8Sg8qnjOg
 * https://macsphere.mcmaster.ca/bitstream/11375/16048/1/thesis.pdf
 * 
 */