using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using HybridWebSocket;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;

namespace mud
{
    public class BlockParams
    {
        public BlockResult result;
    }

    public class BlockResult
    {
        public string number;
    }

    public class Block
    {
        public BlockParams @params;
    }

    public class BlockStream : IDisposable
    {
        private WebSocket _ws;
        public Subject<Block> subject = new();

        private UniTaskCompletionSource<bool> _connectionComplete = new();
        private const int MaxRetryAttempts = 3;
        private int _currentRetryCount = 0;
        private const double BaseDelay = 1000; // Initial delay in milliseconds
        
        public async UniTask WatchBlocks(string wsRpc)
        {
            _ws = WebSocketFactory.CreateInstance(wsRpc);

            _ws.OnOpen += () =>
            {
                _currentRetryCount = 0;
                Debug.Log("WS state: " + _ws.GetState());

                string typeStr = "newHeads";
                var subscriptionRequest = new
                {
                    jsonrpc = "2.0",
                    id = 1,
                    method = "eth_subscribe",
                    @params = new List<string> { typeStr }
                };

                string jsonString = JsonConvert.SerializeObject(subscriptionRequest);
                byte[] byteArray = Encoding.UTF8.GetBytes(jsonString);
                _ws.Send(byteArray);
                _connectionComplete.TrySetResult(true);
            };

            _ws.OnMessage += msg =>
            {
                var message = Encoding.UTF8.GetString(msg);
                var block = JsonConvert.DeserializeObject<Block>(message);
                subject.OnNext(block);
            };

            _ws.OnError += errMsg =>
            {
                Debug.Log("WS error: " + errMsg);
                subject.OnError(new Exception(errMsg));
                TryReconnect(wsRpc);
            };

            _ws.OnClose += code =>
            {
                Debug.Log("WS closed with code: " + code);
                if (code != WebSocketCloseCode.Normal)
                {
                    TryReconnect(wsRpc);
                }
            };

            _ws.Connect();
            await _connectionComplete.Task;
            Debug.Log("WS Connected.");
        }


        private void TryReconnect(string wsRpc)
        {
            if (_currentRetryCount < MaxRetryAttempts)
            {
                var delay = TimeSpan.FromMilliseconds(Math.Pow(2, _currentRetryCount) * BaseDelay); // Exponential delay
                Debug.Log(
                    $"Attempting to reconnect. Trying again in {delay.Seconds} seconds  ({_currentRetryCount + 1} of {MaxRetryAttempts})");
                _currentRetryCount++;
                UniTask.Delay(delay).ContinueWith(() => WatchBlocks(wsRpc));
            }
            else
            {
                Debug.Log("Max retry attempts reached. Giving up.");
                subject.OnError(new Exception("Max retry attempts reached."));
            }
        }

        public void Dispose()
        {
            if (_ws != null && _ws.GetState() == WebSocketState.Open)
            {
                _ws.Close();
            }

            subject?.Dispose();
        }
    }
}
