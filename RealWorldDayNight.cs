using UnityEngine;

public class RealWorldDayNight : MonoBehaviour
{

    private int CurrentHour;
    private float CurrentMinute;

    public GameObject Sun;
    private Vector3 eulerRotation;
    public float SunRiseTime = 8;
    public float SunSetTime = 8;
    private float TimeIntoDay;
    private float SolarDayLength;

    void Start()
    {
        //sets the initial sun rotation to its current rotation
        eulerRotation = Sun.transform.rotation.eulerAngles;
    }

    private void FixedUpdate()
    {
        //calls time refresh once per frame
        CurrentHour = System.DateTime.Now.Hour;
        CurrentMinute = System.DateTime.Now.Minute;

        SunTime(CurrentHour, CurrentMinute);
    }

    void SunTime(int Hour, float Minuet)
    {
        //depending on when the sunrise and sunset is, will determin how long the day is
        SolarDayLength = (12 - SunRiseTime) + SunSetTime;

        TimeIntoDay = Hour + Minuet / 60;

        var SolarEnd = SunRiseTime + SolarDayLength;

        if (TimeIntoDay < SunRiseTime)
        {
            Sun.transform.rotation = Quaternion.Euler(185, eulerRotation.y, eulerRotation.z);
        }
        else if (TimeIntoDay > SolarEnd)
        {
            Sun.transform.rotation = Quaternion.Euler(185, eulerRotation.y, eulerRotation.z);
        }
        else
        {
            //if time is in solar window
            var cTime = TimeIntoDay - SunRiseTime;

            //sets sun rotation based on 185 degrees of rotation (I find works nicely for unity's Directional light) and the percentage of time into the Solarday
            Sun.transform.rotation = Quaternion.Euler(cTime * 185 / SolarDayLength, eulerRotation.y, eulerRotation.z);
        }
    }
}
