using UnityEngine;

public class LineRendererX : MonoBehaviour {

    private LineRenderer lineRendeder;
    private Vector3 laserHit;
    public Transform target;

    private void Start()
    {
        lineRendeder = GetComponent<LineRenderer>();
        laserHit = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        lineRendeder.SetPosition(0, laserHit);
        lineRendeder.SetPosition(1, target.position);
    }
}
