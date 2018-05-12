using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;

public class CameraController : MonoBehaviour
{

    public Transform cameraTarget;
    private float x = 0.0f;
    private float y = 0.0f;

    private int mouseXSpeed = 2;
    private int mouseYSpeed = -2;

    public float maxViewDistance = 25; // how far the camera zoom out
    public float minViewDistance = 1; //how close the camera zoom in
    public int zoomRate = 30;  //zoom in/out speed
    public float cameraTargetHeight = 1f;
    private float distance = 3;  //camera start distance from player
    private float desiredDistance;  //
    private float correctedDistance; //
    private float currentDistance;

    public List<PostProcessingProfile> ppDatas;

    void Start()
    {
        cameraTarget = GameObject.FindWithTag("Player").transform;
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;

        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;
    }

    public void changePPD()
    {
        PostProcessingBehaviour ppb = GetComponent<PostProcessingBehaviour>();
        string groundName = GameObject.FindWithTag("Player").GetComponent<character_movement>().currentGround ? GameObject.FindWithTag("Player").GetComponent<character_movement>().currentGround.transform.name : "naturalIsland";
        switch (groundName)
        {
            case "naturalIsland":
                ppb.profile = ppDatas[0];
                break;
            case "stoneIsland":
                ppb.profile = ppDatas[1];
                break;
            case "desertIsland":
                ppb.profile = ppDatas[2];
                break;
        }
    }

    void LateUpdate()
    {
        changePPD();
        if (Input.GetMouseButton(1))
        {
            x += Input.GetAxis("Mouse X") * mouseXSpeed;
            y += Input.GetAxis("Mouse Y") * mouseYSpeed;
            //if (!character_movement.moving && !character_movement.activity) cameraTarget.transform.rotation = Quaternion.Euler(cameraTarget.transform.rotation.x, x, cameraTarget.transform.rotation.z);
        }
        else if (Input.GetAxis("RightJoystickAxisX") > 0 || Input.GetAxis("RightJoystickAxisY") > 0)
        {
            x += Input.GetAxis("RightJoystickAxisX") * mouseXSpeed;
            y += Input.GetAxis("RightJoystickAxisY") * mouseYSpeed;
            // if (!character_movement.moving && !character_movement.activity) cameraTarget.transform.rotation = Quaternion.Euler(cameraTarget.transform.rotation.x, x, cameraTarget.transform.rotation.z);
        }
        y = ClampAngle(y, -50, 80);

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);  //calculates the distance player zoom
        desiredDistance = Mathf.Clamp(desiredDistance, minViewDistance, maxViewDistance);  //add minim and maxim distance player can zoom in/out
        correctedDistance = desiredDistance;

        Vector3 position = cameraTarget.position - (rotation * Vector3.forward * correctedDistance); //update camera distance

        RaycastHit collisionHit;
        Vector3 cameraTargetPosition = new Vector3(cameraTarget.position.x, cameraTarget.position.y + cameraTargetHeight, cameraTarget.position.z);

        bool isCorrected = false;

        if (Physics.Linecast(cameraTargetPosition, position, out collisionHit))
        {
            position = collisionHit.point;
            correctedDistance = Vector3.Distance(cameraTargetPosition, position);
            isCorrected = true;
        }
        // next line is an if else statement C# like
        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomRate) : correctedDistance;

        position = cameraTarget.position - (rotation * Vector3.forward * currentDistance + new Vector3(0, -cameraTargetHeight, 0));

        transform.rotation = rotation; //rotate the object where the script is attached
        transform.position = position; //change the object position where the script is attached
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
