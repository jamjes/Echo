using UnityEngine;
using System.Collections.Generic;

public class RewindHandler : MonoBehaviour
{
    private List<RewindDataObject> Points = new List<RewindDataObject>();
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Player player;

    private void Start() {
        player = GetComponent<Player>();
    }

    private void Update() {

        if (player.IsRewinding) {
            if (Points.Count > 0) {
                RewindDataObject current = Points[Points.Count - 1];
                transform.position = current.Position;
                transform.rotation = current.Rotation;
                spriteRenderer.sprite = current.Frame;
                Points.RemoveAt(Points.Count - 1);
            }
        }
        else if (!player.IsRewinding) {
            RewindDataObject newPoint = ScriptableObject.CreateInstance<RewindDataObject>();
            newPoint.Position = transform.position;
            newPoint.Rotation = transform.rotation;
            newPoint.Frame = spriteRenderer.sprite;
            Points.Add(newPoint);
        }
    }
}
