using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPPlayerController : MonoBehaviour {
  [SerializeField]
  float linearSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
  public void MovePlayer(Vector3 dir){
    transform.position += dir.normalized * linearSpeed * Time.deltaTime;
    transform.Find("Capsule").LookAt(transform.position + dir);
  }
}
