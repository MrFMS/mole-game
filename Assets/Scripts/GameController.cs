using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    MolesManager moleManager;
    float timer;
    public static float time;

    // Start is called before the first frame update
    void Start()
    {
        this.moleManager = gameObject.GetComponent<MolesManager>();
        this.moleManager.StartGenerate();
    }

    // Update is called once per frame
    void Update()
    {
        this.timer += Time.deltaTime;
    }
}
