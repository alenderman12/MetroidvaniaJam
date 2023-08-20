using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;
	[Space]
	public PlayerMovement playerMove;
	public PlayerAttack playerAttack;

	public Transform camTransform;
	public CinemachineVirtualCamera CM_Camera;
	public CinemachineConfiner2D CM_Confiner;
	// Start is called before the first frame update
	void Start()
    {
        if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
    }

    private void Update()
    {

    }
}
