using UnityEngine;
using System.Collections;

public class StickRecall : MonoBehaviour
{

    public GameObject prefab;
    public Rigidbody attachPoint;
    private Quaternion original;

    SteamVR_TrackedObject trackedObj;
    FixedJoint joint;

    private Collider collisionDetector;
    private MeshRenderer rend;

    public rollBall mainScript;



    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        collisionDetector = prefab.GetComponent<Collider>();
        rend = prefab.transform.GetChild(0).GetComponent<MeshRenderer>();
        original = prefab.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            //prefab.transform.rotation = original;
            mainScript.setMoveChoice(1);
            rend.enabled = true;
            collisionDetector.enabled = true;

            prefab.transform.position = attachPoint.transform.position;

            joint = prefab.AddComponent<FixedJoint>();

            var go = joint.gameObject;
            var rigidbody = go.GetComponent<Rigidbody>();

            rigidbody.constraints = RigidbodyConstraints.None;
            prefab.transform.forward = gameObject.transform.forward;
            prefab.transform.Rotate(new Vector3(0.0f, 270.0f, 270.0f));
            joint.connectedBody = attachPoint;

        }
        else if (joint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            var go = joint.gameObject;
            var rigidbody = go.GetComponent<Rigidbody>();
            Object.DestroyImmediate(joint);
            joint = null;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            // We should probably apply the offset between trackedObj.transform.position
            // and device.transform.pos to insert into the physics sim at the correct
            // location, however, we would then want to predict ahead the visual representation
            // by the same amount we are predicting our render poses.

            var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
            if (origin != null)
            {
                rigidbody.velocity = origin.TransformVector(device.velocity*25);
                rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity*25);
            }
            else
            {
                rigidbody.velocity = device.velocity*25;
                rigidbody.angularVelocity = device.angularVelocity*25;
            }

            rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude*25;
            mainScript.setMoveChoice(0);
        }
    }
}
