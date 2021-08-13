using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Rhythmium.NoteEditor
{
    public sealed class NoteEditorServer
    {
        private readonly string _port;

        public NoteEditorServer(string port)
        {
            _port = port;
        }

        public async UniTask<NoteEditorStatus> GetStatusAsync()
        {
            var url = $"http://localhost:{_port}";
            var request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Content-Type", "application/json");

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new Exception(request.error);
            }

            return JsonUtility.FromJson<NoteEditorStatus>(request.downloadHandler.text);
        }

        public async UniTask<string> GetChartJsonAsync()
        {
            var url = $"http://localhost:{_port}/data";
            var request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Content-Type", "application/json");

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new Exception(request.error);
            }

            return request.downloadHandler.text;
        }
    }
}
