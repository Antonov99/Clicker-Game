using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float timeStart;
    void Start()
    {
        timeStart = 0;
    }

    void Update()
    {
        if (timeStart < 2)
        {
            timeStart += Time.deltaTime;
        }
        else
        {
            timeStart = 0;
            GetComponent<Click>().Days();
        }
    }
}