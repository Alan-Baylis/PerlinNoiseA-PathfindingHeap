﻿using UnityEngine;
using System.Collections;


	public class Unit : MonoBehaviour {

		public Transform target;
		public Vector3[] path;
		int targetIndex;
	    public float speed = 0f;

		void Update(){
			/*if (Input.GetKeyDown (KeyCode.A)) {
				PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
			}*/
		}

		public void OnPathFound(Vector3[] newPath, bool pathSuccessful){
			if (pathSuccessful) {
				path = newPath;
				StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
                Vector3 currentWaypoint = path[0];
			}
		}
		IEnumerator FollowPath (){
            
			Vector3 currentWaypoint = path[0];

			while (true) {
				if(transform.position == currentWaypoint && (Vector3.Distance(transform.position, currentWaypoint) < 5.0f))
            {
					targetIndex ++;
					if(targetIndex >= path.Length){
						targetIndex = 0;
						yield break;
					}
					currentWaypoint = path[targetIndex];

				}
				transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
				yield return null;
			}

		}

		public void OnDrawGizmos(){
			if (path != null) {
				for (int i = targetIndex; i < path.Length; i++){
					Gizmos.color = Color.black;
					Gizmos.DrawCube(path[i], Vector3.one);

					if(i == targetIndex){
						Gizmos.DrawLine(transform.position, path[i]);
					}
					else {
						Gizmos.DrawLine(path[i-1], path[i]);
					}
				}
			}
		}
	}
