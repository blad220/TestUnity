﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBulletEnemy : MonoBehaviour
{
    public float timeout = 2f;
    private float curTimeout;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        curTimeout += Time.deltaTime;
        if (curTimeout > timeout)
        {
            // Destroy(gameObject);
            gameObject.SetActive(false);
            gameObject.transform.position = ScShooting.stackPosition;
            curTimeout = 0f;
        }
    }

    public float damage = 10f;
    public string targetTag = "Player";
    void OnTriggerEnter(Collider coll)
    {
        // Debug.Log(targetTag);
        // Debug.Log(coll.transform.tag);
        if (targetTag == coll.transform.tag)
        {
            coll.transform.GetComponent<ScPlayer>().Damage(damage);
            // Debug.Log("Enemy Shoot2");
            //Destroy(gameObject);
            gameObject.SetActive(false);
            gameObject.transform.position = ScShooting.stackPosition;
        }


    }
}
