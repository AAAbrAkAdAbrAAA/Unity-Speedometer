using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GpsLocation : MonoBehaviour
{
    public Text GPSStatus;
    public Text latitude;
    public Text longitude;
    public Text altitude;
    public Text horizontalAccurancy;
    public Text TimeStamp;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPSLoc());

    }

    IEnumerator GPSLoc()
    {
        //Check if location service is enabled
        if (!Input.location.isEnabledByUser)
        {
            GPSStatus.text = "GPS not enabled";
            yield break;
        }
        //Start location Service
        Input.location.Start();

        //Wait until service initialize
        int maxWait = 20;
        while (Input.location.status ==LocationServiceStatus.Initializing && maxWait>0)
        {
            GPSStatus.text = "Try to find GPS";
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        //Service did not initialize in 20 sec
        if (maxWait < 1)
        {
            GPSStatus.text = "Time out";
            yield break;
        }

        //connection failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus.text = "Unable to determine GPS location";
            yield break;
        }
        else
        {
            //Acess granted
            GPSStatus.text = "Running";
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }
    }// End of GPSLoc

    private void UpdateGPSData()
    {
        if(Input.location.status == LocationServiceStatus.Running)
        {
            //Acess is ganted and has been initializes
            GPSStatus.text = "Running";
            latitude.text = Input.location.lastData.latitude.ToString();
            longitude.text = Input.location.lastData.longitude.ToString();
            altitude.text = Input.location.lastData.altitude.ToString();
            horizontalAccurancy.text = Input.location.lastData.horizontalAccuracy.ToString();
            TimeStamp.text = Input.location.lastData.timestamp.ToString();
        }
        else
        {
            //service is stopped
            GPSStatus.text = "GPS stopped";
        }
    } //End of Update 
    
}
