using UnityEngine;
using UnityEngine.SceneManagement;

namespace GreyWolf
{
    public class SceneTransfer : MonoBehaviour
    {

        [SerializeField] int sceneNumber;
        ServiceLocator _service;
        GameObject player;

        // TODO Create functionality that checks for isFreshSpawned : if true => spawn in door locations, if false => spawn in center of main level
        // ? if false, then don't spawn player in door locations
        bool isFreshSpawned = false;

        public enum Position { Left, Top, Right, Down, Empty }
        public Position positionInScene;

        private void Start()
        {
            _service = FindObjectOfType<ServiceLocator>();

            SetUpPlayer();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                DoorLocation doorLocation = new DoorLocation(transform.position);
                _service.levelManager.SetSpawnPosition(doorLocation);
                print(doorLocation.Location());

                //TODO Scene will be loaded from Main Level Manager that randomises levels.
                // * Depending on direction passed, manager will direct player to next level.
                
                //SceneManager.LoadScene(sceneNumber);
            }
        }

        private void SetUpPlayer()
        {
            var locations = FindObjectsOfType<SceneTransfer>();
            foreach (var location in locations)
            {
                if (location.positionString() == _service.levelManager.SpawnPosition())
                {
                    _service.levelManager.ChangePlayerPosition(location.transform.position);
                }       
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + new Vector3(0, 1, 0), new Vector3(2, 2, 2));
        }

        public string positionString()
        {
            return positionInScene.ToString();
        }
    }
}
