using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScShooting : MonoBehaviour
{
    public Rigidbody bullet;
    public Transform gunPoint;
    public bool isShootingIntoPlayer;
    public bool isAutoShoot = false;
    public int bulletSpeed = 10;
    public float timeout = 0.5f;
    private float curTimeout;
    // private Transform targetShoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) || isAutoShoot)
        {
            curTimeout += Time.deltaTime;
            if (curTimeout > timeout)
            {
                curTimeout = 0;
                Rigidbody bulletInstance = Instantiate(bullet, gunPoint.position, Quaternion.identity) as Rigidbody;
                Vector3 position = new Vector3(0,0,0);

                if (isShootingIntoPlayer) { 
                    Transform targetShoot = GameObject.FindGameObjectsWithTag("Player")[0].transform;
                    position = targetShoot.position;
                    // position = Camera.main.ScreenToWorldPoint(position);
                }
                else
                {
                    float distance = -Camera.main.transform.localPosition.z;
                    position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                    position = Camera.main.ScreenToWorldPoint(position);
                }
                gunPoint.LookAt(position);
                bulletInstance.velocity = gunPoint.forward * bulletSpeed;

                // Debug.Log(worldPosition);
            }

        }
        else
        {
            curTimeout = timeout + 1;
        }

    }
}
