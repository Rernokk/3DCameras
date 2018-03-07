using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISOFadeManager : MonoBehaviour
{
  GameObject[] Walls;
  GameObject camera;
  GameObject player;

  [SerializeField]
  float scalar = 30;
  void Start()
  {
    Walls = GameObject.FindGameObjectsWithTag("Wall");
    camera = GameObject.FindGameObjectWithTag("MainCamera");
    player = GameObject.FindGameObjectWithTag("Player");
  }

  // Update is called once per frame
  void Update()
  {
    //RaycastHit[] hits = Physics.RaycastAll(new Ray(camera.transform.position, (player.transform.position - camera.transform.position)));
    RaycastHit[] hits = Physics.BoxCastAll(camera.transform.position, new Vector3(scalar, scalar, scalar), camera.transform.forward);
    foreach (RaycastHit hit in hits){
      print(Camera.main.WorldToViewportPoint(hit.point).y);
      if (hit.transform.tag == "Wall" && Camera.main.WorldToViewportPoint(hit.point).y < .5f){
        Debug.DrawRay(player.transform.position, hit.transform.position - player.transform.position, Color.red);
        Color col = hit.transform.GetComponent<MeshRenderer>().material.color;
        col.a = Mathf.Clamp(col.a - .2f, .4f, 1f);
        hit.transform.GetComponent<MeshRenderer>().material.color = col;
      }
    }

    foreach (GameObject wall in Walls){
      bool isHit = false;
      foreach (RaycastHit hit in hits){
        if (hit.transform.gameObject == wall && Camera.main.WorldToViewportPoint(hit.point).y < .5f)
        {
          isHit = true;
          break;
        }
      }
      if (!isHit){
        Color col = wall.transform.GetComponent<MeshRenderer>().material.color;
        col.a = Mathf.Clamp01(col.a + .2f);
        wall.transform.GetComponent<MeshRenderer>().material.color = col;
      }
    }
  }
}
