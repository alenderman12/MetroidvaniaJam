using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
	private CinemachineVirtualCamera CM_Camera = null;
	private CinemachineCameraOffset CM_CameraOffset = null;
	[SerializeField] private Room currentRoom = null;

    // Start is called before the first frame update
    void Start()
    {
		CM_Camera = GetComponent<CinemachineVirtualCamera>();
		CM_CameraOffset = GetComponent<CinemachineCameraOffset>();
	}

    // Update is called once per frame
    void Update()
    {
        if(CM_Camera.Follow.position.x - currentRoom.roomSizeX >= CM_Camera.m_Lens.OrthographicSize)
		{
			Debug.Log("Near Edge");
		}
		Debug.Log("Follow: " + CM_Camera.Follow.position.x);
		Debug.Log("Diff: " + (CM_Camera.Follow.position.x - currentRoom.roomSizeX));
    }
}
