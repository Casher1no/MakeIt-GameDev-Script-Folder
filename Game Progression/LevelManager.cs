using UnityEngine;

namespace GreyWolf
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        private int enemyCount = 1;

        public enum doorLocation { Left, Top, Right, Down, Empty }
        public doorLocation d_locations;
        [SerializeField] float spawnOffset = 10f;


        private void Update()
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
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