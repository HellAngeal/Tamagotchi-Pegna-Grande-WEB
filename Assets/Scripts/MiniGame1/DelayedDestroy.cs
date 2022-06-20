using UnityEngine;
using System.Collections;

public class DelayedDestroy : MonoBehaviour {

    public float delay;

	// Use this for initialization
    IEnumerator Start () 
    {
        yield return new WaitForSeconds(delay);	
        Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
