using UnityEngine;
using UnityEditor;

/*
 * 
 *								 Transform Colours
 *			
 *			Script written by: Jonathan Carter (https://jonathan.carter.games)
 *									Version: 1.1.3
 *							  Last Updated: 20/05/2020
 * 
 * 
*/

[CanEditMultipleObjects, CustomEditor(typeof(Transform))]
public class TransformColours : Editor
{
    /// <summary>
    /// A reference to the T component
    /// </summary>
    Transform T;

    /// <summary>
    /// Boolean values for ( Position / Rotation / Scale ) Changes
    /// </summary>
    bool posChange, rotChange, scaleChange;

    /// <summary>
    /// Vector3 Values for initial ( Position / Rotation / Scale ) Values
    /// </summary>
    Vector3 initPos, initRot, initScale;


    /// <summary>
    /// Overrides the default inspector with the one made in this script...
    /// </summary>
    public override void OnInspectorGUI()
    {
        T = targets[0] as Transform;          // Assigns the T componenet to the T Variable
        TransformColoursInspector();    // Runs the method that holds all the editor code 
    }


    /// <summary>
    /// Does the colour change for each box as well as detects changes in each ( Position / Rotation / Scale ) - ( X / Y / Z ) 
    /// </summary>
    void TransformColoursInspector()
    {
        posChange = false;              // Setting the Position change to be false ready for a fresh check
        rotChange = false;              // Setting the Rotation change to be false ready for a fresh check
        scaleChange = false;            // Setting the Scale change to be false ready for a fresh check

        initPos = T.localPosition;      // Setting the Initial Position of the Transform to what the current local position is
        initRot = T.localEulerAngles;   // Setting the Initial Rotation of the Transform to what the current local rotation is
        initScale = T.localScale;       // Setting the Initial Scale of the Transform to what the current local scale is

        /*
         * 
         * 
         *                  Position Editor
         *
         *
        */ 

        // Position Label - Adjusts to whether or not the inspector is in wide mode
        if (EditorGUIUtility.wideMode)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Position", GUILayout.MinWidth(90), GUILayout.ExpandHeight(false));
        }
        else
        {
            GUILayout.Label("Position", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(false));
        }

        // Making the Position Vector3 Boxes in thier colours 
        EditorGUIUtility.labelWidth = 15;
        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginChangeCheck();

        // Position X - Red
        GUI.backgroundColor = Color.red;
        SerializedProperty posX = serializedObject.FindProperty("m_LocalPosition.x");
        EditorGUILayout.PropertyField(posX, new GUIContent("X"));
        GUI.backgroundColor = Color.white;

        // Position Y - Green
        GUI.backgroundColor = Color.green;
        SerializedProperty posY = serializedObject.FindProperty("m_LocalPosition.y");
        EditorGUILayout.PropertyField(posY, new GUIContent("Y"));
        GUI.backgroundColor = Color.white;

        // Position Z - Blue
        GUI.backgroundColor = Color.blue;
        SerializedProperty posZ = serializedObject.FindProperty("m_LocalPosition.z");
        EditorGUILayout.PropertyField(posZ, new GUIContent("Z"));
        GUI.backgroundColor = Color.white;
        EditorGUILayout.EndHorizontal();

        // Runs if a edit was made to one of the fields above
        if (EditorGUI.EndChangeCheck())
        {
            posChange = true;
        }

        // Adjusts the editor Hoz grouping so the label shows above the boxes if the inspector is not in wide mode
        if (EditorGUIUtility.wideMode)
        {
            EditorGUILayout.EndHorizontal();
        }

        /*
         * 
         * 
         *                  Rotation Editor
         *
         *
        */

        // Rotation Label - Adjusts to whether or not the inspector is in wide mode
        if (EditorGUIUtility.wideMode)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Rotation", GUILayout.MinWidth(90), GUILayout.ExpandHeight(false));
        }
        else
        {
            GUILayout.Label("Rotation", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(false));
        }

        // Making the  RotationVector3 Boxes in thier colours 
        EditorGUIUtility.labelWidth = 15;
        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginChangeCheck();

        // Rotation X - Red
        GUI.backgroundColor = Color.red;
        SerializedProperty rotX = serializedObject.FindProperty("m_LocalEulerAnglesHint.x");
        EditorGUILayout.PropertyField(rotX, new GUIContent("X"));
        GUI.backgroundColor = Color.white;

        // Rotation Y - Green
        GUI.backgroundColor = Color.green;
        SerializedProperty rotY = serializedObject.FindProperty("m_LocalEulerAnglesHint.y");
        EditorGUILayout.PropertyField(rotY, new GUIContent("Y"));
        GUI.backgroundColor = Color.white;

        // Rotation Z - Blue
        GUI.backgroundColor = Color.blue;
        SerializedProperty rotZ = serializedObject.FindProperty("m_LocalEulerAnglesHint.z");
        EditorGUILayout.PropertyField(rotZ, new GUIContent("Z"));
        GUI.backgroundColor = Color.white;
        EditorGUILayout.EndHorizontal();


        /* 
         * Makes it so only the local euler is changed, 1.1.0 and earlier used just euler angles which caused a problem with the parent & child editing... 
         * Kinda my bad, should act like the normal T on rotations now xD Rotation is awkward when its split up into its x,y,z values...
        */
        T.localEulerAngles = new Vector3(rotX.floatValue, rotY.floatValue, rotZ.floatValue);


        // Runs if a edit was made to one of the fields above
        if (EditorGUI.EndChangeCheck())
        {
            rotChange = true;
        }


        // 1.1.2 patch, seems to fix the issue where the editor doesn't update correctly when a change is made. Not sure why... rotations are not exactly easy when it comes to custom editors.
        EditorUtility.SetDirty(T);

        // Adjusts the editor Hoz grouping so the label shows above the boxes if the inspector is not in wide mode
        if (EditorGUIUtility.wideMode)
        {
            EditorGUILayout.EndHorizontal();
        }

        /*
         * 
         * 
         *                  Scale Editor
         *
         *
        */

        // Scale Label - Adjusts to whether or not the inspector is in wide mode
        if (EditorGUIUtility.wideMode)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Scale", GUILayout.MinWidth(90), GUILayout.ExpandHeight(false));
        }
        else
        {
            GUILayout.Label("Scale", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(false));
        }

        // Making the Position Vector3 Boxes in thier colours 
        EditorGUIUtility.labelWidth = 15;
        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginChangeCheck();

        // Scale X - Red
        GUI.backgroundColor = Color.red;
        SerializedProperty scaleX = serializedObject.FindProperty("m_LocalScale.x");
        EditorGUILayout.PropertyField(scaleX, new GUIContent("X"));
        GUI.backgroundColor = Color.white;

        // Scale Y - Green
        GUI.backgroundColor = Color.green;
        SerializedProperty scaleY = serializedObject.FindProperty("m_LocalScale.y");
        EditorGUILayout.PropertyField(scaleY, new GUIContent("Y"));
        GUI.backgroundColor = Color.white;

        // Scale Z - Blue
        GUI.backgroundColor = Color.blue;
        SerializedProperty scaleZ = serializedObject.FindProperty("m_LocalScale.z");
        EditorGUILayout.PropertyField(scaleZ, new GUIContent("Z"));
        GUI.backgroundColor = Color.white;
        EditorGUILayout.EndHorizontal();

        // Runs if a edit was made to one of the fields above
        if (EditorGUI.EndChangeCheck())
        {
            scaleChange = true;
        }

        // Adjusts the editor Hoz grouping so the label shows above the boxes if the inspector is not in wide mode
        if (EditorGUIUtility.wideMode)
        {
            EditorGUILayout.EndHorizontal();
        }

        /*
         * 
         * 
         *                  Apply Changes
         *
         *
        */

        // Gets all objects currently selected in the editor
        Transform[] SelectedTransforms = Selection.GetTransforms(SelectionMode.Editable);

        // If there is atleast 1 object selected
        if (SelectedTransforms.Length > 1)
        {
            // Go through them all and adjust their values where they have been changed
            for (int i = 0; i < SelectedTransforms.Length; i++)
            {
                if (posChange)
                {
                    SelectedTransforms[i].localPosition = ApplyPositionWhatChange(SelectedTransforms[i].localPosition, initPos, T.localPosition);
                }
                if (rotChange)
                {
                    SelectedTransforms[i].localEulerAngles = ApplyRotationWhatChange(SelectedTransforms[i].localEulerAngles, initRot, T.localEulerAngles);
                }
                if (scaleChange)
                {
                    SelectedTransforms[i].localScale = ApplyScaleWhatChange(SelectedTransforms[i].localScale, initScale, T.localScale);
                }
            }
        }

        // Apply the changes and update the editor (also fixed animation recording problems.... 1.1.3....)
        Undo.RecordObjects(this.targets, "All");
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }


    /// <summary>
    /// Updates the position for what values were changed...
    /// </summary>
    /// <param name="ToApply">What value is been changed</param>
    /// <param name="Init">What the value was before it was changed</param>
    /// <param name="Change">What the value is been changed too</param>
    /// <returns></returns>
    Vector3 ApplyPositionWhatChange(Vector3 ToApply, Vector3 Init, Vector3 Change)
    {
        if (!Mathf.Approximately(Init.x, Change.x))
        {
            ToApply.x = T.localPosition.x;
        }

        if (!Mathf.Approximately(Init.y, Change.y))
        {
            ToApply.y = T.localPosition.y;
        }

        if (!Mathf.Approximately(Init.z, Change.z))
        {
            ToApply.z = T.localPosition.z;
        }

        Undo.RecordObjects(this.targets, "Transform Colours - Position Change");

        return ToApply;
    }


    /// <summary>
    /// Updates the rotation for what values were changed...
    /// </summary>
    /// <param name="ToApply">What value is been changed</param>
    /// <param name="Init">What the value was before it was changed</param>
    /// <param name="Change">What the value is been changed too</param>
    /// <returns></returns>
    Vector3 ApplyRotationWhatChange(Vector3 ToApply, Vector3 Init, Vector3 Change)
    {
        if (!Mathf.Approximately(Init.x, Change.x))
        {
            ToApply.x = T.localEulerAngles.x;
        }
        else
        {
            ToApply.x = Init.x;
        }

        if (!Mathf.Approximately(Init.y, Change.y))
        {
            ToApply.y = T.localEulerAngles.y;
        }
        else
        {
            ToApply.y = Init.y;
        }

        if (!Mathf.Approximately(Init.z, Change.z))
        {
            ToApply.z = T.localEulerAngles.z;
        }
        else
        {
            ToApply.z = Init.z;
        }

        Undo.RecordObjects(this.targets, "Transform Colours - Rotation Change");

        return ToApply;
    }


    /// <summary>
    /// Updates the scale for what values were changed...
    /// </summary>
    /// <param name="ToApply">What value is been changed</param>
    /// <param name="Init">What the value was before it was changed</param>
    /// <param name="Change">What the value is been changed too</param>
    /// <returns></returns>
    Vector3 ApplyScaleWhatChange(Vector3 ToApply, Vector3 Init, Vector3 Change)
    {
        if (!Mathf.Approximately(Init.x, Change.x))
        {
            ToApply.x = T.localScale.x;
        }

        if (!Mathf.Approximately(Init.y, Change.y))
        {
            ToApply.y = T.localScale.y;
        }

        if (!Mathf.Approximately(Init.z, Change.z))
        {
            ToApply.z = T.localScale.z;
        }

        Undo.RecordObjects(this.targets, "Transform Colours - Scale Change");

        return ToApply;
    }
}