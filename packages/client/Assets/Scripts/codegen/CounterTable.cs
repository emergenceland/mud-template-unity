/* Autogenerated file. Manual edits will not be saved.*/

#nullable enable
using System;
using mud.Client;
using mud.Network.schemas;
using mud.Unity;
using UniRx;
using Property = System.Collections.Generic.Dictionary<string, object>;

namespace DefaultNamespace
{
    public class CounterTableUpdate : TypedRecordUpdate<Tuple<CounterTable?, CounterTable?>> { }

    public class CounterTable : IMudTable
    {
        public static readonly TableId TableId = new("", "Counter");

        public ulong? value;

        public static CounterTable? GetTableValue(string key)
        {
            var query = new Query().In(TableId);
            var result = NetworkManager.Instance.ds.RunQuery(query);
            var counterTable = new CounterTable();
            var hasValues = false;

            foreach (var record in result)
            {
                var v = record.value["value"];

                switch (record.key)
                {
                    case "value":
                        var valueValue = (ulong)v;
                        counterTable.value = valueValue;
                        hasValues = true;
                        break;
                }
            }

            return hasValues ? counterTable : null;
        }

        public static IObservable<CounterTableUpdate> OnRecordUpdate()
        {
            return NetworkManager.Instance.ds.OnDataStoreUpdate
                .Where(
                    update =>
                        update.TableId == TableId.ToString() && update.Type == UpdateType.SetField
                )
                .Select(
                    update =>
                        new CounterTableUpdate
                        {
                            TableId = update.TableId,
                            Key = update.Key,
                            Value = update.Value,
                            TypedValue = MapUpdates(update.Value)
                        }
                );
        }

        public static IObservable<CounterTableUpdate> OnRecordInsert()
        {
            return NetworkManager.Instance.ds.OnDataStoreUpdate
                .Where(
                    update =>
                        update.TableId == TableId.ToString() && update.Type == UpdateType.SetRecord
                )
                .Select(
                    update =>
                        new CounterTableUpdate
                        {
                            TableId = update.TableId,
                            Key = update.Key,
                            Value = update.Value,
                            TypedValue = MapUpdates(update.Value)
                        }
                );
        }

        public static IObservable<CounterTableUpdate> OnRecordDelete()
        {
            return NetworkManager.Instance.ds.OnDataStoreUpdate
                .Where(
                    update =>
                        update.TableId == TableId.ToString()
                        && update.Type == UpdateType.DeleteRecord
                )
                .Select(
                    update =>
                        new CounterTableUpdate
                        {
                            TableId = update.TableId,
                            Key = update.Key,
                            Value = update.Value,
                            TypedValue = MapUpdates(update.Value)
                        }
                );
        }

        public static Tuple<CounterTable?, CounterTable?> MapUpdates(
            Tuple<Property?, Property?> value
        )
        {
            CounterTable? current = null;
            CounterTable? previous = null;

            if (value.Item1 != null)
            {
                try
                {
                    current = new CounterTable
                    {
                        value = value.Item1.TryGetValue("value", out var valueVal)
                            ? (ulong)valueVal
                            : default,
                    };
                }
                catch (InvalidCastException)
                {
                    current = new CounterTable { value = null, };
                }
            }

            if (value.Item2 != null)
            {
                try
                {
                    previous = new CounterTable
                    {
                        value = value.Item2.TryGetValue("value", out var valueVal)
                            ? (ulong)valueVal
                            : default,
                    };
                }
                catch (InvalidCastException)
                {
                    previous = new CounterTable { value = null, };
                }
            }

            return new Tuple<CounterTable?, CounterTable?>(current, previous);
        }
    }
}
