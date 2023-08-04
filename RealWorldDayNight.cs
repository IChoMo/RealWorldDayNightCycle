using UnityEngine;

public class RealWorldDayNight : MonoBehaviour
{
    public GameObject Sun;
    public float SunRiseTime = 8;
    public float SunSetTime = 20;

    private int CurrentHour;
    private float CurrentMinute;
    private Vector3 eulerRotation;
    private float TimeIntoDay;
    private float SolarDayLength;
    private float SolarNightLength;

    void Start()
    {
        //sets the initial sun rotation to its current rotation
        eulerRotation = Sun.transform.rotation.eulerAngles;
    }

    private void FixedUpdate()
    {
        if (SunSetTime > 12) SunSetTime += 12;

        //calls time refresh once per frame
        CurrentHour = System.DateTime.Now.Hour;
        CurrentMinute = System.DateTime.Now.Minute;

        SunTime(CurrentHour, CurrentMinute);
    }

    void SunTime(int Hour, float Minuet)
    {
        //depending on when the sunrise and sunset is, will determin how long the day is
        SolarDayLength = SunSetTime - SunRiseTime;
        SolarNightLength = 24 - SolarDayLength;

        TimeIntoDay = Hour + Minuet / 60;

        if (TimeIntoDay < SunRiseTime) //if the time is beofre sun rise
        {
            Sun.transform.rotation = Quaternion.Euler(TimeIntoDay * 90 / SolarNightLength + 270, eulerRotation.y, eulerRotation.z);
        }
        else if(TimeIntoDay > SunSetTime) //if the time is after sun set
        {
            //Get the amount of hours that you currently are into the Solar Night by removing the amount of hours before the sun set
            var SolarNightTime = TimeIntoDay - SunSetTime;

            Sun.transform.rotation = Quaternion.Euler(SolarNightTime * 90 / SolarNightLength + 180, eulerRotation.y, eulerRotation.z);
        }
        else //if time is in solar window
        {
            //Get the amount of hours that you currently are into the Solar Day by removing the amount of hours before the sun rose
            var SolarDayTime = TimeIntoDay - SunRiseTime;

            //sets sun rotation based on 180 degrees of rotation and the percentage of time into the Solarday
            Sun.transform.rotation = Quaternion.Euler(SolarDayTime * 180 / SolarDayLength, eulerRotation.y, eulerRotation.z);
        }
    }
}
