using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftController : MonoBehaviour
{
    [SerializeField]
    public float maxSpeed = 20f;              // Maksymalna prêdkoœæ pojazdu

    [SerializeField]
    public float baseSpeed = 5f;              // Minimalna prêdkoœæ

    [SerializeField]
    public float maxTurningRadius = 45f;      // Maksymalny promieñ skrêtu

    [SerializeField]
    public float sensitivity = 0.5f;          // Czu³oœæ skrêtu na ró¿nicê pozycji kciuków

    [SerializeField]
    public float accelerationRate = 2f;       // Szybkoœæ przyspieszania

    private float speed = 0f;
    private float turningRadius = 0f;
    private float driftAngle = 0f;

    // Aktualne pozycje kciuków
    private float leftThumbPos = 0f;
    private float rightThumbPos = 0f;

    // Pozycje kciuków w poprzedniej klatce
    private float prevLeftThumbPos = 0f;
    private float prevRightThumbPos = 0f;

    // Identyfikatory palców dla rozpoznawania dotyku
    private int leftFingerId = -1;
    private int rightFingerId = -1;

    private Rigidbody rb;  // Odwo³anie do Rigidbody

    // Start is called before the first frame update
    void Start()
    {
        // Pobranie komponentu Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Aktualizacja pozycji kciuków
        UpdateThumbPositions();

        // Sprawdzanie, czy oba kciuki przesunê³y siê od góry w dó³
        bool isAccelerating = IsBothThumbsMovingDown();

        if (isAccelerating)
        {
            // Zwiêkszanie prêdkoœci tylko wtedy, gdy oba kciuki przesuwaj¹ siê w dó³
            speed = Mathf.Min(speed + accelerationRate * Time.deltaTime, maxSpeed);
        }
        else
        {
            // Jeœli kciuki nie przesuwaj¹ siê w dó³, utrzymywanie minimalnej prêdkoœci
            speed = baseSpeed;
        }

        // Obliczanie ró¿nicy w pozycjach kciuków (dla skrêtu)
        float delta = leftThumbPos - rightThumbPos;

        // Ustalanie kierunku skrêtu i promienia na podstawie delta
        if (Mathf.Abs(delta) > sensitivity)
        {
            turningRadius = maxTurningRadius * Mathf.Abs(delta);
            if (delta > 0)
            {
                // Skrêt w prawo
                driftAngle = Mathf.Lerp(driftAngle, turningRadius, Time.deltaTime * 10f);
            }
            else
            {
                // Skrêt w lewo
                driftAngle = Mathf.Lerp(driftAngle, -turningRadius, Time.deltaTime * 10f);
            }
        }
        else
        {
            // Brak skrêtu przy niewielkiej ró¿nicy
            driftAngle = Mathf.Lerp(driftAngle, 0f, Time.deltaTime * 10f);
        }

        // Aplikowanie momentu obrotowego do skrêtu pojazdu
        rb.AddTorque(Vector3.up * driftAngle, ForceMode.Force);

        // Aplikowanie si³y do przodu (w kierunku obrotu)
        Vector3 forwardForce = transform.forward * speed;
        rb.AddForce(forwardForce, ForceMode.Force);

        // Zapisywanie pozycji kciuków na poprzedni¹ klatkê
        prevLeftThumbPos = leftThumbPos;
        prevRightThumbPos = rightThumbPos;
    }

    private bool IsBothThumbsMovingDown()
    {
        // Sprawdzanie, czy oba kciuki przesuwaj¹ siê od góry w dó³
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

            // Jeœli dotkniêcie jest po lewej stronie ekranu
            if (touch.position.x < Screen.width / 2)
            {
                if (leftFingerId == -1 || leftFingerId == touch.fingerId)
                {
                    leftFingerId = touch.fingerId;
                    leftThumbPos = Mathf.Clamp01(touch.position.y / Screen.height);
                }
            }
            // Jeœli dotkniêcie jest po prawej stronie ekranu
            else
            {
                if (rightFingerId == -1 || rightFingerId == touch.fingerId)
                {
                    rightFingerId = touch.fingerId;
                    rightThumbPos = Mathf.Clamp01(touch.position.y / Screen.height);
                }
            }

            // Sprawdzenie, czy dany dotyk siê zakoñczy³, i resetowanie ID
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (touch.fingerId == leftFingerId)
                {
                    leftFingerId = -1;
                    leftThumbPos = 0.5f; // Resetowanie do œrodkowej pozycji
                }
                if (touch.fingerId == rightFingerId)
                {
                    rightFingerId = -1;
                    rightThumbPos = 0.5f; // Resetowanie do œrodkowej pozycji
                }
            }
        }
    }
}
