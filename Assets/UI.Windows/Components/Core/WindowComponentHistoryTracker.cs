﻿using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UI.Windows {

	public enum HistoryTrackerEventType : byte {

		Init,
		Deinit,

		ShowManual,
		HideManual,

		ShowBegin,
		HideBegin,

		ShowEnd,
		HideEnd,

		WindowOpen,
		WindowClose,

	};

	[System.Serializable]
	public class WindowComponentHistoryTracker {

		[System.Serializable]
		public class Item {

			public HistoryTrackerEventType eventType;
			public AppearanceParameters parameters;
			//[TextArea(1, 10)]
			public string stack;
			
			public Item(StackFrame[] stack, HistoryTrackerEventType eventType) {
				
				this.stack = string.Join("\n", stack.Select(x => x.ToString()).ToArray());
				this.eventType = eventType;
				
			}

			public Item(StackFrame[] stack, AppearanceParameters parameters, HistoryTrackerEventType eventType) : this(stack, eventType) {
				
				this.parameters = parameters;
				
			}

		}

		public List<Item> items = new List<Item>();
		
		public void Add(WindowComponentBase component, HistoryTrackerEventType eventType) {

			if (WindowSystemLogger.IsActiveComponents() == true) {

				var stack = new StackTrace();
				this.items.Add(new Item(stack.GetFrames(), eventType));
				
			}

		}

		public void Add(WindowComponentBase component, AppearanceParameters parameters, HistoryTrackerEventType eventType) {
			
			if (WindowSystemLogger.IsActiveComponents() == true) {

				var stack = new StackTrace();
				this.items.Add(new Item(stack.GetFrames(), parameters, eventType));

			}

		}

	}

}