using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using CnControls;

[RequireComponent(typeof(Rigidbody))]
public class character_movement : MonoBehaviour
{
    private CameraController cameraShake;
    Vector3 targetPosition;
    Transform targetObject;
    Transform hitSign;
    Animator _animator;
    Animator _treeAnimator;
    Rigidbody _rigidbody;

    bool actionBool;
    bool lookAtBool;
    public static bool moving = false;
    public static bool activity = false;
    bool lumberingBool = false;
    bool shouldRotate;

    NavMeshAgent controller;
    float verticalVelocity;
    float gravity = 7.0f;
    float speed = 2.0f;

    public AudioSource choppingSound;
    public AudioSource pickAxeSound;
    public AudioSource destroyRock;
    public AudioSource fallingTree;

    public float movementSpeed;
    public float rotateSpeed;

    ParticleSystem _choppingParticles;
    public GameObject inventoryPrefab;
    public GameObject cameraPrefab;
    public GameObject PlayerManageraPrefab;



    void Start()
    {
       // Instantiate(inventoryPrefab, inventoryPrefab.transform.position, inventoryPrefab.transform.rotation);
        Instantiate(cameraPrefab, cameraPrefab.transform.position, cameraPrefab.transform.rotation);
        Instantiate(PlayerManageraPrefab, PlayerManageraPrefab.transform.position, PlayerManageraPrefab.transform.rotation);

        //cameraShake = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        controller = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        //_choppingParticles = GameObject.FindWithTag("particleSys").GetComponent<ParticleSystem>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return; //ignore clicks on UI
        }


        float moveAxis = Input.GetAxis("Vertical");
        float turnAxis = Input.GetAxis("Horizontal");
        if (moveAxis > 0)
        {
            _animator.SetBool("isWalking", true);
        }
        ApplyInput(moveAxis, turnAxis);

        /*if(CnInputManager.GetAxis("Vertical") > 0)
         {
             transform.position += transform.forward * CnInputManager.GetAxis("Vertical") * speed * Time.deltaTime;
         }
         else if(CnInputManager.GetAxis("Vertical") < 0)
         {
             transform.position -= -transform.forward * CnInputManager.GetAxis("Vertical") * speed * Time.deltaTime;
         }


         if (CnInputManager.GetAxis("Horizontal") > 0)
         {
             transform.position += transform.right * CnInputManager.GetAxis("Horizontal") * speed * Time.deltaTime;
         }
         else if (CnInputManager.GetAxis("Horizontal") < 0)
         {
             transform.position -= -transform.right * CnInputManager.GetAxis("Horizontal") * speed * Time.deltaTime;
         }
         */


        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetBool("lumbering", false);
            StopAllCoroutines();
            actionBool = true;
            goToPosition();
            shouldRotate = true;
        }
        float dist = controller.remainingDistance;
        if (dist != Mathf.Infinity && controller.pathStatus == NavMeshPathStatus.PathComplete && controller.remainingDistance == 0)
        {
            moving = false;
            _animator.SetBool("isWalking", false);
            controller.isStopped = true;

            if (targetObject && actionBool)
            {
                switch (targetObject.tag)
                {
                    case "ground":
                        Debug.Log("Complete!");
                        actionBool = false;
                        break;
                    case "lumber":
                        Debug.Log("Lumbering");
                        lookAtBool = true;
                        actionBool = false;
                        _treeAnimator = targetObject.GetComponent<Animator>();
                        StartCoroutine(hitTree(targetObject, 1.0f));
                        activity = true;
                        break;
                    case "rock":
                        Debug.Log("Rock !!");
                        lookAtBool = true;
                        actionBool = false;
                        StartCoroutine(hitRock(targetObject, 1.0f));
                        activity = true;
                        break;
                    case "cactus":
                        Debug.Log("Cactus !!");
                        lookAtBool = true;
                        actionBool = false;
                        StartCoroutine(hitCactus(targetObject, 1.0f));
                        activity = true;
                        break;
                }
            }
        }
        if (lookAtBool)
        {
            StartCoroutine(LookAtTarget(0.5f));
        }
        if (shouldRotate)
        {
            rotateOnTarget();
        }
    }

    void rotateOnTarget()
    {
        var newTargetPos = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        var distance = newTargetPos - transform.position;
        if (distance.magnitude < 0.1f)
        {
            return;
        }
        Quaternion lookOnLook = Quaternion.LookRotation(distance);
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * controller.angularSpeed);
        StartCoroutine(StopRotating());
    }

    IEnumerator StopRotating()
    {
        yield return new WaitForSeconds(0.1f);
        shouldRotate = false;
        StopCoroutine(StopRotating());
    }


    private IEnumerator LookAtTarget(float sec)
    {
        Quaternion lookOnLook = Quaternion.LookRotation(targetObject.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.fixedDeltaTime * 4);
        yield return new WaitForSeconds(sec);
        lookAtBool = false;
        targetObject = null;
    }

    void goToPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            targetPosition = hit.point;
            targetObject = hit.transform;
            controller.isStopped = false;
            controller.SetDestination(targetPosition);
            moving = true;
            _animator.SetBool("isWalking", true);
            createHitSign();
        }
    }

    private IEnumerator hitTree(Transform tree, float sec)
    {
        int treeHealth = tree.GetComponent<tree_script>().treeHealth;
        if (treeHealth > 0)
        {
            tree.GetComponent<tree_script>().treeHealth--;
            choppingSound.Play();
            //_choppingParticles.Play();
            _animator.SetBool("lumbering", true);
            _treeAnimator.SetTrigger("hit");
            Debug.Log(treeHealth);
            yield return new WaitForSeconds(sec);
            StartCoroutine(hitTree(tree, 1.0f));
        }
        else
        {
            fallingTree.Play();
            _animator.SetBool("lumbering", false);
            tree.GetComponent<tree_script>().isDead = true;
            StopCoroutine("hitTree");
            activity = false;
        }
    }

    private IEnumerator hitRock(Transform tree, float sec)
    {
        int rockHealth = tree.GetComponent<rock_script>().rockHealth;
        if (rockHealth > 0)
        {
            tree.GetComponent<rock_script>().rockHealth--;
            pickAxeSound.Play();
            //_choppingParticles.Play();
            _animator.SetBool("lumbering", true);
            Debug.Log(rockHealth);
            yield return new WaitForSeconds(sec);
            StartCoroutine(hitRock(tree, 1.0f));
        }
        else
        {
            destroyRock.Play();
            _animator.SetBool("lumbering", false);
            tree.GetComponent<rock_script>().isDead = true;
            StopCoroutine("hitRock");
            activity = false;
        }
    }

    private IEnumerator hitCactus(Transform cactus, float sec)
    {
        int cactusHealth = cactus.GetComponent<cactus_script>().cactusHealth;
        if (cactusHealth > 0)
        {
            cactus.GetComponent<cactus_script>().cactusHealth--;
            choppingSound.Play();
            //_choppingParticles.Play();
            _animator.SetBool("lumbering", true);
            Debug.Log(cactusHealth);
            yield return new WaitForSeconds(sec);
            StartCoroutine(hitCactus(cactus, 1.0f));
            Debug.Log(activity);

        }
        else
        {
            fallingTree.Play();
            _animator.SetBool("lumbering", false);
            cactus.GetComponent<cactus_script>().isDead = true;
            StopCoroutine("hitRock");
            activity = false;
        }
    }

    void createHitSign()
    {
        if (hitSign) Destroy(hitSign.gameObject);
        hitSign = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
        hitSign.transform.position = targetPosition;
        hitSign.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Material mat = hitSign.GetComponent<Renderer>().material;
        mat.color = Color.red;
        Collider col = hitSign.GetComponent<SphereCollider>();
        col.isTrigger = true;
    }

    //Character_Controller

    private void ApplyInput(float moveInput, float turnInput)
    {
        Move(moveInput);
        Turn(turnInput);
    }

    private void Move(float input)
    {
        transform.Translate(Vector3.forward * input * movementSpeed * Time.deltaTime);
    }

    private void Turn(float input)
    {
        transform.Rotate(0, input * rotateSpeed * Time.deltaTime, 0);
    }
}