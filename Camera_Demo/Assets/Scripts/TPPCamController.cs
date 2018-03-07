using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPCamController : MonoBehaviour
{
  GameObject player;
  TPPPlayerController playerController;

  [SerializeField]
  bool lockCameraRotation = false;

  [SerializeField]
  float rotationalSpeed, cameraLimit;

  // Use this for initialization
  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
    playerController = player.GetComponent<TPPPlayerController>();
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 temp = Vector3.zero;
    if (Input.GetKey(KeyCode.W))
    {
      temp += transform.forward;
    }
    if (Input.GetKey(KeyCode.S))
    {
      temp += -transform.forward;
    }
    if (Input.GetKey(KeyCode.A))
    {
      temp += -transform.right;
    }
    if (Input.GetKey(KeyCode.D))
    {
      temp += transform.right;
    }
    temp.y = 0;
    temp.Normalize();
    playerController.MovePlayer(temp);

    if (Input.GetKeyDown(KeyCode.Escape))
    {
      Cursor.visible = !Cursor.visible;
      if (Cursor.visible)
      {
        Cursor.lockState = CursorLockMode.None;
      }
      else
      {
        Cursor.lockState = CursorLockMode.Locked;
      }
    }

    if (Cursor.lockState == CursorLockMode.Locked)
    {
      float mouseXVal = Input.GetAxis("Mouse X");
      float mouseYVal = Input.GetAxis("Mouse Y");
      //print(transform.localRotation.x * Mathf.Rad2Deg + (rotationalSpeed * Time.deltaTime));
      Vector3 recalcAngle = Vector3.zero;
      recalcAngle = transform.forward;
      recalcAngle.y = 0;
      recalcAngle.Normalize();
      //print(-Mathf.Sign(transform.forward.y) * Mathf.Acos(Vector3.Dot(transform.forward, recalcAngle)) * Mathf.Rad2Deg);
      if (Input.GetKey(KeyCode.UpArrow))
      {
        transform.Rotate(new Vector3(-rotationalSpeed * Time.deltaTime, 0, 0));
      }
      if (Input.GetKey(KeyCode.DownArrow))
      {
        transform.Rotate(new Vector3(rotationalSpeed * Time.deltaTime, 0, 0));
      }
      //transform.Rotate(new Vector3(Time.deltaTime * rotationalSpeed * mouseYVal, 0, 0));
      if (-Mathf.Sign(transform.forward.y) * Mathf.Acos(Vector3.Dot(transform.forward, recalcAngle)) * Mathf.Rad2Deg > cameraLimit)
      {
        transform.localRotation = Quaternion.Euler(new Vector3(cameraLimit, 0, 0));
      }
      else if (-Mathf.Sign(transform.forward.y) * Mathf.Acos(Vector3.Dot(transform.forward, recalcAngle)) * Mathf.Rad2Deg < -cameraLimit)
      {
        transform.localRotation = Quaternion.Euler(new Vector3(-cameraLimit, 0, 0));
      }
      transform.parent.Rotate(new Vector3(0, -Time.deltaTime * mouseXVal * rotationalSpeed, 0));
    }
  }

  private void LateUpdate()
  {
    if (!lockCameraRotation)
    {
      transform.parent.position = player.transform.position;
    }
  }
}
