using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugManager : MonoBehaviour {

    public Vector3 gyro_rotation;

    public Text DebugTextField;

	void Start () {
        Input.gyro.enabled = true;
	}
    void Update()
    {
        gyro_rotation = Input.gyro.rotationRateUnbiased;
        DebugTextField.text = "Rot. " + gyro_rotation;
    }
}
