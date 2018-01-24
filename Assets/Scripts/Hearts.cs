using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Hearts : MonoBehaviour
    {
        public enum charType
        {
            Player,
            Enemy
        }

        public charType CharacterType;
        private Enemy enemy;
        private PlayerController player;

        public Transform healthHook;

        public GameObject heart;

        public List<GameObject> heartsList = new List<GameObject>();

        // Use this for initialization
        void Start()
        {
            if (CharacterType == charType.Player)
            {
                player = GetComponent<PlayerController>();
                SpawnHearts(player.health);
            }
            else
            {
                enemy = GetComponent<Enemy>();
                SpawnHearts(enemy.health);
            }
        }

        //todo probs convert/round up ayy
        void SpawnHearts(float health)
        {
            for (int i = 0; i < health; i++)
            {
                GameObject go = Instantiate(heart,
                    new Vector3(healthHook.position.x + (-i / 2), healthHook.position.y, healthHook.position.z),
                    Quaternion.identity);
                go.name = "Heart";

                go.transform.parent = healthHook;
                heartsList.Add(go);
            }
        }

        void RemoveHearts(float health)
        {
            //simply take away hearts from last entry in list to first based on dmg

            foreach (var heart in heartsList)
            {
                //todo make hearts take dmg.. 
            }
        }
    }
}