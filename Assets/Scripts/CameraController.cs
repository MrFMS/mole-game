using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public float MAGNITUDE = 0.4f;
    public AnimationCurve curve;
    public float duration = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Shaking()
    {
        Vector3 defaultPos = transform.position;
        for (int i = 0; i <= 360; i += 60)
        {
            transform.position =
                new Vector3(defaultPos.x, defaultPos.y + MAGNITUDE * Mathf.Sin(i * Mathf.Deg2Rad), defaultPos.z);

            yield return null;
        }
    }

    public void ShakeCamera()
    {
        StartCoroutine(Shaking());
    }
}
