/* Autogenerated file. Manual edits will not be saved.*/

#nullable enable
using System;
using mud.Client;
using mud.Network.schemas;
using mud.Unity;
using UniRx;
using Property = System.Collections.Generic.Dictionary<string, object>;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class BallsTableUpdate : TypedRecordUpdate<Tuple<BallsTable?, BallsTable?>> { }

    public class BallsTable : IMudTable
    {
        public readonly static TableId ID = new("", "Balls");

        public override TableId GetTableId()
        {
            return ID;
        }

        public long? count;

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
            if (count != other.count)
            {
                return false;
            }
            return true;
        }

        public override void SetValues(params object[] functionParameters)
        {
            count = (long)(int)functionParameters[0];
        }

        public override void RecordToTable(Record record)
        {
            var table = record.value;
            //bool hasValues = false;

            var countValue = (long)table["count"];
            count = countValue;
        }

        public override IMudTable RecordUpdateToTable(RecordUpdate tableUpdate)
        {
            BallsTableUpdate update = (BallsTableUpdate)tableUpdate;
            return update?.TypedValue.Item1;
        }

        public override RecordUpdate CreateTypedRecord(RecordUpdate newUpdate)
        {
            return new BallsTableUpdate
            {
                TableId = newUpdate.TableId,
                Key = newUpdate.Key,
                Value = newUpdate.Value,
                TypedValue = MapUpdates(newUpdate.Value)
            };
        }

        public static Tuple<BallsTable?, BallsTable?> MapUpdates(Tuple<Property?, Property?> value)
        {
            BallsTable? current = null;
            BallsTable? previous = null;

            if (value.Item1 != null)
            {
                try
                {
                    current = new BallsTable
                    {
                        count = value.Item1.TryGetValue("count", out var countVal)
                            ? (long)countVal
                            : default,
                    };
                }
                catch (InvalidCastException)
                {
                    current = new BallsTable { count = null, };
                }
            }

            if (value.Item2 != null)
            {
                try
                {
                    previous = new BallsTable
                    {
                        count = value.Item2.TryGetValue("count", out var countVal)
                            ? (long)countVal
                            : default,
                    };
                }
                catch (InvalidCastException)
                {
                    previous = new BallsTable { count = null, };
                }
            }

            return new Tuple<BallsTable?, BallsTable?>(current, previous);
        }
    }
}