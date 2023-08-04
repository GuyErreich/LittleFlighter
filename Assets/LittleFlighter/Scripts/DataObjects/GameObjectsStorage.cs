using System.Collections.Generic;
using UnityEngine;

namespace LittleFlighter.DataObjects
{
    [CreateAssetMenu(fileName = "New Dictionary Storage", menuName = "Data Objects/Dictionary/Game Objects")]
    public class GameObjectsStorage : ScriptableObject
    {
        [SerializeField] private List<string> keys;
        [SerializeField] private List<GameObject> values;

        public List<string> Keys { get => this.keys; }
        public List<GameObject> Values { get => this.values; }
    }
}