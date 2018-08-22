using System.CodeDom;
using System.Collections;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Photon.Hive.Plugin;

namespace UnityIsland
{
    public class UnityIslandPlugin : PluginBase
    {

        public UnityIslandPlugin()
        {

        }

        public override string Name
        {
            get { return "UnityIslandPlugin"; }
        }

        public override void BeforeJoin(IBeforeJoinGameCallInfo info)
        {
            this.PluginHost.LogInfo("Joining player " + info.UserId);
            base.BeforeJoin(info);
        }

        public override void OnJoin(IJoinGameCallInfo info)
        {
            this.PluginHost.LogInfo("Joined player " + info.ActorNr);

            //set ammo
            SetAmmo(info.ActorNr,5);

            base.OnJoin(info);
        }

        private void SetAmmo(int actorNr, int ammo)
        {
            this.GetActor(actorNr).Properties.Set("Ammo", ammo);
        }

        public override void OnCreateGame(ICreateGameCallInfo info)
        {
            this.PluginHost.LogInfo("Game created. Player " + info.UserId);
            base.OnCreateGame(info);
        }

        public override void BeforeCloseGame(IBeforeCloseGameCallInfo info)
        {
            this.PluginHost.LogInfo("Closing game. Player " + info.UserId);
            base.BeforeCloseGame(info);
        }

        public override void OnLeave(ILeaveGameCallInfo info)
        {
            this.PluginHost.LogInfo("Leaving player " + info.ActorNr);
            base.OnLeave(info);
        }

        public override void OnRaiseEvent(IRaiseEventCallInfo info)
        {
            if (info.Request.EvCode == 200) //RPC
            {
                var rpc = GetRPC(info.Request);
                this.PluginHost.LogInfo($"Event RPC: {GetRPC(info.Request)}");
                switch (rpc)
                {
                    case Rpc.Fire:
                    {
                        Fire(info);
                        return;
                    }
                }
            }
            else
            {
                this.PluginHost.LogInfo($"Event raised:\r\nCode: {info.Request.EvCode}\r\nData: {ToJson(info.Request.Data)}\r\nOperationCode: {info.OperationRequest.OperationCode}\r\nOperationparameters:{ToJson(info.OperationRequest.Parameters)}");
            }

            base.OnRaiseEvent(info);
        }

        void Fire(IRaiseEventCallInfo info)
        {
            var ammo = (int)GetActor(info.ActorNr).Properties.GetProperty("Ammo").Value;
            if (ammo <= 0)
            {
                this.PluginHost.LogError("Fire failed for player " + info.ActorNr);
                info.Fail("No ammo available");
            }
            else
            {
                SetAmmo(info.ActorNr, ammo - 1);
                info.Continue();
            }
        }

        string ToJson(object x)
        {
            var json = new JsonSerializer();
            var writer = new StringWriter();
            json.Serialize(writer, x);
            return writer.ToString();
        }

        IActor GetActor(int actorNr)
        {
            return this.PluginHost.GameActors.First(a => a.ActorNr == actorNr);
        }

        Rpc GetRPC(IRaiseEventRequest request)
        {
            IDictionary rpcData = (IDictionary)request.Data;
            return (Rpc) ((byte)rpcData[(byte)5]);  
        }

        enum Rpc : byte
        {
            Fire = 11
        }

    }
}