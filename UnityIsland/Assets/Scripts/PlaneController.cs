using UnityEngine;

namespace UnityIsland
{

    public class PlaneController : Photon.MonoBehaviour
    {
        public GameObject m_projectilePrefab;
        //private GameObject m_projectileSpawnPoint;
        public float m_speed = 50;
        public float m_projectileSpeed = 500;
        public float m_rotationSpeed = 40;
        public float m_acceleration = 50;
        public float m_holdHorizontal = 0;
        public float m_holdVertical = 0;

        private void Awake()
        {
            //m_projectileSpawnPoint = this.gameObject..transform.FindChild("ProjectileSpawnPoint");
            
        }

        void Update()
        {
            ControlPlane();
        }

        public void FireProjectile()
        {
            var spawnTransform = this.gameObject.transform.Find("ProjectileSpawnPoint");
            var projectile = Instantiate(m_projectilePrefab);
            projectile.transform.position = spawnTransform.position;
            projectile.transform.rotation = spawnTransform.rotation;
            projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * (m_speed + m_projectileSpeed);

            GameObject.Destroy(projectile, 3);
        }

        private void ControlPlane()
        {
            if (this.photonView.isMine || !PhotonNetwork.connected)
            {
                var t = this.gameObject.transform;

                if (Input.GetButton("Fire1"))
                {
                    this.FireProjectile();
                }

                if (Input.GetKeyDown(KeyCode.Keypad6)) m_holdHorizontal = +1;
                if (Input.GetKeyDown(KeyCode.Keypad4)) m_holdHorizontal = -1;
                if (Input.GetKeyDown(KeyCode.Keypad8)) m_holdVertical = +1;
                if (Input.GetKeyDown(KeyCode.Keypad2)) m_holdVertical = -1;
                if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    m_holdVertical = m_holdHorizontal = 0;
                }


                var rotation = new Vector3();
                rotation.x = (m_holdVertical + Input.GetAxis("Vertical")) * m_rotationSpeed * Time.deltaTime;
                rotation.y = (m_holdHorizontal + Input.GetAxis("Horizontal")) * m_rotationSpeed * Time.deltaTime;
                //rotation.z = Input.GetAxis("Axe3") * m_rotationSpeed * Time.deltaTime;
                t.Rotate(rotation);


                m_speed += Input.GetAxis("Throttle") * m_acceleration * Time.deltaTime;
                m_speed = m_speed < 0 ? 0 : m_speed;
                var speedVector = t.forward * m_speed;
                t.position += speedVector * Time.deltaTime;

                this.gameObject.GetComponent<PhotonTransformView>().SetSynchronizedValues(speedVector, rotation.y / Time.deltaTime);
            }
        }
    }
}