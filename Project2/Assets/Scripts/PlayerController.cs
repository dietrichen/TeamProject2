using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour
{
	struct CubeState
	{
		public int x;
		public int y;
		public float zRotate;
		public float spearY;
	}

	//float attacked= 0;//this is to control the timing for the spear going out and then back to resting as well as a cooldown


	[SyncVar]
	bool attacking = false;
	float movementSpeed = 8f;
	float rotationSpeed = 4f;
	public int score = 0;//kills
	public GameObject spear;
	[SyncVar]
	CubeState state;
	// Use this for initialization
	void Start()
	{
		InitState();
	}

	[Server]
	void InitState()
	{
		state = new CubeState
		{
			x = -4,
			y = 0,
			zRotate = 0,
			spearY = 0
		};
	}

	// Update is called once per frame
	void Update()
	{
		if (isLocalPlayer)
		{
			KeyCode[] arrowKeys = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow };
			foreach (KeyCode arrowKey in arrowKeys)
			{
				if (!Input.GetKeyDown(arrowKey))
					continue;
				//state = Move(state, arrowKey);
				CmdMoveOnServer(arrowKey);
			}

			KeyCode[] rotKeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
			foreach (KeyCode rotkey in rotKeys)
			{
				if (!Input.GetKeyDown(rotkey))
					continue;
				//state = Rotate(state, rotkey);
				CmdRotateOnServer(rotkey);
			}
		}
		SyncState();
	}

	[Command]
	void CmdMoveOnServer(KeyCode arrowKey)
	{
		state = Move(state, arrowKey);
	}
	[Command]
	void CmdAttackOnServer()
	{
		attacking = true;
	}
	[Command]
	void CmdRotateOnServer(KeyCode rotkey)
	{
		state = Rotate(state, rotkey);
	}

	void SyncState()
	{
		if (!attacking)
		{
			transform.position = Vector2.Lerp(transform.position, new Vector2(state.x, state.y), Time.deltaTime * movementSpeed);
			Quaternion target = Quaternion.Euler(0, 0, state.zRotate);
			transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotationSpeed);
		}
		else
		{
			StartCoroutine(Wait(1f));
		}
	}
	IEnumerator Wait(float seconds)
	{
		if (state.zRotate == 0 || state.zRotate == 180)
		{
			if (state.zRotate == 0)
			{
				state.spearY = 0.8f;
			}
			else
			{
				state.spearY = -0.8f;
			}
			spear.transform.position = Vector3.Lerp(spear.transform.position, new Vector3(spear.transform.position.x, state.spearY, spear.transform.position.z), Time.deltaTime * movementSpeed);
			yield return new WaitForSeconds(seconds);
			state.spearY = 0f;
			spear.transform.position = Vector3.Lerp(spear.transform.position, new Vector3(spear.transform.position.x, state.spearY, spear.transform.position.z), Time.deltaTime * movementSpeed);
			attacking = false;
		}
		else
		{
			if (state.zRotate == 270)
			{
				state.spearY = 0.8f;
			}
			else
			{//270
				state.spearY = -0.8f;
			}
			spear.transform.position = Vector3.Lerp(spear.transform.position, new Vector3(state.spearY, spear.transform.position.y, spear.transform.position.z), Time.deltaTime * movementSpeed);
			yield return new WaitForSeconds(seconds);
			state.spearY = 0f;
			spear.transform.position = Vector3.Lerp(spear.transform.position, new Vector3(state.spearY, spear.transform.position.y, spear.transform.position.z), Time.deltaTime * movementSpeed);
			attacking = false;
		}
	}

	CubeState Move(CubeState previous, KeyCode arrowKey)
	{
		int dx = 0;
		int dy = 0;
		switch (arrowKey)
		{
			case KeyCode.UpArrow:
				dy = 1;
				break;
			case KeyCode.DownArrow:
				dy = -1;
				break;
			case KeyCode.RightArrow:
				dx = 1;
				break;
			case KeyCode.LeftArrow:
				dx = -1;
				break;
		}
		return new CubeState
		{
			x = dx + previous.x,
			y = dy + previous.y,
			zRotate = previous.zRotate
		};
	}

	CubeState Rotate(CubeState previous, KeyCode rotKey)
	{
		float dRot = 0f;
		switch (rotKey)
		{
			case KeyCode.W:
				dRot = 0f;
				break;
			case KeyCode.D:
				dRot = 270f;
				break;
			case KeyCode.S:
				dRot = 180f;
				break;
			case KeyCode.A:
				dRot = 90f;
				break;
		}
		return new CubeState
		{
			x = previous.x,
			y = previous.y,
			zRotate = dRot
		};
	}
}

