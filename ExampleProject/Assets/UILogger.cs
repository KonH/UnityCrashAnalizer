using UnityEngine;
using UnityEngine.UI;

public class UILogger : MonoBehaviour {

	public Text Text;

	void Awake () {
		Application.logMessageReceived += Application_logMessageReceived;
	}

	public void Clear() {
		Text.text = "";
	}

	void Application_logMessageReceived(string condition, string stackTrace, LogType type) {
		var stackTraceShort = !string.IsNullOrEmpty(stackTrace) ? stackTrace.Split('\n')[0] : "";
		Text.text += string.Format("\n[{0}] {1}: {2}\n", condition, type, stackTraceShort);
	}
}
