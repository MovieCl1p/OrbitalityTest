using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

namespace Core.Actions
{
    public class DownloadAction : BaseAction
    {
        private readonly Action<string> _callback;
        private readonly string _url;

        public DownloadAction(string url, Action<string> callback)
        {
            _url = url;
            _callback = callback;
        }

        public override void Execute()
        {
            base.Execute();

            CoroutineManager.Instance.StartCoroutine(SendRequest(_url, OnComplete, OnFail));
        }

        private void OnComplete(string data)
        {
            _callback?.Invoke(data);

            Complete();
        }

        private void OnFail(string error)
        {
            Fail(error);
        }

        private IEnumerator SendRequest(string url, Action<string> onComplete, Action<string> onFailed = null)
        {
            var request = new UnityWebRequest(url);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.timeout = 10;

            var stopwatch = Stopwatch.StartNew();

            yield return request.SendWebRequest();

            stopwatch.Stop();
            Debug.LogFormat("Received at {0}:{1}:{2}, ping {3} ms", DateTime.Now.Hour, DateTime.Now.Minute,
                DateTime.Now.Second, Mathf.CeilToInt((float) stopwatch.Elapsed.TotalMilliseconds));

            if (request.isNetworkError || request.isHttpError)
            {
                var sb = new StringBuilder();
                sb.Append(request.error);
                sb.Append("\n\nHeaders:\n");

                if (onFailed != null)
                {
                    onFailed(sb.ToString());
                }
            }
            else
            {
                if (onComplete != null)
                {
                    onComplete(request.downloadHandler.text);
                }
            }
        }
    }
}