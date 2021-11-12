using System.Collections;
using UnityEngine;

public class RemovePrefix : MonoBehaviour
{
    public Transform MixamoRigObject = default;
    public string _prefix = "mixamorig:";
    
    [ContextMenu("Remove Prefix")]
    public void RemovePrefixFromNames()
    {
        foreach (Transform child in MixamoRigObject)
        {
            StartCoroutine(FixPrefix(child));
        }
    }
    
    private IEnumerator FixPrefix(Transform go)
    {
        foreach (Transform child in MixamoRigObject)
        {
            yield return null;

            if (child.name.Contains(_prefix))
            {
                child.name =  child.name.Split(':')[1];
                Debug.Log($"Removed prefix from {child.name}");
            }

            foreach (Transform grandChild in child)
                yield return StartCoroutine(FixPrefix(grandChild));
        }
    }
}
