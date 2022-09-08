using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_sc : MonoBehaviour
{
    [SerializeField]
    bool _IsRight = true;
    [SerializeField]
    float _SpeedAngle = 1f;
    [SerializeField]
    float _Angle = 0f;
    private void Update()
    {
        if (_Angle <= -90f || _Angle >= 90f)
        {
            _IsRight = !_IsRight;
        }
        _Angle += Time.deltaTime * (_IsRight ? _SpeedAngle : -_SpeedAngle);
        _Angle = Mathf.Clamp(_Angle, -90f, 90f);
        transform.rotation = Quaternion.Euler(0, 0, _Angle);
    }
}
