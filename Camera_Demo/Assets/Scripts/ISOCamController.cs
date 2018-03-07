using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ISOCamController : MonoBehaviour
{
  GameObject player;

  [SerializeField]
  float Angle, Distance;

  [SerializeField]
  bool lockCameraRotation = false;

  float Height, Length;
  // Use this for initialization
  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
    if (lockCameraRotation)
    {
      transform.parent.parent = player.transform;
    }
  }

  private void Update()
  {
    Length = Mathf.Cos(Mathf.Deg2Rad * Angle) * Distance;
    Height = Mathf.Sin(Mathf.Deg2Rad * Angle) * Distance;

    if (Input.GetKeyDown(KeyCode.Mouse0))
    {
      RaycastHit[] output = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
      foreach (RaycastHit hit in output)
      {
        if (hit.transform.tag == "Ground")
        {
          player.GetComponent<NavMeshAgent>().SetDestination(hit.point);
          break;
        }
      }
    }

    if (Input.GetKey(KeyCode.Q))
    {
      transform.parent.Rotate(new Vector3(0, 180 * Time.deltaTime, 0));
    }
    if (Input.GetKey(KeyCode.E))
    {
      transform.parent.Rotate(new Vector3(0, -180 * Time.deltaTime, 0));
    }
  }

  private void LateUpdate()
  {
    if (!lockCameraRotation){
      transform.parent.position = player.transform.position;
    }
    transform.localRotation = Quaternion.Euler(Angle, 0, 0);
    Vector3 temp = transform.localPosition;
    temp.z = -Length;
    temp.y = Height;
    transform.localPosition = temp;
  }
}
