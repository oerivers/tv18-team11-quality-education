using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text tiempo;
    private int seconds,minutes;

	// Use this for initialization
	void Start () {
        minutes = 0;
        seconds = 0;
        tiempo.text = "0:00";
        StartCoroutine(Time());
	}

    IEnumerator Time()
    {
        while (true) {
            seconds++;
            seconds %= 60;
            if (seconds == 0)
            {
                minutes++;
            }
            
            tiempo.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(1);
        }
    }
    public void StopTime()
    {
        StopCoroutine(Time());
    }
}
