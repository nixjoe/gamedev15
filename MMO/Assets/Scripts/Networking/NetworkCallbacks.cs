﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
		IList<string> logMessages = new List<string> ();

		public override void SceneLoadLocalDone (string map)
		{
				var pos = new Vector3 (Random.Range (-4, 4), 0, Random.Range (-4, 4));

				BoltNetwork.Instantiate (BoltPrefabs.PlayerLANObject, pos, Quaternion.identity);
		}

		public override void OnEvent (LogEvent evnt)
		{
				logMessages.Insert (0, evnt.Message);
		}

		void OnGUI ()
		{
				int maxMessages = Mathf.Min (5, logMessages.Count);

				GUILayout.BeginArea (new Rect (Screen.width / 2 - 200, Screen.height - 100, 400, 100), GUI.skin.box);

				for (int i = 0; i < maxMessages; ++i) {
						GUILayout.Label (logMessages [i]);
				}

				GUILayout.EndArea ();
		}
}
