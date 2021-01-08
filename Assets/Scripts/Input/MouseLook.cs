using System.Collections;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXandY;
    public float horizontalSens = 9.0f;
    public float verticalSens = 9.0f;
    public float minVertRange = -45.0f;
    public float maxVertRange = 45.0f;

    private float _rotationX = 0;

    void Start()
    {
        Rigidbody playerBody = GetComponent<Rigidbody>();

        if (playerBody != null)
            playerBody.freezeRotation = true;
    }

    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * horizontalSens, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * verticalSens;
            _rotationX = Mathf.Clamp(_rotationX, minVertRange, maxVertRange);

            float _rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * verticalSens;
            _rotationX = Mathf.Clamp(_rotationX, minVertRange, maxVertRange);
            float delta = Input.GetAxis("Mouse X") * horizontalSens;
            float _rotationY = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }
        

    }
}
