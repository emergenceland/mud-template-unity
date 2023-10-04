using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace mud {

    public class MUDTableObject : ScriptableObject {
        public string TableName { get { return tableName; } }
        public string TableUpdateName { get { return tableUpdateName; } }
        public GameObject DefaultPrefab {get {return defaultPrefab;}}
        public Type Table { get { return tableType; } }

        [Header("Table")]
        [SerializeField] string tableName;
        [SerializeField] string tableUpdateName;
        [SerializeField] GameObject defaultPrefab;
        [SerializeField] Type tableType;
        [SerializeField] IMudTable table;

        public void SetTable(Type newtable) {

            table = (IMudTable)System.Activator.CreateInstance(newtable);
            
            tableType = table.GetType();
            tableName = tableType.FullName;

            Debug.Log(tableType.ToString());

        }
    }

}
