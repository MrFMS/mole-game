using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    // Start is called before the first frame update
	public float speedReturn = 10f;
	public GameObject particle;
	void Start()
    {
        
    }

	IEnumerator Hit(Vector3 target)
	{
	
		transform.position = new Vector3(target.x, 0, target.z);

	
		yield return new WaitForSeconds(0.1f);

		for (int i = 0; i < 6; i++)
		{
			transform.Translate(0, 0, speedReturn * Time.deltaTime);
			yield return null;
		}
	}


	public void MakeHammer(int id)
    {

		Debug.Log("here");
		GameObject hole = GameObject.FindGameObjectWithTag(id.ToString());

		StartCoroutine(Hit(hole.transform.position));
		//transform.position = new Vector3(hole.transform.position.x, 0, hole.transform.position.z);

		/*bool isHit = mole.GetComponent<Mole>().Hit();

		// if hit the mole, show hummer and effect
		if (isHit)
		{
			

		}*/

	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mole"))
        {
			Instantiate(this.particle, transform.position, Quaternion.identity);
			Camera.main.GetComponent<CameraController>().ShakeCamera();
			ScoreManager.score += 10;
			Debug.Log("here");
			other.gameObject.GetComponent<Mole>().Hit();
        }
    }

    void Update()
	{
		
	}
}
