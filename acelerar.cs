using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class acelerar : MonoBehaviour {

  private IEnumerator Start() {
    foreach (InputDevice device in InputSystem.devices) {
      if (device.name == "Accelerometer") {
        _accelerometer = (Accelerometer)device;
        InputSystem.EnableDevice(_accelerometer);
        break;
      }
    }

    Input.location.Start();
    int maxWait = 20;
    while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
      yield return new WaitForSeconds(1);
      maxWait--;
    }
    const float range = 0.01f;
    _latitud_minima= Input.location.lastData.latitude - range;
    _latitud_maxima = Input.location.lastData.latitude + range;
    _longitud_minima = Input.location.lastData.longitude - range;
    _longitud_maxima = Input.location.lastData.longitude + range;
  }

  private void Update() {
    if (
      Input.location.isEnabledByUser &&
      Input.location.status != LocationServiceStatus.Initializing &&
       _fueraDeRango()) {
      return;
    }
    Vector3 aceleracion = _accelerometer.acceleration.ReadValue();
    Vector3 rotacion = new(aceleracion.x, 0, aceleracion.y);
    transform.Translate(multiplicador * Time.deltaTime * rotacion, Space.World);
  }

  private bool _fueraDeRango() {
    float latitud_actual = Input.location.lastData.latitude;
    float longitud_actual = Input.location.lastData.longitude;
    return latitud_actual < _latitud_minima ||
           latitud_actual > _latitud_maxima ||
           longitud_actual < _longitud_minima ||
           longitud_actual > _longitud_maxima;
  }



  private Accelerometer _accelerometer;
  private float _latitud_minima;
  private float _latitud_maxima;
  private float _longitud_minima;
  private float _longitud_maxima;
  private const float multiplicador = 1f;

}