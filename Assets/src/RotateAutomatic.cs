using UnityEngine;
using System.Collections;

public class RotateAutomatic : MonoBehaviour {

    public float _z;
    public float _y;

    void Update()
    {
        float Y = 0;
        if(_y>0)
            Y = transform.localEulerAngles.y + _y;

        float Z = 0;
        if (_z > 0)
            Z = transform.localEulerAngles.z + _z;

        transform.localEulerAngles = new Vector3(0, Y, Z);
    }
}
