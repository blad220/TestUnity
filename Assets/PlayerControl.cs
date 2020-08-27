using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float transX = 0.0f;
    public float transY = 0.0f;

    public List<KeyCode> upButton;
    public List<KeyCode> downButton;
    public List<KeyCode> leftButton;
    public List<KeyCode> rightButton;

    // Изменение скорости перемещения героя
    public float playerSpeed = 2.0f;

    // Текущая скорость перемещения
    private float currentSpeed = 0.0f;
    private Vector3 lastMovement = new Vector3();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // transX = this.transform.Translate.
        Movement();
    }

    void Movement()
    {
        // Необходимое движение
        Vector3 movement = new Vector3();
        // Проверка нажатых клавиш
        movement += MoveIfPressed(upButton, Vector3.forward);
        movement += MoveIfPressed(downButton, Vector3.back);
        movement += MoveIfPressed(leftButton, Vector3.left);
        movement += MoveIfPressed(rightButton, Vector3.right);
        // Если нажато несколько кнопок, обрабатываем это
        movement.Normalize();
        // Проверка нажатия кнопки
        if (movement.magnitude > 0)
        {
            // После нажатия двигаемся в этом направлении
            currentSpeed = playerSpeed;
            this.transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
            lastMovement = movement;
        }
        else
        {
            // Если ничего не нажато
            this.transform.Translate(lastMovement * Time.deltaTime * currentSpeed, Space.World);
            // Замедление со временем
            currentSpeed *= 0.9f;
        }
    }

    // Возвращает движение, если нажата кнопка
    Vector3 MoveIfPressed(List<KeyCode> keyList, Vector3 Movement)
    {
        // Проверяем кнопки из списка
        foreach (KeyCode element in keyList)
        {
            if (Input.GetKey(element))
            {
                // Если нажато, покидаем функцию
                return Movement;
            }
        }
        // Если кнопки не нажаты, то не двигаемся
        return Vector3.zero;
    }
}
