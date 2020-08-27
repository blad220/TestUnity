using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScPlayer : MonoBehaviour
{
    public float health = 100f;
    float maxhealth;
    float  maxScaleX = 1f;
    float  maxScaleY = 1f;
    float  minScaleX = 0.6f;
    float  minScaleY = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        maxhealth = health;
        maxScaleX = this.transform.localScale.x;
        maxScaleY = this.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Damage(float numb) {
        health -= numb;

        if (health < 0f) {
            
            Time.timeScale=0;
        }
        // EditorApplication.isPaused = true;
        // SceneManager.LoadScene("SampleScene");        

        float dmgScaleX = minScaleX + ( (health/maxhealth) * ((maxScaleX - minScaleX)) ); //1..0.37
        float dmgScaleY = minScaleY + ( (health/maxhealth) * ((maxScaleY - minScaleY)) ); //1..0.37
	    // Debug.Log(dmgScale);

        this.transform.localScale =  new Vector3(maxScaleX * dmgScaleX, maxScaleY * dmgScaleY, this.transform.localScale.z);
        
    }
}
