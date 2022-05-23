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
    private float TotalDayLength;

    void Start()
    {
        //sets the initial sun rotation to its current rotation
        eulerRotation = Sun.transform.rotation.eulerAngles;
    }

    private void FixedUpdate()
    {
        //calls time refresh once per frame
        CurrentHour = System.DateTime.UtcNow.Hour;
        CurrentMinute = System.DateTime.UtcNow.Minute;

        SunTime(CurrentHour, CurrentMinute);
    }

    void SunTime(int Hour, float Minuet)
    {
        //depending on when the sunrise and sunset is, will determin how long the day is
        TotalDayLength = (12 - SunRiseTime) + SunSetTime;

        if (Hour == 12)
        {
            TimeIntoDay = (Hour - SunRiseTime) + Minuet / 60;
        }
        else
        {
            TimeIntoDay = (12 - SunRiseTime) + Hour + Minuet / 60;
        }

        //sets sun rotation based on 185 degrees of rotation (I find works nicely for unity's Directional light) and the percentage of time into the Solarday
        Sun.transform.rotation = Quaternion.Euler(TimeIntoDay * 185 / TotalDayLength, eulerRotation.y, eulerRotation.z);
    }
}
