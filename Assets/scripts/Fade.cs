using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Fade : MonoBehaviour {

    public Image img;
    public float fadeInDelay = 2;
    public float fadeInSpeed=1;
    // Use this for initialization
    void Start () {
        Image img = GetComponent<Image>();
        img.color = Color.black;
        
	}
	
	// Update is called once per frame
	void Update () {
        Image img = GetComponent<Image>();
        Color temp = img.color;
        if (Time.timeSinceLevelLoad > fadeInDelay)
        {
            if (temp.a >= 0)
            {
                temp.a -= fadeInSpeed * Time.deltaTime;
                
            }
            else
            {
                Destroy(gameObject); //get rid of panel as it blocks interactions with buttons
            }
            img.color = temp;
           
        }
	}
}
