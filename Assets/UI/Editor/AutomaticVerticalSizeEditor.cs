using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AutoUISizer))]
public class AutomaticVerticalSizeEditor : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		if( GUILayout.Button ("Recalculate UI Size") ) {
			((AutoUISizer)target).AdjustSize();
		}
	}
}
