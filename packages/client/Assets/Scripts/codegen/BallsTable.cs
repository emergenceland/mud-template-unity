/* Autogenerated file. Manual edits will not be saved.*/

#nullable enable
using System;
using mud;
using UniRx;
using Property = System.Collections.Generic.Dictionary<string, object>;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class BallsTableUpdate : TypedRecordUpdate<Tuple<BallsTable?, BallsTable?>> { }

    public class BallsTable : IMudTable
    {
        public readonly static string ID = "Balls";
        public static RxTable BallsRxTable
        {
            get { return NetworkManager.Datastore.tableNameIndex[ID]; }
        }

        public override string GetTableId()
        {
            return ID;
        }

        public override Type TableType()
        {
            return typeof(BallsTable);
        }

        public override Type TableUpdateType()
        {
            return typeof(BallsTableUpdate);
        }

        public override bool Equals(object? obj)
        {
            BallsTable other = (BallsTable)obj;

            if (other == null)
            {
                return false;
            }
            return true;
        }

        public override void SetValues(params object[] functionParameters) { }

        public override void RecordToTable(RxRecord record)
        {
            var table = record.value;
            //bool hasValues = false;
        }

        public static Tuple<BallsTable?, BallsTable?> MapUpdates(Tuple<Property?, Property?> value)
        {
            BallsTable? current = null;
            BallsTable? previous = null;

            if (value.Item1 != null)
            {
                try
                {
                    current = new BallsTable { };
                }
                catch (InvalidCastException)
                {
                    current = new BallsTable { };
                }
            }

            if (value.Item2 != null)
            {
                try
                {
                    previous = new BallsTable { };
                }
                catch (InvalidCastException)
                {
                    previous = new BallsTable { };
                }
            }

            return new Tuple<BallsTable?, BallsTable?>(current, previous);
        }
    }
}
