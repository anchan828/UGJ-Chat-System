using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatManager : MonoBehaviour
{
	//文字入力などを管理するスクリプト　クライアント側
	
	public GUISkin skin;
	public GameObject userManager;
	private bool isLogin = false;
	private string playerName = "";
	private string comment = "";
	private Vector2 scroll = Vector2.zero;

	void Update ()
	{
		if (userManager == null)
			userManager = GameObject.FindGameObjectWithTag ("UserManager");
	}

	void OnGUI ()
	{
		if (skin != null)
			GUI.skin = skin;
		
		GUILayout.BeginHorizontal ();
		if (!isLogin) {
			GUILayout.Box ("名前");
			playerName = GUILayout.TextField (playerName, GUILayout.Width (Screen.width * 0.5f));
			if (GUILayout.Button ("ログイン")) {
				userManager.networkView.RPC ("AddName", RPCMode.AllBuffered, playerName);
				isLogin = true;
			}
		} else {
			GUILayout.EndHorizontal ();
			GUILayout.Box ("ようこそ   " + playerName + "   さん");
			GUILayout.BeginHorizontal ();
			GUILayout.Box ("コメント");
		
			comment = GUILayout.TextField (comment, GUILayout.Width (Screen.width * 0.5f));
			if (GUILayout.Button ("コメントする")) {
				userManager.networkView.RPC ("Comment", RPCMode.AllBuffered, playerName, comment);
				comment = "";
			}
		}
		GUILayout.EndHorizontal ();
		GUILayout.Space(50);
		GUILayout.BeginHorizontal ();
		GUILayout.Box ("名前", GUILayout.Width (Screen.width * 0.15f));
		GUILayout.Space (Screen.width * 0.02f);
		GUILayout.Box ("コメント", GUILayout.Width (Screen.width * 0.5f));
		GUILayout.Space (Screen.width * 0.02f);
		GUILayout.Box ("時間", GUILayout.Width (Screen.width * 0.25f));
		GUILayout.EndHorizontal ();
		
		scroll = GUILayout.BeginScrollView (scroll, GUILayout.Width (Screen.width), GUILayout.Height (Screen.height * 0.8f));
		foreach (string com in  userManager.GetComponent<UserManager>().commentList) {
			GUILayout.BeginHorizontal ();
			string[] split = com.Split (',');
			GUILayout.Box (split [0], GUILayout.Width (Screen.width * 0.15f));
			GUILayout.Space (Screen.width * 0.02f);
			GUILayout.Box (split [1], GUILayout.Width (Screen.width * 0.5f));
			GUILayout.Space (Screen.width * 0.02f);
			GUILayout.Box (split [2], GUILayout.Width (Screen.width * 0.25f));
			GUILayout.EndHorizontal ();
		}
		GUILayout.EndScrollView ();
	}
	
}
