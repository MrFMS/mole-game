using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolesManager : MonoBehaviour
{
	List<Mole> moles = new List<Mole>();
	bool generate;
	public AnimationCurve maxMoles;

	void Start()
	{
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Mole");
		foreach (GameObject go in gos)
		{
			moles.Add(go.GetComponent<Mole>());
		}

		this.generate = false;
	}

	public void StartGenerate()
	{
		StartCoroutine("Generate");
	}

	public void StopGenerate()
	{
		this.generate = false;
	}

	IEnumerator Generate()
	{
		this.generate = true;


		// mettre conditon de fin
		while (true)
		{
			yield return new WaitForSeconds(1.0f);

			int n = moles.Count;
			int maxNum = (int)this.maxMoles.Evaluate(Time.deltaTime);
			for (int i = 0; i < maxNum; i++)
			{
				this.moles[Random.Range(0, n)].Up();

				yield return new WaitForSeconds(0.3f);
			}
		}
	}
}
