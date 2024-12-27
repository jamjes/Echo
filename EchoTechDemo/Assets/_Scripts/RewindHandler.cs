using UnityEngine;
using System.Collections.Generic;

public class RewindHandler : MonoBehaviour
{
    public List<RewindDataObject> Points = new List<RewindDataObject>();
    public bool isRewinding = false;
    public int index;

    private void Update() {
        index = Points.Count;
        
        isRewinding = Input.GetKey(KeyCode.K);
        if (isRewinding) {
            if (Points.Count > 0) {
                RewindDataObject current = Points[Points.Count - 1];
                transform.position = current.Position;
                transform.rotation = current.Rotation;
                Points.RemoveAt(Points.Count - 1);
            }
        }
        else if (!isRewinding) {
            RewindDataObject newPoint = new RewindDataObject();
            newPoint.Position = transform.position;
            newPoint.Rotation = transform.rotation;

            Points.Add(newPoint);
        }
    }
}
