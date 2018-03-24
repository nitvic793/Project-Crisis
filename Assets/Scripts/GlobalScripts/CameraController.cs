using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [Range(0f,1f)]
    public float movementSpeed = 0.1f;
    [Range(0f, 10f)]
    public float zoomSpeed = 1f;
    [Range(0f,10f)]
    public float rotationSpeed = 4f;
    [Range(0f,1f)]
    public float smoothness = 0.85f;

    public float minPositionX = 1f;
    public float minPositionY = 1f;
    public float minPositionZ = -10f;
    public float maxPositionX = 20f;
    public float maxPositionY = 10f;
    public float maxPositionZ = 1f;

    Vector3 targetPosition;
    
    public Quaternion targetRotation;
    float targetRotationY;
    float targetRotationX;

    // Use this for initialization
    void Start()
	{
		targetPosition = transform.position;
		targetRotation = transform.rotation;
		targetRotationY = transform.localRotation.eulerAngles.y;
		targetRotationX = transform.localRotation.eulerAngles.x;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            targetPosition += transform.forward * zoomSpeed;
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            targetPosition -= transform.forward * zoomSpeed;

        if ( Input.GetKey( KeyCode.W ) )
            targetPosition += transform.forward * movementSpeed;
        if ( Input.GetKey( KeyCode.A ) )
            targetPosition -= transform.right * movementSpeed;
        if( Input.GetKey( KeyCode.S ) )
            targetPosition -= transform.forward * movementSpeed;
        if( Input.GetKey( KeyCode.D ) )
            targetPosition += transform.right * movementSpeed;
        if( Input.GetKey( KeyCode.Q ) )
            targetPosition -= transform.up * movementSpeed;
        if( Input.GetKey( KeyCode.E ) )
            targetPosition += transform.up * movementSpeed;

        if( Input.GetMouseButton( 1 ) )
        {
            Cursor.visible = false;
            targetRotationY += Input.GetAxis( "Mouse X" ) * rotationSpeed;
            targetRotationX -= Input.GetAxis( "Mouse Y" ) * rotationSpeed;
            targetRotation = Quaternion.Euler( targetRotationX, targetRotationY, 0.0f );
        }
        else
            Cursor.visible = true;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minPositionX, maxPositionX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPositionY, maxPositionY);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minPositionZ, maxPositionZ);

        transform.position = Vector3.Lerp( transform.position, targetPosition, ( 1.0f - smoothness ) );
        transform.rotation = Quaternion.Lerp( transform.rotation, targetRotation, ( 1.0f - smoothness ) );

      
    }
}
