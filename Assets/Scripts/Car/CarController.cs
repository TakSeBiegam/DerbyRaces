using UnityEngine;

public class CarController : MonoBehaviour
{
    public float acceleration = 10f; // Przyśpieszenie
    public float maxSpeed = 20f; // Prędkość maksymalna
    public float turnSpeed = 5f; // Prędkość skręcania
    public float slideFactor = 0.5f; // Współczynnik poślizgu auta

    private float currentSpeed; // Aktualna prędkość auta
    private float currentTurnSpeed; // Aktualna prędkość skręcania auta

    private void Update()
    {
        float verticalInput = Input.GetAxis("Vertical"); // Pobranie wartości osi pionowej (w przód/tył)
        float horizontalInput = Input.GetAxis("Horizontal"); // Pobranie wartości osi poziomej (w lewo/prawo)

        // Przyśpieszanie/hamowanie
        currentSpeed += verticalInput * acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        // Skręcanie
        currentTurnSpeed = turnSpeed / Mathf.Lerp(1f, maxSpeed, Mathf.Abs(currentSpeed / maxSpeed));
        if (currentSpeed > 1f)
        {
            float rotation = horizontalInput * currentTurnSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotation);
        }

        // Obrót auta wokół osi pionowej


        // Poruszanie autem z uwzględnieniem jego obrotu
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        // Poślizg auta - zmniejszenie prędkości w przypadku kolizji (poślizg)
        if (collision.collider.CompareTag("Ground"))
        {
            currentSpeed *= slideFactor;
        }
    }
}
