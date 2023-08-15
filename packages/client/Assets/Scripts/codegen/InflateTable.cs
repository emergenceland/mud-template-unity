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
    public class InflateTableUpdate : TypedRecordUpdate<Tuple<InflateTable?, InflateTable?>> { }

    public class InflateTable : IMudTable
    {
        public readonly static TableId ID = new("", "Inflate");

        public override TableId GetTableId()
        {
            return ID;
        }

        public ulong? value;

        public override Type TableType()
        {
            return typeof(InflateTable);
        }

        public override Type TableUpdateType()
        {
            return typeof(InflateTableUpdate);
        }

        public override void SetValues(params object[] functionParameters)
        {
            value = (ulong)(int)functionParameters[0];
        }

        public override void RecordToTable(Record record)
        {
            var table = record.value;
            //bool hasValues = false;

            var valueValue = (ulong)table["value"];

            value = valueValue;
        }

        public override IMudTable RecordUpdateToTable(RecordUpdate tableUpdate)
        {
            InflateTableUpdate update = (InflateTableUpdate)tableUpdate;
            return update?.TypedValue.Item1;
        }

        public override RecordUpdate CreateTypedRecord(RecordUpdate newUpdate)
        {
            return new InflateTableUpdate
            {
                TableId = newUpdate.TableId,
                Key = newUpdate.Key,
                Value = newUpdate.Value,
                TypedValue = MapUpdates(newUpdate.Value)
            };
        }

        public static Tuple<InflateTable?, InflateTable?> MapUpdates(
            Tuple<Property?, Property?> value
        )
        {
            InflateTable? current = null;
            InflateTable? previous = null;

            if (value.Item1 != null)
            {
                try
                {
                    current = new InflateTable
                    {
                        value = value.Item1.TryGetValue("value", out var valueVal)
                            ? (ulong)valueVal
                            : default,
                    };
                }
                catch (InvalidCastException)
                {
                    current = new InflateTable { value = null, };
                }
            }

            if (value.Item2 != null)
            {
                try
                {
                    previous = new InflateTable
                    {
                        value = value.Item2.TryGetValue("value", out var valueVal)
                            ? (ulong)valueVal
                            : default,
                    };
                }
                catch (InvalidCastException)
                {
                    previous = new InflateTable { value = null, };
                }
            }

            return new Tuple<InflateTable?, InflateTable?>(current, previous);
        }
    }
}
