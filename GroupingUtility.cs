using UnityEditor;
using UnityEngine;

namespace Utilities 
{
    public class GroupingUtility
    {
        [MenuItem("GameObject/Group it %g")]
        private static void GroupSelection() 
        {
            // If there's nothing selected then return
            if(!Selection.activeTransform) 
                return;

            // Create parent gameobject
            GameObject _go = new GameObject
            {
                name = "Group_Parent"
            };

            Undo.RegisterCreatedObjectUndo(_go, "Group");

            // Set parent to selection
            _go.transform.SetParent(Selection.activeTransform.parent, false);

            Vector3 mediumPosition = new Vector3(0,0,0);
            int numberOfObjectsSelected = 0;

            // Iterate over each selection and add it to parent gameobject created
            foreach (Transform transform in Selection.transforms)
            {
                mediumPosition += transform.position;
                numberOfObjectsSelected++;
                Undo.SetTransformParent(transform, _go.transform, "Group");
            }

            // Set parent in medium position
            _go.transform.position = mediumPosition / numberOfObjectsSelected;

            // Select parent gameobject
            Selection.activeGameObject = _go;
        }
    }
}

