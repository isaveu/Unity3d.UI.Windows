﻿using UnityEditor.UI.Windows.Plugins.DevicePreview;
using UnityEditor;
using UnityEngine.UI.Windows;
using UnityEngine.UI.Windows.Plugins.Flow;
using UnityEngine;

namespace UnityEditor.UI.Windows.Plugins.Flow {
	
	public interface IWindowFlowAddon : IWindowAddon {

		void OnFlowWindowGUI(FlowWindow window);
		void OnFlowSettingsGUI();
		void OnFlowToolbarGUI(GUIStyle toolbarButton);
		
	}

	public class Flow : IWindowAddon {
		
		#if UNITY_EDITOR
		[MenuItem("Window/UI.Windows: Flow")]
		public static void ShowEditor() {
			
			Flow.ShowEditor(null);
			
		}
		
		[MenuItem("Assets/Open In Flow Editor", validate = true)]
		private static bool ContextMenuValidate() {
			
			return Selection.activeObject is FlowData;
		}
		
		[MenuItem("Assets/Open In Flow Editor")]
		[MenuItem("Window/UI.Windows: Flow")]
		public static void ShowEditorFromContextMenu() {
			
			var editor = Flow.ShowEditor(null);
			
			var selectedObject = Selection.activeObject as FlowData;
			if (selectedObject != null) {
				
				editor.OpenFlowData(selectedObject);

			}
			
		}

		public static FlowSystemEditorWindow ShowEditor(System.Action onClose) {
			
			var editor = EditorWindow.GetWindow<FlowSystemEditorWindow>();
			editor.title = "UI.Windows: Flow";
			editor.autoRepaintOnSceneChange = true;

			return editor;

		}
		#endif

		public void Show(System.Action onClose) {

			Flow.ShowEditor(onClose);

		}
		
		public static void OnDrawWindowGUI(FlowWindow window) {
			
			var flowAddons = WindowUtilities.GetAddons<IWindowFlowAddon>();
			foreach (var addon in flowAddons) {
				
				addon.OnFlowWindowGUI(window);
				
			}
			
		}
		
		public static void OnDrawSettingsGUI() {
			
			var flowAddons = WindowUtilities.GetAddons<IWindowFlowAddon>();
			foreach (var addon in flowAddons) {
				
				addon.OnFlowSettingsGUI();
				
			}
			
		}
		
		public static void OnDrawToolbarGUI(GUIStyle buttonStyle) {
			
			var flowAddons = WindowUtilities.GetAddons<IWindowFlowAddon>();
			foreach (var addon in flowAddons) {
				
				addon.OnFlowToolbarGUI(buttonStyle);
				
			}
			
		}

	}

}