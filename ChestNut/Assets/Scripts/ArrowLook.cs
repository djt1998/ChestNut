using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLook : MonoBehaviour
{
    [SerializeField]
    private Transform m_player;
    public Transform player {get {return m_player;}}
    [SerializeField]
    private Transform m_Target;
    public Transform LookAtTarget {get {return m_Target;}}
    private Vector3 offset = new Vector3(0, 2, 0);
    private Player p;
    [SerializeField]
    private Transform m_Spinner;
    public Transform Spinner {get {return m_Spinner;}}
    // [SerializeField]
    // private Transform m_Scaler;
    // public Transform Scaler {get {return m_Scaler;}}

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();
        speed = 20f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offset.y = p.getRadius() / 2.2f;
        offset.z = -p.getRadius() / 2.2f;
        transform.position = player.position + offset;
        if (LookAtTarget) {
            transform.LookAt(LookAtTarget);
        }

        if (Spinner) {
            Spinner.transform.Rotate(0, 0, speed * Time.deltaTime);
        }
    }

    private void SetTarget(Transform target = null) {
        m_Target = target;
    }
}
