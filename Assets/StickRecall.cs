using UnityEngine;
using System.Collections;

public class StickRecall : MonoBehaviour
{

    public GameObject prefab;
    public Rigidbody attachPoint;

    SteamVR_TrackedObject trackedObj;
    FixedJoint joint;

    //Quaternion temp;


    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {

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
                rigidbody.velocity = origin.TransformVector(device.velocity*10);
                rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity*10);
            }
            else
            {
                rigidbody.velocity = device.velocity*10;
                rigidbody.angularVelocity = device.angularVelocity*10;
            }

            rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude*10;
        }
    }
}
