using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// A class to control network manager and networkmanager hud. 
public class NetworkScript : NetworkManager
{
	public override void OnStartHost()
	{
		GameManager.instance.StartGame();
	}
	public override void OnClientConnect(NetworkConnection conn)
	{
		base.OnClientConnect(conn);
		GameManager.instance.StartGame();
	}
}