using UnityEngine;
using System;
using System.IO;

public class CrashController : MonoBehaviour {

	public void PerformFileException() {
		var lines = File.ReadAllLines(Guid.NewGuid().ToString());
		Debug.Log(lines.Length);
	}

	public void StackOverflow() {
		StackOverflow(0);
	}

	void StackOverflow(int x) {
		StackOverflow(x++);
	}

	public void SetNull() {
		GameObject go = null;
		go.SetActive(false);
	}

	public void Instantiation() {
		while ( true ) {
			var go = new GameObject("");
		}
	}
}
