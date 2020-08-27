using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Vector3 transS;
    Vector3 transP;
    float transX1;
    float transX2;
    float transY1;
    float transY2;
    float width = 0.0f;
    float height = 0.0f;


    public List<KeyCode> upButton;
    public List<KeyCode> downButton;
    public List<KeyCode> leftButton;
    public List<KeyCode> rightButton;

    public GameObject restrictions;
    public GameObject prefabEnemy;
    public int colEnemy = 4;
    public float rangeRadiusSpawnEnemy = 5f;

    public float playerSpeed = 5.0f;

    private float currentSpeed = 0.0f;
    private Vector3 lastMovement = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        upButton.Add(KeyCode.W);
        downButton.Add(KeyCode.S);
        leftButton.Add(KeyCode.A);
        rightButton.Add(KeyCode.D);

        transS = restrictions.GetComponent<Renderer>().bounds.size;
        transP = restrictions.GetComponent<Renderer>().bounds.center;

        width = this.GetComponent<Renderer>().bounds.size.x / 2;
        height = this.GetComponent<Renderer>().bounds.size.y / 2;

        transX1 = restrictions.GetComponent<Renderer>().bounds.center.x + (restrictions.GetComponent<Renderer>().bounds.size.x / 2);
        transX2 = restrictions.GetComponent<Renderer>().bounds.center.x - (restrictions.GetComponent<Renderer>().bounds.size.x / 2);
        transY1 = restrictions.GetComponent<Renderer>().bounds.center.y + (restrictions.GetComponent<Renderer>().bounds.size.y / 2);
        transY2 = restrictions.GetComponent<Renderer>().bounds.center.y - (restrictions.GetComponent<Renderer>().bounds.size.y / 2);

    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        EnemyCreate();
    }

    void Movement()
    {

        Vector3 movement = new Vector3();
  
        movement += MoveIfPressed(upButton, Vector3.up);
        movement += MoveIfPressed(downButton, Vector3.down);
        movement += MoveIfPressed(leftButton, Vector3.left);
        movement += MoveIfPressed(rightButton, Vector3.right);

        movement.Normalize();
        if (
                (this.transform.position.x + width <= transX1) && (this.transform.position.x - width >= transX2) &&
                (this.transform.position.y + height <= transY1) && (this.transform.position.y - height >= transY2)
            )
        {
            if (movement.magnitude > 0)
            {

                currentSpeed = playerSpeed;
                this.transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
                lastMovement = movement;

            }
            else
            {
                this.transform.Translate(lastMovement * Time.deltaTime * currentSpeed, Space.World);
                currentSpeed *= 0.9f;
            }
        }
        else
        {
            if (this.transform.position.x + width > transX1) this.transform.position = new Vector3(transX1 - width, this.transform.position.y, this.transform.position.z);
            if (this.transform.position.x - width < transX2) this.transform.position = new Vector3(transX2 + width, this.transform.position.y, this.transform.position.z);
            if (this.transform.position.y + height > transY1) this.transform.position = new Vector3(this.transform.position.x, transY1 - height, this.transform.position.z);
            if (this.transform.position.y - height < transY2) this.transform.position = new Vector3(this.transform.position.x, transY2 + height, this.transform.position.z);

        }
    }

    Vector3 MoveIfPressed(List<KeyCode> keyList, Vector3 Movement)
    {
        foreach (KeyCode element in keyList)
        {
            if (Input.GetKey(element))
            {
                return Movement;
            }
        }
        return Vector3.zero;
    }

    void EnemyCreate()
    {

        if (GameObject.FindGameObjectsWithTag("enemy").Length < colEnemy)
        {
            
            float transRandX1 = -rangeRadiusSpawnEnemy;
            float transRandX2= rangeRadiusSpawnEnemy;;
            float transRandY1= -rangeRadiusSpawnEnemy;;
            float transRandY2= rangeRadiusSpawnEnemy;;
            float ofsetEnemySpot = 3f;
            if (this.transform.position.x + width + rangeRadiusSpawnEnemy > transX1)
            {
                transRandX2 = (transX1 - (this.transform.position.x + width));
                transRandX1 = -10f - transRandX2 - rangeRadiusSpawnEnemy;
            }
            if (this.transform.position.x - width - rangeRadiusSpawnEnemy < transX2)
            {
                transRandX1 = (transX2 - (this.transform.position.x - width));
                transRandX2 = 10f + transRandX1 + rangeRadiusSpawnEnemy;
            }
            if (this.transform.position.y + width + rangeRadiusSpawnEnemy > transY1)
            {
                transRandY2 = (transY1 - (this.transform.position.y + width));
                transRandY1 = -10f - transRandY2 - rangeRadiusSpawnEnemy;
            }
            if (this.transform.position.y - width - rangeRadiusSpawnEnemy < transY2)
            {
                transRandY1 = (transY2 - (this.transform.position.y - width));
                transRandY2 = 10f + transRandY1 + rangeRadiusSpawnEnemy;
            }
            // Debug.Log(transRandX1 + " --transRandX-- " + transRandX2);
            // Debug.Log(transRandY1 + " --transRandY-- " + transRandY2);

            // float randX1 = Random.Range(transRandX1, -ofsetEnemySpot);
            // float randX2 = Random.Range(ofsetEnemySpot, transRandX2);
            // float randY1 = Random.Range(transRandY1, -ofsetEnemySpot);
            // float randY2 = Random.Range(ofsetEnemySpot, transRandY2);

            // float[] masXtemp = {randX1, randX2};
            // float[] masYtemp = {randY1, randY2};

            // float randX = masXtemp[Random.Range (0, masXtemp.Length)];
            // float randY = masYtemp[Random.Range (0, masYtemp.Length)];

            float randX = Random.Range(transRandX1, transRandX2);
            float randY = Random.Range(transRandY1, transRandY2);

            // float randY2 = Random.Range(ofsetEnemySpot, transRandY2);
            Vector3 position = new Vector3(randX, randY, 0);
            Instantiate(prefabEnemy, this.transform.position + position, Quaternion.identity);
        }
    }
}
