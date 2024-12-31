using System.Collections.Generic;
using UnityEngine;

public class RewindRecorder : MonoBehaviour
{
    [SerializeField] private int FramesPerSecond;
    private float speed;
    private float elapsedTime = 0;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private List<TimePoint> rewindPoints;
    public bool rewind;

    private void Start() {
        speed = 1/(float)FramesPerSecond;
        Debug.Log(speed);
        rewindPoints = new List<TimePoint>();
    }

    private void Update() {
        if (rewind == false) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= speed) {
                TimePoint current = new TimePoint();
                current.Frame = spriteRenderer.sprite;
                if (transform.rotation == Quaternion.Euler(0, 0, 0)) {
                    current.FacingRight = true;
                }
                else {
                    current.FacingRight = false;
                }

                current.Position = transform.position;
                rewindPoints.Add(current);

                elapsedTime = 0;
            }

            Debug.Log(rewindPoints.Count);
        }
        
    }
}
