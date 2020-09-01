using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform gunPoint;
    public bool isShootingIntoPlayer;
    public bool isAutoShoot = false;
    public int bulletSpeed = 10;
    public float shootingTimeReload = 1f;
    public int maximumBulletCount = 20;
    private Transform targetShoot;
    private float distance;
    private Vector3 position;
    public GameObject[] instances;
    // public List<GameObject> instances = new List<GameObject>();
    public static Vector3 stackPosition = new Vector3(-9999, -9999, -9999);
    private bool mouseTime = true;
    private bool isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject?.GetComponent<ScPlayer>()) isPlayer = true;

        if (isShootingIntoPlayer)
        {
            targetShoot = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        }
        else
        {
            distance = -Camera.main.transform.localPosition.z;
        }
        position = new Vector3(0, 0, 0);

        // if (isAutoShoot)
        // {
        //     StartCoroutine(FireBulletAutoShoot(position, new WaitForSeconds(1f)));
        // }

        //Object Pooling
        instances = new GameObject[maximumBulletCount];
        for (int i = 0; i < maximumBulletCount; i++)
        {
            instances[i] = Instantiate(bullet, stackPosition, Quaternion.identity);
            instances[i].SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        // || Time.timeScale == 0
        if ((Input.GetMouseButton(0) && isPlayer) || (isAutoShoot))
        {
            if (mouseTime)
                StartCoroutine(FireBullet(new WaitForSeconds(shootingTimeReload)));
        }

    }

    IEnumerator FireBullet(WaitForSeconds time)
    {
        mouseTime = false;
        if (isShootingIntoPlayer) position = targetShoot.position;
        else
        {
            position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            position = Camera.main.ScreenToWorldPoint(position);
        }

        //Debug.Log("Shoot");
        GameObject instance = GetNextAvailiableInstance() as GameObject;
        if (instance != null)
        {
            instance.transform.position = gunPoint.position;
            instance.SetActive(true);
            gunPoint.LookAt(position);
            instance.GetComponent<Rigidbody>().velocity = gunPoint.forward * bulletSpeed;
        }

        // Rigidbody bulletInstance = Instantiate(bullet, gunPoint.position, Quaternion.identity) as Rigidbody;
        // gunPoint.LookAt(position);
        // bulletInstance.velocity = gunPoint.forward * bulletSpeed;

        yield return time;
        mouseTime = true;

    }
    GameObject GetNextAvailiableInstance()
    {
        for (int i = 0; i < maximumBulletCount; i++)
        {
            if (!instances[i].activeSelf) return instances[i];
        }
        return null;
    }
    // IEnumerator FireBulletAutoShoot(Vector3 position, WaitForSeconds time)
    // {
    //     for (; ; )
    //     {
    //         if (isShootingIntoPlayer) position = targetShoot.position;
    //         else
    //         {
    //             position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
    //             position = Camera.main.ScreenToWorldPoint(position);
    //         }
    //         Rigidbody bulletInstance = Instantiate(bullet, gunPoint.position, Quaternion.identity) as Rigidbody;
    //         gunPoint.LookAt(position);
    //         bulletInstance.velocity = gunPoint.forward * bulletSpeed;
    //         yield return time;
    //     }
    // }




}
