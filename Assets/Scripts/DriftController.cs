using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftController : MonoBehaviour
{
    [SerializeField]
    public float maxSpeed = 20f;              // Maksymalna pr�dko�� pojazdu

    [SerializeField]
    public float baseSpeed = 5f;              // Minimalna pr�dko��

    [SerializeField]
    public float maxTurningRadius = 45f;      // Maksymalny promie� skr�tu

    [SerializeField]
    public float sensitivity = 0.5f;          // Czu�o�� skr�tu na r�nic� pozycji kciuk�w

    [SerializeField]
    public float accelerationRate = 2f;       // Szybko�� przyspieszania

    private float speed = 0f;
    private float turningRadius = 0f;
    private float driftAngle = 0f;

    // Aktualne pozycje kciuk�w
    private float leftThumbPos = 0f;
    private float rightThumbPos = 0f;

    // Pozycje kciuk�w w poprzedniej klatce
    private float prevLeftThumbPos = 0f;
    private float prevRightThumbPos = 0f;

    // Identyfikatory palc�w dla rozpoznawania dotyku
    private int leftFingerId = -1;
    private int rightFingerId = -1;

    private Rigidbody rb;  // Odwo�anie do Rigidbody

    // Start is called before the first frame update
    void Start()
    {
        // Pobranie komponentu Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Aktualizacja pozycji kciuk�w
        UpdateThumbPositions();

        // Sprawdzanie, czy oba kciuki przesun�y si� od g�ry w d�
        bool isAccelerating = IsBothThumbsMovingDown();

        if (isAccelerating)
        {
            // Zwi�kszanie pr�dko�ci tylko wtedy, gdy oba kciuki przesuwaj� si� w d�
            speed = Mathf.Min(speed + accelerationRate * Time.deltaTime, maxSpeed);
        }
        else
        {
            // Je�li kciuki nie przesuwaj� si� w d�, utrzymywanie minimalnej pr�dko�ci
            speed = baseSpeed;
        }

        // Obliczanie r�nicy w pozycjach kciuk�w (dla skr�tu)
        float delta = leftThumbPos - rightThumbPos;

        // Ustalanie kierunku skr�tu i promienia na podstawie delta
        if (Mathf.Abs(delta) > sensitivity)
        {
            turningRadius = maxTurningRadius * Mathf.Abs(delta);
            if (delta > 0)
            {
                // Skr�t w prawo
                driftAngle = Mathf.Lerp(driftAngle, turningRadius, Time.deltaTime * 10f);
            }
            else
            {
                // Skr�t w lewo
                driftAngle = Mathf.Lerp(driftAngle, -turningRadius, Time.deltaTime * 10f);
            }
        }
        else
        {
            // Brak skr�tu przy niewielkiej r�nicy
            driftAngle = Mathf.Lerp(driftAngle, 0f, Time.deltaTime * 10f);
        }

        // Aplikowanie momentu obrotowego do skr�tu pojazdu
        rb.AddTorque(Vector3.up * driftAngle, ForceMode.Force);

        // Aplikowanie si�y do przodu (w kierunku obrotu)
        Vector3 forwardForce = transform.forward * speed;
        rb.AddForce(forwardForce, ForceMode.Force);

        // Zapisywanie pozycji kciuk�w na poprzedni� klatk�
        prevLeftThumbPos = leftThumbPos;
        prevRightThumbPos = rightThumbPos;
    }

    private bool IsBothThumbsMovingDown()
    {
        // Sprawdzanie, czy oba kciuki przesuwaj� si� od g�ry w d�
        bool leftMovingDown = leftThumbPos < prevLeftThumbPos;
        bool rightMovingDown = rightThumbPos < prevRightThumbPos;

        return leftMovingDown && rightMovingDown;
    }

    private void UpdateThumbPositions()
    {
        // Przechodzimy przez wszystkie punkty dotyku na ekranie
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // Je�li dotkni�cie jest po lewej stronie ekranu
            if (touch.position.x < Screen.width / 2)
            {
                if (leftFingerId == -1 || leftFingerId == touch.fingerId)
                {
                    leftFingerId = touch.fingerId;
                    leftThumbPos = Mathf.Clamp01(touch.position.y / Screen.height);
                }
            }
            // Je�li dotkni�cie jest po prawej stronie ekranu
            else
            {
                if (rightFingerId == -1 || rightFingerId == touch.fingerId)
                {
                    rightFingerId = touch.fingerId;
                    rightThumbPos = Mathf.Clamp01(touch.position.y / Screen.height);
                }
            }

            // Sprawdzenie, czy dany dotyk si� zako�czy�, i resetowanie ID
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (touch.fingerId == leftFingerId)
                {
                    leftFingerId = -1;
                    leftThumbPos = 0.5f; // Resetowanie do �rodkowej pozycji
                }
                if (touch.fingerId == rightFingerId)
                {
                    rightFingerId = -1;
                    rightThumbPos = 0.5f; // Resetowanie do �rodkowej pozycji
                }
            }
        }
    }
}
