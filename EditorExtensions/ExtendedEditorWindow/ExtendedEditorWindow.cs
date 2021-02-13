using UnityEditor;

namespace Utilities
{
	public class ExtendedEditorWindow : EditorWindow
	{
		protected SerializedObject serializedObject;
		protected SerializedProperty currentProperty;

		protected void DrawProperties(SerializedProperty property, bool drawChildren)
		{
			string lastPropPath = string.Empty;
			
			while(property.NextVisible(true))
			{
				if (property.isArray && property.propertyType == SerializedPropertyType.Generic)
				{
					EditorGUILayout.BeginHorizontal();
					property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, property.displayName);
					EditorGUILayout.EndHorizontal();

					if (property.isExpanded)
					{
						EditorGUI.indentLevel++;
						DrawProperties(property,drawChildren);
						EditorGUI.indentLevel--;
					}
				}
				else
				{
					if (!string.IsNullOrEmpty(lastPropPath) && property.propertyPath.Contains(lastPropPath))
					{
						continue;
					}

					lastPropPath = property.propertyPath;
					EditorGUILayout.PropertyField(property, drawChildren);
				}
			}
		}
		
		protected void ApplyChanges()
		{
			serializedObject.ApplyModifiedProperties();
		}
		
	}
}