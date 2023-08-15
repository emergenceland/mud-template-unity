using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using mud.Unity;
using mud.Client;
using IWorld.ContractDefinition;
using System.Text;
using Nethereum.RPC.TransactionTypes;
using Nethereum.Contracts;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.TransactionReceipts;
using Nethereum.RPC.TransactionTypes;
using Nethereum.Web3;

public enum StateType
{
    Normal,
    Happy,
    Sad,
    Count
}

public class BallTest : MonoBehaviour
{
    public int ballIndex;
    public TextMesh text;
    public string key, keyBytes32;
    public StateType state;
    public StateType transactionState;
    public UpdateType expectedType;
    public Material[] materials;
    public MeshRenderer mr;
    int txDelay = 2000;
    bool txSending = false;

    public void UpdateState(StateType newState, UpdateType updateType)
    {

        Debug.Log("Updated " + ballIndex + " " + updateType.ToString(), this);

        state = newState;
        mr.sharedMaterial = materials[(int)newState];

        if(TestManager.TestInitialized) {
            // Debug.Assert(newState == transactionState);
            Debug.Assert(expectedType == updateType, "[Actual: " + updateType.ToString() + "]", this);
        }

        text.text = newState.ToString() + "\n" + updateType.ToString();
    }   

    void Update() {
        
        if(txSending) {
            return;
        }

        if(ballIndex != 0) {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            TestSingle(0);
        } else if(Input.GetKeyDown(KeyCode.Alpha2)) {
            TestSingle(1);
        } else if(Input.GetKeyDown(KeyCode.Alpha3)) {
            TestSingle(2);
        } else if(Input.GetKeyDown(KeyCode.Alpha4)) {
            TestSingle(3);
        } else if(Input.GetKeyDown(KeyCode.Alpha5)) {
            TestSingle(4);
        }
    }

    public async UniTask TestSingle(int function)
    {
      
        if (function == 0)
        {
            transactionState = (StateType)(((int)state + 1) % (int)StateType.Count);
            expectedType = UpdateType.SetRecord;
            Debug.Log("[SET]");
            Debug.Log("[Expected: " + expectedType.ToString() + "]");
            await RunTxUntilItPasses<SetSimpleFunction>();
        }
        else if (function == 1)
        {
            transactionState = (StateType)(((int)state + 1) % (int)StateType.Count);
            expectedType = UpdateType.DeleteRecord;
            Debug.Log("[DELETE]");
            Debug.Log("[Expected: " + expectedType.ToString() + "]");
            await RunTxUntilItPasses<DeleteSimpleFunction>();
        }
        else if (function == 2)
        {
            transactionState = (StateType)(((int)state + 1) % (int)StateType.Count);
            expectedType = UpdateType.SetRecord;
            Debug.Log("[DELETESET]");
            Debug.Log("[Expected: " + expectedType.ToString() + "]");
            await RunTxUntilItPasses<DeleteSetFunction>();
        }
        else if (function == 3)
        {
            transactionState = (StateType)(((int)state + 1) % (int)StateType.Count);
            expectedType = UpdateType.DeleteRecord;
            Debug.Log("[SETDELETE]");
            Debug.Log("[Expected: " + expectedType.ToString() + "]");
            await RunTxUntilItPasses<SetDeleteFunction>();
        } 

        
    }


    static byte[] HexStringToByteArray(string hexString)
    {
        int length = hexString.Length;
        byte[] byteArray = new byte[length / 2];

        for (int i = 0; i < length; i += 2)
        {
            byteArray[i / 2] = System.Convert.ToByte(hexString.Substring(i, 2), 16);
        }

        return byteArray;
    }


    private async UniTask RunTxUntilItPasses<TFunction>() where TFunction : FunctionMessage, new()
    {

        txSending = true;
        while (await SendTx<TFunction>() == false)
        {
            await UniTask.Delay(txDelay);
        }
        txSending = false;

    }

    
    private async UniTask<bool> SendTx<TFunction>() where TFunction : FunctionMessage, new() 
    {

        byte[] byteHexArray = HexStringToByteArray(key.Replace("0x", ""));

        try
        {
            await NetworkManager.Instance.worldSend.TxExecute<TFunction>(byteHexArray);
            return true;
        }
        catch (System.Exception ex)
        {
            // Handle your exception here
            Debug.LogException(ex);
            return false;
        }
    }


}
