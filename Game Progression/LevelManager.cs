using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace GreyWolf
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        private int enemyCount = 1;
        private GameObject[] levelDoors;

        public enum doorLocation { Left, Top, Right, Down, Empty }
        public doorLocation d_locations;
        [SerializeField] float spawnOffset = 10f;

        void Start()
        {
            ScanForDoors();
        }
        private void Update()
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            SyncDoorAccessWithEnemies();
        }
        public int GetEnemyCount()
        {
            return enemyCount;
        }
        public string SpawnPosition()
        {
            switch (d_locations)
            {
                case doorLocation.Left:
                    return "Left";

                case doorLocation.Top:
                    return "Top";

                case doorLocation.Right:
                    return "Right";

                case doorLocation.Down:
                    return "Down";
            }
            return "Empty";
        }
        public void SetSpawnPosition(DoorLocation location)
        {
            switch (location.Location())
            {
                case "Left":
                    d_locations = doorLocation.Right;
                    break;

                case "Top":
                    d_locations = doorLocation.Down;
                    break;

                case "Right":
                    d_locations = doorLocation.Left;
                    break;

                case "Down":
                    d_locations = doorLocation.Top;
                    break;
            }
        }



        public void ChangeLocation(doorLocation location)
        {
            d_locations = location;
        }

        public void ChangePlayerPosition(Vector3 spawnPosition)
        {
            GameObject player = GameObject.Find("Player");

            Vector3 positionOffset = new Vector3();

            string doorPosition = d_locations.ToString();

            if (doorPosition == "Left") positionOffset.x += spawnOffset;
            if (doorPosition == "Right") positionOffset.x -= spawnOffset;
            if (doorPosition == "Top") positionOffset.z -= spawnOffset;
            if (doorPosition == "Down") positionOffset.z += spawnOffset;


            player.transform.position = spawnPosition + positionOffset;
        }

        public void ScanForDoors()
        {
            // ! adds all doors from the scene in list
            levelDoors = GameObject.FindGameObjectsWithTag("Door");
        }

        public void ClearDoorCache()
        {
            // ! Clears all doors from list            
        }

        public void SyncDoorAccessWithEnemies()
        {
            // TODO This creates infinite loop and breaks performance
            // if (enemyCount == 0)
            // {
            //     foreach (GameObject door in levelDoors)
            //     {
            //         door.GetComponent<IDoorAccessor>().OpenDoor();
            //     }
            // }
            // else
            // {
            //     foreach (GameObject door in levelDoors)
            //     {
            //         door.GetComponent<IDoorAccessor>().CloseDoor();
            //     }
            // }
        }
    }

    public class DoorLocation
    {
        private float xPosition, zPosition;

        public DoorLocation(Vector3 location)
        {
            xPosition = location.x;
            zPosition = location.z;
        }

        public string Location()
        {
            float axisOffset = 55;

            if (xPosition >= axisOffset) return "Right";
            if (xPosition <= -axisOffset) return "Left";
            if (zPosition >= axisOffset) return "Top";
            if (zPosition <= -axisOffset) return "Down";

            return "Empty";
        }

    }
}