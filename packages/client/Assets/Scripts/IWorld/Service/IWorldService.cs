using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using IWorld.ContractDefinition;

namespace IWorld.Service
{
    public partial class IWorldService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, IWorldDeployment iWorldDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<IWorldDeployment>().SendRequestAndWaitForReceiptAsync(iWorldDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, IWorldDeployment iWorldDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<IWorldDeployment>().SendRequestAsync(iWorldDeployment);
        }

        public static async Task<IWorldService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, IWorldDeployment iWorldDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, iWorldDeployment, cancellationTokenSource);
            return new IWorldService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public IWorldService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public IWorldService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AttackRequestAsync(AttackFunction attackFunction)
        {
             return ContractHandler.SendRequestAsync(attackFunction);
        }

        public Task<TransactionReceipt> AttackRequestAndWaitForReceiptAsync(AttackFunction attackFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(attackFunction, cancellationToken);
        }

        public Task<string> AttackRequestAsync(int x, int y)
        {
            var attackFunction = new AttackFunction();
                attackFunction.X = x;
                attackFunction.Y = y;
            
             return ContractHandler.SendRequestAsync(attackFunction);
        }

        public Task<TransactionReceipt> AttackRequestAndWaitForReceiptAsync(int x, int y, CancellationTokenSource cancellationToken = null)
        {
            var attackFunction = new AttackFunction();
                attackFunction.X = x;
                attackFunction.Y = y;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(attackFunction, cancellationToken);
        }

        public Task<string> CallRequestAsync(CallFunction callFunction)
        {
             return ContractHandler.SendRequestAsync(callFunction);
        }

        public Task<TransactionReceipt> CallRequestAndWaitForReceiptAsync(CallFunction callFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(callFunction, cancellationToken);
        }

        public Task<string> CallRequestAsync(byte[] @namespace, byte[] name, byte[] funcSelectorAndArgs)
        {
            var callFunction = new CallFunction();
                callFunction.Namespace = @namespace;
                callFunction.Name = name;
                callFunction.FuncSelectorAndArgs = funcSelectorAndArgs;
            
             return ContractHandler.SendRequestAsync(callFunction);
        }

        public Task<TransactionReceipt> CallRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, byte[] funcSelectorAndArgs, CancellationTokenSource cancellationToken = null)
        {
            var callFunction = new CallFunction();
                callFunction.Namespace = @namespace;
                callFunction.Name = name;
                callFunction.FuncSelectorAndArgs = funcSelectorAndArgs;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(callFunction, cancellationToken);
        }

        public Task<string> DeleteRecordRequestAsync(DeleteRecordFunction deleteRecordFunction)
        {
             return ContractHandler.SendRequestAsync(deleteRecordFunction);
        }

        public Task<TransactionReceipt> DeleteRecordRequestAndWaitForReceiptAsync(DeleteRecordFunction deleteRecordFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(deleteRecordFunction, cancellationToken);
        }

        public Task<string> DeleteRecordRequestAsync(byte[] table, List<byte[]> key)
        {
            var deleteRecordFunction = new DeleteRecordFunction();
                deleteRecordFunction.Table = table;
                deleteRecordFunction.Key = key;
            
             return ContractHandler.SendRequestAsync(deleteRecordFunction);
        }

        public Task<TransactionReceipt> DeleteRecordRequestAndWaitForReceiptAsync(byte[] table, List<byte[]> key, CancellationTokenSource cancellationToken = null)
        {
            var deleteRecordFunction = new DeleteRecordFunction();
                deleteRecordFunction.Table = table;
                deleteRecordFunction.Key = key;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(deleteRecordFunction, cancellationToken);
        }

        public Task<string> DeleteRecordRequestAsync(DeleteRecord1Function deleteRecord1Function)
        {
             return ContractHandler.SendRequestAsync(deleteRecord1Function);
        }

        public Task<TransactionReceipt> DeleteRecordRequestAndWaitForReceiptAsync(DeleteRecord1Function deleteRecord1Function, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(deleteRecord1Function, cancellationToken);
        }

        public Task<string> DeleteRecordRequestAsync(byte[] @namespace, byte[] name, List<byte[]> key)
        {
            var deleteRecord1Function = new DeleteRecord1Function();
                deleteRecord1Function.Namespace = @namespace;
                deleteRecord1Function.Name = name;
                deleteRecord1Function.Key = key;
            
             return ContractHandler.SendRequestAsync(deleteRecord1Function);
        }

        public Task<TransactionReceipt> DeleteRecordRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, List<byte[]> key, CancellationTokenSource cancellationToken = null)
        {
            var deleteRecord1Function = new DeleteRecord1Function();
                deleteRecord1Function.Namespace = @namespace;
                deleteRecord1Function.Name = name;
                deleteRecord1Function.Key = key;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(deleteRecord1Function, cancellationToken);
        }

        public Task<string> EmitEphemeralRecordRequestAsync(EmitEphemeralRecord1Function emitEphemeralRecord1Function)
        {
             return ContractHandler.SendRequestAsync(emitEphemeralRecord1Function);
        }

        public Task<TransactionReceipt> EmitEphemeralRecordRequestAndWaitForReceiptAsync(EmitEphemeralRecord1Function emitEphemeralRecord1Function, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(emitEphemeralRecord1Function, cancellationToken);
        }

        public Task<string> EmitEphemeralRecordRequestAsync(byte[] @namespace, byte[] name, List<byte[]> key, byte[] data)
        {
            var emitEphemeralRecord1Function = new EmitEphemeralRecord1Function();
                emitEphemeralRecord1Function.Namespace = @namespace;
                emitEphemeralRecord1Function.Name = name;
                emitEphemeralRecord1Function.Key = key;
                emitEphemeralRecord1Function.Data = data;
            
             return ContractHandler.SendRequestAsync(emitEphemeralRecord1Function);
        }

        public Task<TransactionReceipt> EmitEphemeralRecordRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, List<byte[]> key, byte[] data, CancellationTokenSource cancellationToken = null)
        {
            var emitEphemeralRecord1Function = new EmitEphemeralRecord1Function();
                emitEphemeralRecord1Function.Namespace = @namespace;
                emitEphemeralRecord1Function.Name = name;
                emitEphemeralRecord1Function.Key = key;
                emitEphemeralRecord1Function.Data = data;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(emitEphemeralRecord1Function, cancellationToken);
        }

        public Task<string> EmitEphemeralRecordRequestAsync(EmitEphemeralRecordFunction emitEphemeralRecordFunction)
        {
             return ContractHandler.SendRequestAsync(emitEphemeralRecordFunction);
        }

        public Task<TransactionReceipt> EmitEphemeralRecordRequestAndWaitForReceiptAsync(EmitEphemeralRecordFunction emitEphemeralRecordFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(emitEphemeralRecordFunction, cancellationToken);
        }

        public Task<string> EmitEphemeralRecordRequestAsync(byte[] table, List<byte[]> key, byte[] data)
        {
            var emitEphemeralRecordFunction = new EmitEphemeralRecordFunction();
                emitEphemeralRecordFunction.Table = table;
                emitEphemeralRecordFunction.Key = key;
                emitEphemeralRecordFunction.Data = data;
            
             return ContractHandler.SendRequestAsync(emitEphemeralRecordFunction);
        }

        public Task<TransactionReceipt> EmitEphemeralRecordRequestAndWaitForReceiptAsync(byte[] table, List<byte[]> key, byte[] data, CancellationTokenSource cancellationToken = null)
        {
            var emitEphemeralRecordFunction = new EmitEphemeralRecordFunction();
                emitEphemeralRecordFunction.Table = table;
                emitEphemeralRecordFunction.Key = key;
                emitEphemeralRecordFunction.Data = data;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(emitEphemeralRecordFunction, cancellationToken);
        }

        public Task<byte[]> GetFieldQueryAsync(GetFieldFunction getFieldFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetFieldFunction, byte[]>(getFieldFunction, blockParameter);
        }

        
        public Task<byte[]> GetFieldQueryAsync(byte[] table, List<byte[]> key, byte schemaIndex, BlockParameter blockParameter = null)
        {
            var getFieldFunction = new GetFieldFunction();
                getFieldFunction.Table = table;
                getFieldFunction.Key = key;
                getFieldFunction.SchemaIndex = schemaIndex;
            
            return ContractHandler.QueryAsync<GetFieldFunction, byte[]>(getFieldFunction, blockParameter);
        }

        public Task<BigInteger> GetFieldLengthQueryAsync(GetFieldLengthFunction getFieldLengthFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetFieldLengthFunction, BigInteger>(getFieldLengthFunction, blockParameter);
        }

        
        public Task<BigInteger> GetFieldLengthQueryAsync(byte[] table, List<byte[]> key, byte schemaIndex, byte[] schema, BlockParameter blockParameter = null)
        {
            var getFieldLengthFunction = new GetFieldLengthFunction();
                getFieldLengthFunction.Table = table;
                getFieldLengthFunction.Key = key;
                getFieldLengthFunction.SchemaIndex = schemaIndex;
                getFieldLengthFunction.Schema = schema;
            
            return ContractHandler.QueryAsync<GetFieldLengthFunction, BigInteger>(getFieldLengthFunction, blockParameter);
        }

        public Task<byte[]> GetFieldSliceQueryAsync(GetFieldSliceFunction getFieldSliceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetFieldSliceFunction, byte[]>(getFieldSliceFunction, blockParameter);
        }

        
        public Task<byte[]> GetFieldSliceQueryAsync(byte[] table, List<byte[]> key, byte schemaIndex, byte[] schema, BigInteger start, BigInteger end, BlockParameter blockParameter = null)
        {
            var getFieldSliceFunction = new GetFieldSliceFunction();
                getFieldSliceFunction.Table = table;
                getFieldSliceFunction.Key = key;
                getFieldSliceFunction.SchemaIndex = schemaIndex;
                getFieldSliceFunction.Schema = schema;
                getFieldSliceFunction.Start = start;
                getFieldSliceFunction.End = end;
            
            return ContractHandler.QueryAsync<GetFieldSliceFunction, byte[]>(getFieldSliceFunction, blockParameter);
        }

        public Task<byte[]> GetKeySchemaQueryAsync(GetKeySchemaFunction getKeySchemaFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetKeySchemaFunction, byte[]>(getKeySchemaFunction, blockParameter);
        }

        
        public Task<byte[]> GetKeySchemaQueryAsync(byte[] table, BlockParameter blockParameter = null)
        {
            var getKeySchemaFunction = new GetKeySchemaFunction();
                getKeySchemaFunction.Table = table;
            
            return ContractHandler.QueryAsync<GetKeySchemaFunction, byte[]>(getKeySchemaFunction, blockParameter);
        }

        public Task<byte[]> GetRecordQueryAsync(GetRecord1Function getRecord1Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetRecord1Function, byte[]>(getRecord1Function, blockParameter);
        }

        
        public Task<byte[]> GetRecordQueryAsync(byte[] table, List<byte[]> key, byte[] schema, BlockParameter blockParameter = null)
        {
            var getRecord1Function = new GetRecord1Function();
                getRecord1Function.Table = table;
                getRecord1Function.Key = key;
                getRecord1Function.Schema = schema;
            
            return ContractHandler.QueryAsync<GetRecord1Function, byte[]>(getRecord1Function, blockParameter);
        }

        public Task<byte[]> GetRecordQueryAsync(GetRecordFunction getRecordFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetRecordFunction, byte[]>(getRecordFunction, blockParameter);
        }

        
        public Task<byte[]> GetRecordQueryAsync(byte[] table, List<byte[]> key, BlockParameter blockParameter = null)
        {
            var getRecordFunction = new GetRecordFunction();
                getRecordFunction.Table = table;
                getRecordFunction.Key = key;
            
            return ContractHandler.QueryAsync<GetRecordFunction, byte[]>(getRecordFunction, blockParameter);
        }

        public Task<byte[]> GetSchemaQueryAsync(GetSchemaFunction getSchemaFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetSchemaFunction, byte[]>(getSchemaFunction, blockParameter);
        }

        
        public Task<byte[]> GetSchemaQueryAsync(byte[] table, BlockParameter blockParameter = null)
        {
            var getSchemaFunction = new GetSchemaFunction();
                getSchemaFunction.Table = table;
            
            return ContractHandler.QueryAsync<GetSchemaFunction, byte[]>(getSchemaFunction, blockParameter);
        }

        public Task<string> GrantAccessRequestAsync(GrantAccessFunction grantAccessFunction)
        {
             return ContractHandler.SendRequestAsync(grantAccessFunction);
        }

        public Task<TransactionReceipt> GrantAccessRequestAndWaitForReceiptAsync(GrantAccessFunction grantAccessFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(grantAccessFunction, cancellationToken);
        }

        public Task<string> GrantAccessRequestAsync(byte[] @namespace, byte[] name, string grantee)
        {
            var grantAccessFunction = new GrantAccessFunction();
                grantAccessFunction.Namespace = @namespace;
                grantAccessFunction.Name = name;
                grantAccessFunction.Grantee = grantee;
            
             return ContractHandler.SendRequestAsync(grantAccessFunction);
        }

        public Task<TransactionReceipt> GrantAccessRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, string grantee, CancellationTokenSource cancellationToken = null)
        {
            var grantAccessFunction = new GrantAccessFunction();
                grantAccessFunction.Namespace = @namespace;
                grantAccessFunction.Name = name;
                grantAccessFunction.Grantee = grantee;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(grantAccessFunction, cancellationToken);
        }

        public Task<string> IncrementRequestAsync(IncrementFunction incrementFunction)
        {
             return ContractHandler.SendRequestAsync(incrementFunction);
        }

        public Task<string> IncrementRequestAsync()
        {
             return ContractHandler.SendRequestAsync<IncrementFunction>();
        }

        public Task<TransactionReceipt> IncrementRequestAndWaitForReceiptAsync(IncrementFunction incrementFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(incrementFunction, cancellationToken);
        }

        public Task<TransactionReceipt> IncrementRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<IncrementFunction>(null, cancellationToken);
        }

        public Task<string> InstallModuleRequestAsync(InstallModuleFunction installModuleFunction)
        {
             return ContractHandler.SendRequestAsync(installModuleFunction);
        }

        public Task<TransactionReceipt> InstallModuleRequestAndWaitForReceiptAsync(InstallModuleFunction installModuleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(installModuleFunction, cancellationToken);
        }

        public Task<string> InstallModuleRequestAsync(string module, byte[] args)
        {
            var installModuleFunction = new InstallModuleFunction();
                installModuleFunction.Module = module;
                installModuleFunction.Args = args;
            
             return ContractHandler.SendRequestAsync(installModuleFunction);
        }

        public Task<TransactionReceipt> InstallModuleRequestAndWaitForReceiptAsync(string module, byte[] args, CancellationTokenSource cancellationToken = null)
        {
            var installModuleFunction = new InstallModuleFunction();
                installModuleFunction.Module = module;
                installModuleFunction.Args = args;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(installModuleFunction, cancellationToken);
        }

        public Task<string> InstallRootModuleRequestAsync(InstallRootModuleFunction installRootModuleFunction)
        {
             return ContractHandler.SendRequestAsync(installRootModuleFunction);
        }

        public Task<TransactionReceipt> InstallRootModuleRequestAndWaitForReceiptAsync(InstallRootModuleFunction installRootModuleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(installRootModuleFunction, cancellationToken);
        }

        public Task<string> InstallRootModuleRequestAsync(string module, byte[] args)
        {
            var installRootModuleFunction = new InstallRootModuleFunction();
                installRootModuleFunction.Module = module;
                installRootModuleFunction.Args = args;
            
             return ContractHandler.SendRequestAsync(installRootModuleFunction);
        }

        public Task<TransactionReceipt> InstallRootModuleRequestAndWaitForReceiptAsync(string module, byte[] args, CancellationTokenSource cancellationToken = null)
        {
            var installRootModuleFunction = new InstallRootModuleFunction();
                installRootModuleFunction.Module = module;
                installRootModuleFunction.Args = args;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(installRootModuleFunction, cancellationToken);
        }



        public Task<string> MoveRequestAsync(MoveFunction moveFunction)
        {
             return ContractHandler.SendRequestAsync(moveFunction);
        }

        public Task<TransactionReceipt> MoveRequestAndWaitForReceiptAsync(MoveFunction moveFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(moveFunction, cancellationToken);
        }

        public Task<string> MoveRequestAsync(int x, int y)
        {
            var moveFunction = new MoveFunction();
                moveFunction.X = x;
                moveFunction.Y = y;
            
             return ContractHandler.SendRequestAsync(moveFunction);
        }

        public Task<TransactionReceipt> MoveRequestAndWaitForReceiptAsync(int x, int y, CancellationTokenSource cancellationToken = null)
        {
            var moveFunction = new MoveFunction();
                moveFunction.X = x;
                moveFunction.Y = y;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(moveFunction, cancellationToken);
        }

        public Task<string> PopFromFieldRequestAsync(PopFromField1Function popFromField1Function)
        {
             return ContractHandler.SendRequestAsync(popFromField1Function);
        }

        public Task<TransactionReceipt> PopFromFieldRequestAndWaitForReceiptAsync(PopFromField1Function popFromField1Function, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(popFromField1Function, cancellationToken);
        }

        public Task<string> PopFromFieldRequestAsync(byte[] @namespace, byte[] name, List<byte[]> key, byte schemaIndex, BigInteger byteLengthToPop)
        {
            var popFromField1Function = new PopFromField1Function();
                popFromField1Function.Namespace = @namespace;
                popFromField1Function.Name = name;
                popFromField1Function.Key = key;
                popFromField1Function.SchemaIndex = schemaIndex;
                popFromField1Function.ByteLengthToPop = byteLengthToPop;
            
             return ContractHandler.SendRequestAsync(popFromField1Function);
        }

        public Task<TransactionReceipt> PopFromFieldRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, List<byte[]> key, byte schemaIndex, BigInteger byteLengthToPop, CancellationTokenSource cancellationToken = null)
        {
            var popFromField1Function = new PopFromField1Function();
                popFromField1Function.Namespace = @namespace;
                popFromField1Function.Name = name;
                popFromField1Function.Key = key;
                popFromField1Function.SchemaIndex = schemaIndex;
                popFromField1Function.ByteLengthToPop = byteLengthToPop;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(popFromField1Function, cancellationToken);
        }

        public Task<string> PopFromFieldRequestAsync(PopFromFieldFunction popFromFieldFunction)
        {
             return ContractHandler.SendRequestAsync(popFromFieldFunction);
        }

        public Task<TransactionReceipt> PopFromFieldRequestAndWaitForReceiptAsync(PopFromFieldFunction popFromFieldFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(popFromFieldFunction, cancellationToken);
        }

        public Task<string> PopFromFieldRequestAsync(byte[] table, List<byte[]> key, byte schemaIndex, BigInteger byteLengthToPop)
        {
            var popFromFieldFunction = new PopFromFieldFunction();
                popFromFieldFunction.Table = table;
                popFromFieldFunction.Key = key;
                popFromFieldFunction.SchemaIndex = schemaIndex;
                popFromFieldFunction.ByteLengthToPop = byteLengthToPop;
            
             return ContractHandler.SendRequestAsync(popFromFieldFunction);
        }

        public Task<TransactionReceipt> PopFromFieldRequestAndWaitForReceiptAsync(byte[] table, List<byte[]> key, byte schemaIndex, BigInteger byteLengthToPop, CancellationTokenSource cancellationToken = null)
        {
            var popFromFieldFunction = new PopFromFieldFunction();
                popFromFieldFunction.Table = table;
                popFromFieldFunction.Key = key;
                popFromFieldFunction.SchemaIndex = schemaIndex;
                popFromFieldFunction.ByteLengthToPop = byteLengthToPop;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(popFromFieldFunction, cancellationToken);
        }

        public Task<string> PushToFieldRequestAsync(PushToFieldFunction pushToFieldFunction)
        {
             return ContractHandler.SendRequestAsync(pushToFieldFunction);
        }

        public Task<TransactionReceipt> PushToFieldRequestAndWaitForReceiptAsync(PushToFieldFunction pushToFieldFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(pushToFieldFunction, cancellationToken);
        }

        public Task<string> PushToFieldRequestAsync(byte[] table, List<byte[]> key, byte schemaIndex, byte[] dataToPush)
        {
            var pushToFieldFunction = new PushToFieldFunction();
                pushToFieldFunction.Table = table;
                pushToFieldFunction.Key = key;
                pushToFieldFunction.SchemaIndex = schemaIndex;
                pushToFieldFunction.DataToPush = dataToPush;
            
             return ContractHandler.SendRequestAsync(pushToFieldFunction);
        }

        public Task<TransactionReceipt> PushToFieldRequestAndWaitForReceiptAsync(byte[] table, List<byte[]> key, byte schemaIndex, byte[] dataToPush, CancellationTokenSource cancellationToken = null)
        {
            var pushToFieldFunction = new PushToFieldFunction();
                pushToFieldFunction.Table = table;
                pushToFieldFunction.Key = key;
                pushToFieldFunction.SchemaIndex = schemaIndex;
                pushToFieldFunction.DataToPush = dataToPush;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(pushToFieldFunction, cancellationToken);
        }

        public Task<string> PushToFieldRequestAsync(PushToField1Function pushToField1Function)
        {
             return ContractHandler.SendRequestAsync(pushToField1Function);
        }

        public Task<TransactionReceipt> PushToFieldRequestAndWaitForReceiptAsync(PushToField1Function pushToField1Function, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(pushToField1Function, cancellationToken);
        }

        public Task<string> PushToFieldRequestAsync(byte[] @namespace, byte[] name, List<byte[]> key, byte schemaIndex, byte[] dataToPush)
        {
            var pushToField1Function = new PushToField1Function();
                pushToField1Function.Namespace = @namespace;
                pushToField1Function.Name = name;
                pushToField1Function.Key = key;
                pushToField1Function.SchemaIndex = schemaIndex;
                pushToField1Function.DataToPush = dataToPush;
            
             return ContractHandler.SendRequestAsync(pushToField1Function);
        }

        public Task<TransactionReceipt> PushToFieldRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, List<byte[]> key, byte schemaIndex, byte[] dataToPush, CancellationTokenSource cancellationToken = null)
        {
            var pushToField1Function = new PushToField1Function();
                pushToField1Function.Namespace = @namespace;
                pushToField1Function.Name = name;
                pushToField1Function.Key = key;
                pushToField1Function.SchemaIndex = schemaIndex;
                pushToField1Function.DataToPush = dataToPush;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(pushToField1Function, cancellationToken);
        }

        public Task<string> RegisterFunctionSelectorRequestAsync(RegisterFunctionSelectorFunction registerFunctionSelectorFunction)
        {
             return ContractHandler.SendRequestAsync(registerFunctionSelectorFunction);
        }

        public Task<TransactionReceipt> RegisterFunctionSelectorRequestAndWaitForReceiptAsync(RegisterFunctionSelectorFunction registerFunctionSelectorFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerFunctionSelectorFunction, cancellationToken);
        }

        public Task<string> RegisterFunctionSelectorRequestAsync(byte[] @namespace, byte[] name, string systemFunctionName, string systemFunctionArguments)
        {
            var registerFunctionSelectorFunction = new RegisterFunctionSelectorFunction();
                registerFunctionSelectorFunction.Namespace = @namespace;
                registerFunctionSelectorFunction.Name = name;
                registerFunctionSelectorFunction.SystemFunctionName = systemFunctionName;
                registerFunctionSelectorFunction.SystemFunctionArguments = systemFunctionArguments;
            
             return ContractHandler.SendRequestAsync(registerFunctionSelectorFunction);
        }

        public Task<TransactionReceipt> RegisterFunctionSelectorRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, string systemFunctionName, string systemFunctionArguments, CancellationTokenSource cancellationToken = null)
        {
            var registerFunctionSelectorFunction = new RegisterFunctionSelectorFunction();
                registerFunctionSelectorFunction.Namespace = @namespace;
                registerFunctionSelectorFunction.Name = name;
                registerFunctionSelectorFunction.SystemFunctionName = systemFunctionName;
                registerFunctionSelectorFunction.SystemFunctionArguments = systemFunctionArguments;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerFunctionSelectorFunction, cancellationToken);
        }

        public Task<string> RegisterHookRequestAsync(RegisterHookFunction registerHookFunction)
        {
             return ContractHandler.SendRequestAsync(registerHookFunction);
        }

        public Task<TransactionReceipt> RegisterHookRequestAndWaitForReceiptAsync(RegisterHookFunction registerHookFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerHookFunction, cancellationToken);
        }

        public Task<string> RegisterHookRequestAsync(byte[] @namespace, byte[] name, string hook)
        {
            var registerHookFunction = new RegisterHookFunction();
                registerHookFunction.Namespace = @namespace;
                registerHookFunction.Name = name;
                registerHookFunction.Hook = hook;
            
             return ContractHandler.SendRequestAsync(registerHookFunction);
        }

        public Task<TransactionReceipt> RegisterHookRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, string hook, CancellationTokenSource cancellationToken = null)
        {
            var registerHookFunction = new RegisterHookFunction();
                registerHookFunction.Namespace = @namespace;
                registerHookFunction.Name = name;
                registerHookFunction.Hook = hook;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerHookFunction, cancellationToken);
        }

        public Task<string> RegisterNamespaceRequestAsync(RegisterNamespaceFunction registerNamespaceFunction)
        {
             return ContractHandler.SendRequestAsync(registerNamespaceFunction);
        }

        public Task<TransactionReceipt> RegisterNamespaceRequestAndWaitForReceiptAsync(RegisterNamespaceFunction registerNamespaceFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerNamespaceFunction, cancellationToken);
        }

        public Task<string> RegisterNamespaceRequestAsync(byte[] @namespace)
        {
            var registerNamespaceFunction = new RegisterNamespaceFunction();
                registerNamespaceFunction.Namespace = @namespace;
            
             return ContractHandler.SendRequestAsync(registerNamespaceFunction);
        }

        public Task<TransactionReceipt> RegisterNamespaceRequestAndWaitForReceiptAsync(byte[] @namespace, CancellationTokenSource cancellationToken = null)
        {
            var registerNamespaceFunction = new RegisterNamespaceFunction();
                registerNamespaceFunction.Namespace = @namespace;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerNamespaceFunction, cancellationToken);
        }

        public Task<string> RegisterRootFunctionSelectorRequestAsync(RegisterRootFunctionSelectorFunction registerRootFunctionSelectorFunction)
        {
             return ContractHandler.SendRequestAsync(registerRootFunctionSelectorFunction);
        }

        public Task<TransactionReceipt> RegisterRootFunctionSelectorRequestAndWaitForReceiptAsync(RegisterRootFunctionSelectorFunction registerRootFunctionSelectorFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerRootFunctionSelectorFunction, cancellationToken);
        }

        public Task<string> RegisterRootFunctionSelectorRequestAsync(byte[] @namespace, byte[] name, byte[] worldFunctionSelector, byte[] systemFunctionSelector)
        {
            var registerRootFunctionSelectorFunction = new RegisterRootFunctionSelectorFunction();
                registerRootFunctionSelectorFunction.Namespace = @namespace;
                registerRootFunctionSelectorFunction.Name = name;
                registerRootFunctionSelectorFunction.WorldFunctionSelector = worldFunctionSelector;
                registerRootFunctionSelectorFunction.SystemFunctionSelector = systemFunctionSelector;
            
             return ContractHandler.SendRequestAsync(registerRootFunctionSelectorFunction);
        }

        public Task<TransactionReceipt> RegisterRootFunctionSelectorRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, byte[] worldFunctionSelector, byte[] systemFunctionSelector, CancellationTokenSource cancellationToken = null)
        {
            var registerRootFunctionSelectorFunction = new RegisterRootFunctionSelectorFunction();
                registerRootFunctionSelectorFunction.Namespace = @namespace;
                registerRootFunctionSelectorFunction.Name = name;
                registerRootFunctionSelectorFunction.WorldFunctionSelector = worldFunctionSelector;
                registerRootFunctionSelectorFunction.SystemFunctionSelector = systemFunctionSelector;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerRootFunctionSelectorFunction, cancellationToken);
        }

        public Task<string> RegisterSchemaRequestAsync(RegisterSchemaFunction registerSchemaFunction)
        {
             return ContractHandler.SendRequestAsync(registerSchemaFunction);
        }

        public Task<TransactionReceipt> RegisterSchemaRequestAndWaitForReceiptAsync(RegisterSchemaFunction registerSchemaFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerSchemaFunction, cancellationToken);
        }

        public Task<string> RegisterSchemaRequestAsync(byte[] table, byte[] schema, byte[] keySchema)
        {
            var registerSchemaFunction = new RegisterSchemaFunction();
                registerSchemaFunction.Table = table;
                registerSchemaFunction.Schema = schema;
                registerSchemaFunction.KeySchema = keySchema;
            
             return ContractHandler.SendRequestAsync(registerSchemaFunction);
        }

        public Task<TransactionReceipt> RegisterSchemaRequestAndWaitForReceiptAsync(byte[] table, byte[] schema, byte[] keySchema, CancellationTokenSource cancellationToken = null)
        {
            var registerSchemaFunction = new RegisterSchemaFunction();
                registerSchemaFunction.Table = table;
                registerSchemaFunction.Schema = schema;
                registerSchemaFunction.KeySchema = keySchema;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerSchemaFunction, cancellationToken);
        }

        public Task<string> RegisterStoreHookRequestAsync(RegisterStoreHookFunction registerStoreHookFunction)
        {
             return ContractHandler.SendRequestAsync(registerStoreHookFunction);
        }

        public Task<TransactionReceipt> RegisterStoreHookRequestAndWaitForReceiptAsync(RegisterStoreHookFunction registerStoreHookFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerStoreHookFunction, cancellationToken);
        }

        public Task<string> RegisterStoreHookRequestAsync(byte[] table, string hook)
        {
            var registerStoreHookFunction = new RegisterStoreHookFunction();
                registerStoreHookFunction.Table = table;
                registerStoreHookFunction.Hook = hook;
            
             return ContractHandler.SendRequestAsync(registerStoreHookFunction);
        }

        public Task<TransactionReceipt> RegisterStoreHookRequestAndWaitForReceiptAsync(byte[] table, string hook, CancellationTokenSource cancellationToken = null)
        {
            var registerStoreHookFunction = new RegisterStoreHookFunction();
                registerStoreHookFunction.Table = table;
                registerStoreHookFunction.Hook = hook;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerStoreHookFunction, cancellationToken);
        }

        public Task<string> RegisterSystemRequestAsync(RegisterSystemFunction registerSystemFunction)
        {
             return ContractHandler.SendRequestAsync(registerSystemFunction);
        }

        public Task<TransactionReceipt> RegisterSystemRequestAndWaitForReceiptAsync(RegisterSystemFunction registerSystemFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerSystemFunction, cancellationToken);
        }

        public Task<string> RegisterSystemRequestAsync(byte[] @namespace, byte[] name, string system, bool publicAccess)
        {
            var registerSystemFunction = new RegisterSystemFunction();
                registerSystemFunction.Namespace = @namespace;
                registerSystemFunction.Name = name;
                registerSystemFunction.System = system;
                registerSystemFunction.PublicAccess = publicAccess;
            
             return ContractHandler.SendRequestAsync(registerSystemFunction);
        }

        public Task<TransactionReceipt> RegisterSystemRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, string system, bool publicAccess, CancellationTokenSource cancellationToken = null)
        {
            var registerSystemFunction = new RegisterSystemFunction();
                registerSystemFunction.Namespace = @namespace;
                registerSystemFunction.Name = name;
                registerSystemFunction.System = system;
                registerSystemFunction.PublicAccess = publicAccess;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerSystemFunction, cancellationToken);
        }

        public Task<string> RegisterSystemHookRequestAsync(RegisterSystemHookFunction registerSystemHookFunction)
        {
             return ContractHandler.SendRequestAsync(registerSystemHookFunction);
        }

        public Task<TransactionReceipt> RegisterSystemHookRequestAndWaitForReceiptAsync(RegisterSystemHookFunction registerSystemHookFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerSystemHookFunction, cancellationToken);
        }

        public Task<string> RegisterSystemHookRequestAsync(byte[] @namespace, byte[] name, string hook)
        {
            var registerSystemHookFunction = new RegisterSystemHookFunction();
                registerSystemHookFunction.Namespace = @namespace;
                registerSystemHookFunction.Name = name;
                registerSystemHookFunction.Hook = hook;
            
             return ContractHandler.SendRequestAsync(registerSystemHookFunction);
        }

        public Task<TransactionReceipt> RegisterSystemHookRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, string hook, CancellationTokenSource cancellationToken = null)
        {
            var registerSystemHookFunction = new RegisterSystemHookFunction();
                registerSystemHookFunction.Namespace = @namespace;
                registerSystemHookFunction.Name = name;
                registerSystemHookFunction.Hook = hook;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerSystemHookFunction, cancellationToken);
        }

        public Task<string> RegisterTableRequestAsync(RegisterTableFunction registerTableFunction)
        {
             return ContractHandler.SendRequestAsync(registerTableFunction);
        }

        public Task<TransactionReceipt> RegisterTableRequestAndWaitForReceiptAsync(RegisterTableFunction registerTableFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerTableFunction, cancellationToken);
        }

        public Task<string> RegisterTableRequestAsync(byte[] @namespace, byte[] name, byte[] valueSchema, byte[] keySchema)
        {
            var registerTableFunction = new RegisterTableFunction();
                registerTableFunction.Namespace = @namespace;
                registerTableFunction.Name = name;
                registerTableFunction.ValueSchema = valueSchema;
                registerTableFunction.KeySchema = keySchema;
            
             return ContractHandler.SendRequestAsync(registerTableFunction);
        }

        public Task<TransactionReceipt> RegisterTableRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, byte[] valueSchema, byte[] keySchema, CancellationTokenSource cancellationToken = null)
        {
            var registerTableFunction = new RegisterTableFunction();
                registerTableFunction.Namespace = @namespace;
                registerTableFunction.Name = name;
                registerTableFunction.ValueSchema = valueSchema;
                registerTableFunction.KeySchema = keySchema;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerTableFunction, cancellationToken);
        }

        public Task<string> RegisterTableHookRequestAsync(RegisterTableHookFunction registerTableHookFunction)
        {
             return ContractHandler.SendRequestAsync(registerTableHookFunction);
        }

        public Task<TransactionReceipt> RegisterTableHookRequestAndWaitForReceiptAsync(RegisterTableHookFunction registerTableHookFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerTableHookFunction, cancellationToken);
        }

        public Task<string> RegisterTableHookRequestAsync(byte[] @namespace, byte[] name, string hook)
        {
            var registerTableHookFunction = new RegisterTableHookFunction();
                registerTableHookFunction.Namespace = @namespace;
                registerTableHookFunction.Name = name;
                registerTableHookFunction.Hook = hook;
            
             return ContractHandler.SendRequestAsync(registerTableHookFunction);
        }

        public Task<TransactionReceipt> RegisterTableHookRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, string hook, CancellationTokenSource cancellationToken = null)
        {
            var registerTableHookFunction = new RegisterTableHookFunction();
                registerTableHookFunction.Namespace = @namespace;
                registerTableHookFunction.Name = name;
                registerTableHookFunction.Hook = hook;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerTableHookFunction, cancellationToken);
        }

        public Task<string> RevokeAccessRequestAsync(RevokeAccessFunction revokeAccessFunction)
        {
             return ContractHandler.SendRequestAsync(revokeAccessFunction);
        }

        public Task<TransactionReceipt> RevokeAccessRequestAndWaitForReceiptAsync(RevokeAccessFunction revokeAccessFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeAccessFunction, cancellationToken);
        }

        public Task<string> RevokeAccessRequestAsync(byte[] @namespace, byte[] name, string grantee)
        {
            var revokeAccessFunction = new RevokeAccessFunction();
                revokeAccessFunction.Namespace = @namespace;
                revokeAccessFunction.Name = name;
                revokeAccessFunction.Grantee = grantee;
            
             return ContractHandler.SendRequestAsync(revokeAccessFunction);
        }

        public Task<TransactionReceipt> RevokeAccessRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, string grantee, CancellationTokenSource cancellationToken = null)
        {
            var revokeAccessFunction = new RevokeAccessFunction();
                revokeAccessFunction.Namespace = @namespace;
                revokeAccessFunction.Name = name;
                revokeAccessFunction.Grantee = grantee;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeAccessFunction, cancellationToken);
        }

        public Task<string> SetFieldRequestAsync(SetFieldFunction setFieldFunction)
        {
             return ContractHandler.SendRequestAsync(setFieldFunction);
        }

        public Task<TransactionReceipt> SetFieldRequestAndWaitForReceiptAsync(SetFieldFunction setFieldFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setFieldFunction, cancellationToken);
        }

        public Task<string> SetFieldRequestAsync(byte[] table, List<byte[]> key, byte schemaIndex, byte[] data)
        {
            var setFieldFunction = new SetFieldFunction();
                setFieldFunction.Table = table;
                setFieldFunction.Key = key;
                setFieldFunction.SchemaIndex = schemaIndex;
                setFieldFunction.Data = data;
            
             return ContractHandler.SendRequestAsync(setFieldFunction);
        }

        public Task<TransactionReceipt> SetFieldRequestAndWaitForReceiptAsync(byte[] table, List<byte[]> key, byte schemaIndex, byte[] data, CancellationTokenSource cancellationToken = null)
        {
            var setFieldFunction = new SetFieldFunction();
                setFieldFunction.Table = table;
                setFieldFunction.Key = key;
                setFieldFunction.SchemaIndex = schemaIndex;
                setFieldFunction.Data = data;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setFieldFunction, cancellationToken);
        }

        public Task<string> SetFieldRequestAsync(SetField1Function setField1Function)
        {
             return ContractHandler.SendRequestAsync(setField1Function);
        }

        public Task<TransactionReceipt> SetFieldRequestAndWaitForReceiptAsync(SetField1Function setField1Function, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setField1Function, cancellationToken);
        }

        public Task<string> SetFieldRequestAsync(byte[] @namespace, byte[] name, List<byte[]> key, byte schemaIndex, byte[] data)
        {
            var setField1Function = new SetField1Function();
                setField1Function.Namespace = @namespace;
                setField1Function.Name = name;
                setField1Function.Key = key;
                setField1Function.SchemaIndex = schemaIndex;
                setField1Function.Data = data;
            
             return ContractHandler.SendRequestAsync(setField1Function);
        }

        public Task<TransactionReceipt> SetFieldRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, List<byte[]> key, byte schemaIndex, byte[] data, CancellationTokenSource cancellationToken = null)
        {
            var setField1Function = new SetField1Function();
                setField1Function.Namespace = @namespace;
                setField1Function.Name = name;
                setField1Function.Key = key;
                setField1Function.SchemaIndex = schemaIndex;
                setField1Function.Data = data;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setField1Function, cancellationToken);
        }

        public Task<string> SetMetadataRequestAsync(SetMetadata1Function setMetadata1Function)
        {
             return ContractHandler.SendRequestAsync(setMetadata1Function);
        }

        public Task<TransactionReceipt> SetMetadataRequestAndWaitForReceiptAsync(SetMetadata1Function setMetadata1Function, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMetadata1Function, cancellationToken);
        }

        public Task<string> SetMetadataRequestAsync(byte[] @namespace, byte[] name, string tableName, List<string> fieldNames)
        {
            var setMetadata1Function = new SetMetadata1Function();
                setMetadata1Function.Namespace = @namespace;
                setMetadata1Function.Name = name;
                setMetadata1Function.TableName = tableName;
                setMetadata1Function.FieldNames = fieldNames;
            
             return ContractHandler.SendRequestAsync(setMetadata1Function);
        }

        public Task<TransactionReceipt> SetMetadataRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, string tableName, List<string> fieldNames, CancellationTokenSource cancellationToken = null)
        {
            var setMetadata1Function = new SetMetadata1Function();
                setMetadata1Function.Namespace = @namespace;
                setMetadata1Function.Name = name;
                setMetadata1Function.TableName = tableName;
                setMetadata1Function.FieldNames = fieldNames;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMetadata1Function, cancellationToken);
        }

        public Task<string> SetMetadataRequestAsync(SetMetadataFunction setMetadataFunction)
        {
             return ContractHandler.SendRequestAsync(setMetadataFunction);
        }

        public Task<TransactionReceipt> SetMetadataRequestAndWaitForReceiptAsync(SetMetadataFunction setMetadataFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMetadataFunction, cancellationToken);
        }

        public Task<string> SetMetadataRequestAsync(byte[] table, string tableName, List<string> fieldNames)
        {
            var setMetadataFunction = new SetMetadataFunction();
                setMetadataFunction.Table = table;
                setMetadataFunction.TableName = tableName;
                setMetadataFunction.FieldNames = fieldNames;
            
             return ContractHandler.SendRequestAsync(setMetadataFunction);
        }

        public Task<TransactionReceipt> SetMetadataRequestAndWaitForReceiptAsync(byte[] table, string tableName, List<string> fieldNames, CancellationTokenSource cancellationToken = null)
        {
            var setMetadataFunction = new SetMetadataFunction();
                setMetadataFunction.Table = table;
                setMetadataFunction.TableName = tableName;
                setMetadataFunction.FieldNames = fieldNames;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMetadataFunction, cancellationToken);
        }

        public Task<string> SetRecordRequestAsync(SetRecord1Function setRecord1Function)
        {
             return ContractHandler.SendRequestAsync(setRecord1Function);
        }

        public Task<TransactionReceipt> SetRecordRequestAndWaitForReceiptAsync(SetRecord1Function setRecord1Function, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setRecord1Function, cancellationToken);
        }

        public Task<string> SetRecordRequestAsync(byte[] @namespace, byte[] name, List<byte[]> key, byte[] data)
        {
            var setRecord1Function = new SetRecord1Function();
                setRecord1Function.Namespace = @namespace;
                setRecord1Function.Name = name;
                setRecord1Function.Key = key;
                setRecord1Function.Data = data;
            
             return ContractHandler.SendRequestAsync(setRecord1Function);
        }

        public Task<TransactionReceipt> SetRecordRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, List<byte[]> key, byte[] data, CancellationTokenSource cancellationToken = null)
        {
            var setRecord1Function = new SetRecord1Function();
                setRecord1Function.Namespace = @namespace;
                setRecord1Function.Name = name;
                setRecord1Function.Key = key;
                setRecord1Function.Data = data;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setRecord1Function, cancellationToken);
        }

        public Task<string> SetRecordRequestAsync(SetRecordFunction setRecordFunction)
        {
             return ContractHandler.SendRequestAsync(setRecordFunction);
        }

        public Task<TransactionReceipt> SetRecordRequestAndWaitForReceiptAsync(SetRecordFunction setRecordFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setRecordFunction, cancellationToken);
        }

        public Task<string> SetRecordRequestAsync(byte[] table, List<byte[]> key, byte[] data)
        {
            var setRecordFunction = new SetRecordFunction();
                setRecordFunction.Table = table;
                setRecordFunction.Key = key;
                setRecordFunction.Data = data;
            
             return ContractHandler.SendRequestAsync(setRecordFunction);
        }

        public Task<TransactionReceipt> SetRecordRequestAndWaitForReceiptAsync(byte[] table, List<byte[]> key, byte[] data, CancellationTokenSource cancellationToken = null)
        {
            var setRecordFunction = new SetRecordFunction();
                setRecordFunction.Table = table;
                setRecordFunction.Key = key;
                setRecordFunction.Data = data;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setRecordFunction, cancellationToken);
        }

        public Task<string> SpawnRequestAsync(SpawnFunction spawnFunction)
        {
             return ContractHandler.SendRequestAsync(spawnFunction);
        }

        public Task<TransactionReceipt> SpawnRequestAndWaitForReceiptAsync(SpawnFunction spawnFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(spawnFunction, cancellationToken);
        }

        public Task<string> SpawnRequestAsync(int x, int y)
        {
            var spawnFunction = new SpawnFunction();
                spawnFunction.X = x;
                spawnFunction.Y = y;
            
             return ContractHandler.SendRequestAsync(spawnFunction);
        }

        public Task<TransactionReceipt> SpawnRequestAndWaitForReceiptAsync(int x, int y, CancellationTokenSource cancellationToken = null)
        {
            var spawnFunction = new SpawnFunction();
                spawnFunction.X = x;
                spawnFunction.Y = y;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(spawnFunction, cancellationToken);
        }

        public Task<string> UpdateInFieldRequestAsync(UpdateInFieldFunction updateInFieldFunction)
        {
             return ContractHandler.SendRequestAsync(updateInFieldFunction);
        }

        public Task<TransactionReceipt> UpdateInFieldRequestAndWaitForReceiptAsync(UpdateInFieldFunction updateInFieldFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updateInFieldFunction, cancellationToken);
        }

        public Task<string> UpdateInFieldRequestAsync(byte[] table, List<byte[]> key, byte schemaIndex, BigInteger startByteIndex, byte[] dataToSet)
        {
            var updateInFieldFunction = new UpdateInFieldFunction();
                updateInFieldFunction.Table = table;
                updateInFieldFunction.Key = key;
                updateInFieldFunction.SchemaIndex = schemaIndex;
                updateInFieldFunction.StartByteIndex = startByteIndex;
                updateInFieldFunction.DataToSet = dataToSet;
            
             return ContractHandler.SendRequestAsync(updateInFieldFunction);
        }

        public Task<TransactionReceipt> UpdateInFieldRequestAndWaitForReceiptAsync(byte[] table, List<byte[]> key, byte schemaIndex, BigInteger startByteIndex, byte[] dataToSet, CancellationTokenSource cancellationToken = null)
        {
            var updateInFieldFunction = new UpdateInFieldFunction();
                updateInFieldFunction.Table = table;
                updateInFieldFunction.Key = key;
                updateInFieldFunction.SchemaIndex = schemaIndex;
                updateInFieldFunction.StartByteIndex = startByteIndex;
                updateInFieldFunction.DataToSet = dataToSet;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updateInFieldFunction, cancellationToken);
        }

        public Task<string> UpdateInFieldRequestAsync(UpdateInField1Function updateInField1Function)
        {
             return ContractHandler.SendRequestAsync(updateInField1Function);
        }

        public Task<TransactionReceipt> UpdateInFieldRequestAndWaitForReceiptAsync(UpdateInField1Function updateInField1Function, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updateInField1Function, cancellationToken);
        }

        public Task<string> UpdateInFieldRequestAsync(byte[] @namespace, byte[] name, List<byte[]> key, byte schemaIndex, BigInteger startByteIndex, byte[] dataToSet)
        {
            var updateInField1Function = new UpdateInField1Function();
                updateInField1Function.Namespace = @namespace;
                updateInField1Function.Name = name;
                updateInField1Function.Key = key;
                updateInField1Function.SchemaIndex = schemaIndex;
                updateInField1Function.StartByteIndex = startByteIndex;
                updateInField1Function.DataToSet = dataToSet;
            
             return ContractHandler.SendRequestAsync(updateInField1Function);
        }

        public Task<TransactionReceipt> UpdateInFieldRequestAndWaitForReceiptAsync(byte[] @namespace, byte[] name, List<byte[]> key, byte schemaIndex, BigInteger startByteIndex, byte[] dataToSet, CancellationTokenSource cancellationToken = null)
        {
            var updateInField1Function = new UpdateInField1Function();
                updateInField1Function.Namespace = @namespace;
                updateInField1Function.Name = name;
                updateInField1Function.Key = key;
                updateInField1Function.SchemaIndex = schemaIndex;
                updateInField1Function.StartByteIndex = startByteIndex;
                updateInField1Function.DataToSet = dataToSet;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updateInField1Function, cancellationToken);
        }
    }
}
