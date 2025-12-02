
using UnityEngine;



public class mirar_norte : MonoBehaviour {
  private void Start() {

    Input.location.Start();
    Input.compass.enabled = true;
  }

  private void Update() {
    if (Input.location.status != LocationServiceStatus.Running) {
      return;
    }
    float rotacion = Input.compass.trueHeading;
    Quaternion targetRotation = Quaternion.Euler(0, -rotacion, 0);
    transform.rotation = Quaternion.Slerp(
        transform.rotation,
        targetRotation,
        suavizado
    );
  }

   private const float suavizado = 0.1f;
}