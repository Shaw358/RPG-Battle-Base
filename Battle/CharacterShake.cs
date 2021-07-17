using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShake : MonoBehaviour
{
    [SerializeField] private float _shake_amount = 0.1f;

    Vector3 _original_pos;

    private bool _shaking;

    void Awake()
    {
        _shaking = false;
        _original_pos = transform.localPosition;
    }

    void Update()
    {
        if (_shaking)
        {
            transform.localPosition = _original_pos + Random.insideUnitSphere * _shake_amount;
        }
    }

    public void Shake()
    {
        StartCoroutine(Shaking());
    }

    private IEnumerator Shaking()
    {
        _shaking = true;
        yield return new WaitForSeconds(0.4f);
        _shaking = false;

        transform.localPosition = _original_pos;
    }
}
