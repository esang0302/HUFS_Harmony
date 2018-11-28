using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeProgressBar : MonoBehaviour
{
    float time;
    float spendTime;
    Image timebar;
    bool imageOn;
    public Button button;
    // Use this for initialization
    void Start()
    {
        time = 10;
        //spendTime = 3.0f; //3sec
        timebar = this.GetComponent<Image>();
        spendTime = time;
        timebar.enabled = false;
        imageOn = false;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == ("cursor"))
        {
            imageOn = true;
            timebar.enabled = true;
            //Debug.Log(spendTime);
            spendTime -= (Time.deltaTime) * 3;
        }
    }
    void OnCollisionExit(Collision other)
    {
        spendTime = time;
        timebar.enabled = false;
        Debug.Log("Exit!!!!!!!!!!!!!!!");
    }

    // Update is called once per frame
    void Update()
    {
        if(spendTime > 0)
        {
            timebar.fillAmount = spendTime / time;
        }
        else
        {
            timebar.enabled = false;
            imageOn = true;
            button.onClick.Invoke();
            Debug.Log("Click!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            spendTime = time;
        }
    }
}


