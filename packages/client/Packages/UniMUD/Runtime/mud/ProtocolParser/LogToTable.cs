using System;
using System.Collections.Generic;
using System.Linq;
using mud.IStore.ContractDefinition;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.Model;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using UnityEngine;
using static mud.Common;

namespace mud
{
    public partial class ProtocolParser
    {
        public static bool IsTableRegistrationLog(FilterLog log)
        {
            var storeConfig = MudDefinitions.DefineStoreConfig(null); 
            var schemasTable = storeConfig["Tables"];
            var schemasTableId = ResourceIDToHex(new ResourceID
            {
                Type = schemasTable.OffchainOnly != null ? ResourceType.OffchainTable : ResourceType.Table,
                Namespace = schemasTable.Namespace,
                Name = schemasTable.Name
            });
            var storeSetRecordSignature =
                new StoreSetRecordEventDTO().GetEventABI().Sha3Signature;
            if (!log.IsLogForEvent(storeSetRecordSignature)) return false;
            var decoded = Event<StoreSetRecordEventDTO>.DecodeEvent(log);
            var tableId = BytesToHex(decoded.Event.TableId, 32);
            return string.Equals(tableId, schemasTableId, StringComparison.CurrentCultureIgnoreCase);
        }

        public static readonly Dictionary<string, SchemaAbiTypes.SchemaType> RegisterValueSchema = new()
        {
            { "fieldLayout", SchemaAbiTypes.SchemaType.BYTES32 },
            { "keySchema", SchemaAbiTypes.SchemaType.BYTES32 },
            { "valueSchema", SchemaAbiTypes.SchemaType.BYTES32 },
            { "abiEncodedKeyNames", SchemaAbiTypes.SchemaType.BYTES },
            { "abiEncodedFieldNames", SchemaAbiTypes.SchemaType.BYTES }
        };

        public static Table LogToTable(FilterLog log)
        {
            var decoded = Event<StoreSetRecordEventDTO>.DecodeEvent(log);
            var keyTuple = decoded.Event.KeyTuple.Select(key => BytesToHex(key)).ToList();
            if (keyTuple.Count > 1)
                Debug.LogWarning(
                    "registerSchema event is expected to have only one key in key tuple, but got multiple.");

            var table = HexToResourceId(keyTuple[0]);

            var staticData = BytesToHex(decoded.Event.StaticData);
            var encodedLengths = BytesToHex(decoded.Event.EncodedLengths);
            var dynamicData = BytesToHex(decoded.Event.DynamicData);

            var data = ConcatHex(new[] { staticData, encodedLengths, dynamicData });
            var value = DecodeValue(RegisterValueSchema, data);

            var keySchema = HexToSchema(value["keySchema"].ToString());
            var valueSchema = HexToSchema(value["valueSchema"].ToString());

            var paramDecoder = new ParameterDecoder();
            var decodedKeyNames = paramDecoder.DecodeDefaultData(value["abiEncodedKeyNames"].ToString(),
                new Parameter("string[]", "abiEncodedKeyNames", 0));
            var keyNames = decodedKeyNames[0].Result as List<string>;


            var decodedFieldNames = paramDecoder.DecodeDefaultData(value["abiEncodedFieldNames"].ToString(),
                new Parameter("string[]", "abiEncodedFieldNames", 0));
            var fieldNames = decodedFieldNames[0].Result as List<string>;

            var valueAbiTypes = valueSchema.StaticFields.Concat(valueSchema.DynamicFields).ToList();

            var tableKeySchema = keySchema.StaticFields
                .Select((schemaType, index) => new { Key = keyNames[index], Value = schemaType })
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            var tableValueSchema = valueAbiTypes
                .Select((schemaType, index) => new { Key = fieldNames[index], Value = schemaType })
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            return new Table
            {
                Address = log.Address,
                TableId = keyTuple[0],
                Namespace = FormatGetRecordResult(table.Namespace)[0],
                Name = FormatGetRecordResult(table.Name)[0],
                KeySchema = tableKeySchema,
                ValueSchema = tableValueSchema,
            };
        }
    }
}
