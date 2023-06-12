using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;

namespace WS3
{
    public class PlayerNetwork : MonoBehaviourPunCallbacks
    {
        public static GameObject UserMeInstance;

        public Material PlayerLocalMat;
        /// <summary>
        /// Represents the GameObject on which to change the color for the local player
        /// </summary>
        public GameObject GameObjectLocalPlayerColor;

        /// <summary>
        /// The FreeLookCameraRig GameObject to configure for the UserMe
        /// </summary>
        GameObject goFreeLookCameraRig = null;


        #region Snwoball Spawn
        /// <summary>
        /// The Transform from which the snow ball is spawned
        /// </summary>
        [SerializeField] Transform snowballSpawner;
        /// <summary>
        /// The prefab to create when spawning
        /// </summary>
        [SerializeField] GameObject SnowballPrefab;



        // Use to configure the throw ball feature
        [Range(0.2f, 100.0f)] public float MinSpeed;
        [Range(0.2f, 100.0f)] public float MaxSpeed;
        [Range(0.2f, 100.0f)] public float MaxSpeedForPressDuration;
        private float pressDuration = 0;
        Equipment equipment;

        #endregion

        void Awake()
        {
            if (photonView.IsMine)
            {
                Debug.LogFormat("Avatar UserMe created for userId {0}", photonView.ViewID);
                UserMeInstance = gameObject;

            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            equipment = GetComponent<Equipment>();
            Debug.Log("isLocalPlayer:" + photonView.IsMine);
            updateGoFreeLookCameraRig();
            followLocalPlayer();
            activateLocalPlayer();
        }

        /// <summary>
        /// Get the GameObject of the CameraRig
        /// </summary>
        protected void updateGoFreeLookCameraRig()
        {
            if (!photonView.IsMine) return;
            try
            {
                // Get the Camera to set as the followed camera
                goFreeLookCameraRig = transform.Find("/FreeLookCameraRig").gameObject;
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Warning, no goFreeLookCameraRig found\n" + ex);
            }
        }

        /// <summary>
        /// Make the CameraRig following the LocalPlayer only.
        /// </summary>
        protected void followLocalPlayer()
        {
            if (photonView.IsMine)
            {
                if (goFreeLookCameraRig != null)
                {
                    // find Avatar EthanHips
                    Transform transformFollow = transform.Find("EthanSkeleton/EthanHips") != null ? transform.Find("EthanSkeleton/EthanHips") : transform;
                    // call the SetTarget on the FreeLookCam attached to the FreeLookCameraRig
                    goFreeLookCameraRig.GetComponent<FreeLookCam>().SetTarget(transformFollow);
                    Debug.Log("ThirdPersonControllerMultiuser follow:" + transformFollow);
                }
            }
        }

        protected void activateLocalPlayer()
        {
            // enable the ThirdPersonUserControl if it is a Loacl player = UserMe
            // disable the ThirdPersonUserControl if it is not a Loacl player = UserOther
            GetComponent<ThirdPersonUserControl>().enabled = photonView.IsMine;
            GetComponent<Rigidbody>().isKinematic = !photonView.IsMine;
            if (photonView.IsMine)
            {
                try
                {
                    // Change the material of the Ethan Glasses
                    GameObjectLocalPlayerColor.GetComponent<Renderer>().material = PlayerLocalMat;
                }
                catch (System.Exception)
                {

                }
            }
        }


        #region Snwoball Spawn
        // Update is called once per frame
        void Update()
        {
            // Don't do anything if we are not the UserMe isLocalPlayer
            //...
            if (!photonView.IsMine)
                return;
            if (Input.GetButtonDown("Fire1"))
            {
                // Start Loading time when fire is pressed
                pressDuration = 0.0f;
            }
            else if (Input.GetButton("Fire1"))
            {
                // count the time the Fire1 is pressed
                pressDuration += Time.deltaTime; 
                //...
            }

            else if (Input.GetButtonUp("Fire1"))
            {
            if (equipment.amount <= 0)
                    return;
                // When releasing Fire1, spawn the ball
                // Define the initial speed of the Snowball between MinSpeed and MaxSpeed according to the duration the button is pressed
                var speed = MinSpeed +(pressDuration / MaxSpeedForPressDuration) * (MaxSpeed - MinSpeed); //... update with the right value
                Debug.Log(string.Format("time {0:F2} <  {1} => speed {2} < {3} < {4}", pressDuration, MaxSpeedForPressDuration, MinSpeed, speed, MaxSpeed));
                photonView.RPC("ThrowBall", RpcTarget.AllViaServer, snowballSpawner.position, speed * Camera.main.transform.forward, Camera.main.transform.rotation);
                equipment.Fire();

            }
        }

        [PunRPC]
        void ThrowBall(Vector3 position, Vector3 directionAndSpeed,  Quaternion rotation, PhotonMessageInfo info)
        {

            // Tips for Photon lag compensation. Il faut compenser le temps de lag pour l'envoi du message.
            // donc décaler la position de départ de la balle dans la direction
            float lag = (float)(PhotonNetwork.Time - info.SentServerTime);

            // Instantiate the Snowball from the Snowball Prefab at the position of the Spawner
            //...
            GameObject snowball = Instantiate(SnowballPrefab, position, rotation);

            // Set velocity to the snowballRigidBody direction and speed
            //...
            snowball.GetComponent<Rigidbody>().velocity = directionAndSpeed;

            // Instantiate the snow ball
            //...

            // Destroy the Snowball after 5 seconds
            //...
            Destroy(snowball, 5f);
        }
        #endregion

    }
}
