﻿using System;

using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

	///<summary>
	/// Creates a custom editor GUI for a value to set on a Blackboard object.
	///</summary>
	[Serializable]
	public abstract class BlackboardValueEditor<T> : IBlackboardValueEditor
	{

		protected const string SERIALIZED_DATA_PROP = "m_SerializedData";

		/// <summary>
		/// Draws the GUI of the value (using IMGUI).
		/// </summary>
		/// <param name="_Position">The position and available space to draw the GUI.</param>
		/// <param name="_Item">The serialized property that represent an entry of a blackboard.</param>
		/// <param name="_Label">The label of the property.</param>
		public virtual void OnGUI(Rect _Position, SerializedProperty _Item, GUIContent _Label) { }

		/// <summary>
		/// Gets the expected height of the field in the editor.
		/// </summary>
		/// <param name="_Item">The serialized property that represent an entry of a blackboard.</param>
		/// <param name="_Label">The label of the property.</param>
		public virtual float GetPropertyHeight(SerializedProperty _Item, GUIContent _Label)
        {
			return MuffinDevGUI.LINE_HEIGHT;
        }

		/// <summary>
		/// Creates a VisualElement to draw the GUI of the item.
		/// </summary>
		/// <param name="_Item">The serialized property that represent an entry of a blackboard.</param>
		public virtual VisualElement CreatePropertyGUI(SerializedProperty _Item)
		{
			return null;
		}

		/// <summary>
		/// Gets the value of the given entry.
		/// </summary>
		/// <param name="_Item">The serialized property that represent an entry of a blackboard.</param>
		public T GetValue(SerializedProperty _Item)
        {
			SerializedProperty serializedDataProperty = _Item.FindPropertyRelative(SERIALIZED_DATA_PROP);
			return SerializationUtility.DeserializeFromString<T>(serializedDataProperty.stringValue);
		}

		/// <summary>
		/// Sets the serialized value of the given entry.
		/// </summary>
		/// <param name="_Item">The serialized property that represent an entry of a blackboard.</param>
		public void SetValue(SerializedProperty _Item, T _Value)
		{
			SerializedProperty serializedDataProperty = _Item.FindPropertyRelative(SERIALIZED_DATA_PROP);
			serializedDataProperty.stringValue = SerializationUtility.SerializeToString(_Value);
		}

		/// <summary>
		/// Gets the decorated type of this Blackboard Value Editor.
		/// </summary>
		public Type ValueType
        {
            get { return typeof(T); }
        }

	}

}