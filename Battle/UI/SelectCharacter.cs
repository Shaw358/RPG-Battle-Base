using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    [SerializeField] Camera cam;

    public void SetNewTarget(GameObject newTarget)
    {
        transform.position = cam.WorldToScreenPoint(newTarget.transform.position);    
    }
}
