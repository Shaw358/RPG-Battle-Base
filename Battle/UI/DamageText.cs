using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    TextMeshPro textMesh;
    RectTransform thisTransform;

    float startPosX;

    [SerializeField] float shakeIntensity;
    [SerializeField] float shakeSpeed;

    float timer;

    [SerializeField] GameObject testTarget;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        thisTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < 1.5f)
        {
            DoShake();
        }
        else if (timer < 3f)
        {
            float newPos = transform.position.y - 0.01f;
            transform.position = new Vector2(transform.position.x, newPos);
        }
        else if(timer > 4f)
        {
            ResetObject();
        }
    }

    public void DoMove(string newText, GameObject target)
    {
        textMesh.text = newText;
        startPosX = target.transform.position.x + 1.3f;
    }

    private void DoShake()
    {
        thisTransform.position = new Vector2(startPosX + Mathf.Sin(Time.time * shakeSpeed) * shakeIntensity, transform.position.y);
    }

    private void ResetObject()
    {
        timer = 0;
        gameObject.SetActive(false);
    }
}