using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Server
{
    public class Main : BaseScript
    {
        public Main()
        {
            EventHandlers["Server:SmartSounds"] += new Action<Vector3, string>((targetCoords, soundFile) =>
            {
                TriggerClientEvent("Client:SmartSounds", targetCoords, soundFile);
            });
        }
    }
}
