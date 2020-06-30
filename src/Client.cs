using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using SharpConfig;

namespace SmartSounds
{
    public class Main : BaseScript
    {
        public Dictionary<string, string> Commands;
        public float SoundVolume = 0.6f;
        public float SoundRadius = 15f;
        public Main()
        {

            ConfigReader();
            EventHandlers["Client:SmartSounds"] += new Action<Vector3, string>((targetCoords, soundFile) =>
            {
                Vector3 playerCoords = Game.Player.Character.Position;
                var distance = Vdist(playerCoords.X, playerCoords.Y, playerCoords.Z, targetCoords.X, targetCoords.Y, targetCoords.Z);
                float distanceVolumeMultiplier = (SoundVolume / 20f);
                float distanceVolume = SoundVolume - (distance * distanceVolumeMultiplier);
                if (distance <= 20f)
                {
                    SendNuiMessage(string.Format("{{\"submissionType\":\"smartSounds\", \"submissionVolume\":{0}, \"submissionFile\":\"{1}\"}}", (object)distanceVolume, (object)soundFile));
                }
            });
        }

        public void RegisterMainCommand()
        {
            TriggerEvent("chat:addSuggestion", "/sound", "Plays a sound", new[]
            {
                new { name="Name", help="Use /sounds for the full list" },
            });

            TriggerEvent("chat:addSuggestion", "/sounds", "Lists available sounds");

            RegisterCommand("sound", new Action<int, List<object>, string>((source, args, raw) =>
            {
                if (IsStringNullOrEmpty(Convert.ToString(args[0])))
                {
                    TriggerEvent("chat:addMessage", new
                    {
                        color = new[] { 219, 131, 48 },
                        args = new[] { "[SmartSounds]", $"Usage /sound [Name]." }
                    });
                }
                else
                {
                    SoundHandler(Convert.ToString(args[0]));
                }
            }), false);

            RegisterCommand("sounds", new Action<int, List<object>, string>((source, args, raw) =>
            {
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 219, 131, 48 },
                    multiline = true,
                    args = new[] { "[SmartSounds]", $"Sounds: {String.Join(", ", Commands.Keys)}" }
                });
            }), false);
        }

        private void SoundHandler(string name)
        {
            string soundFile = "";
            Commands.TryGetValue(name, out soundFile);
            if (!IsStringNullOrEmpty(soundFile))
            {
                TriggerServerEvent("Server:SmartSounds", GetEntityCoords(PlayerPedId(), true), soundFile);
            }
            else
            {
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 219, 131, 48 },
                    args = new[] { "[SmartSounds]", $"Sound not found. Use /sounds for a list." }
                });
            }
        }

        private void ConfigReader()
        {
            var data = LoadResourceFile(GetCurrentResourceName(), "config.ini");
            if (Configuration.LoadFromString(data).Contains("SmartSounds", "Commands") == true)
            {
                Configuration loaded = Configuration.LoadFromString(data);
                Commands = new Dictionary<string, string>();
                SoundVolume = loaded["SmartSounds"]["SoundVolume"].FloatValue;
                SoundRadius = loaded["SmartSounds"]["SoundRadius"].FloatValue;
                if (SoundVolume > 1.0f)
                {
                    SoundVolume = 1.0f;
                }
                foreach (string s in loaded["SmartSounds"]["Commands"].StringValueArray)
                {
                    if(loaded.Contains(s, "SoundFile"))
                    {
                        var soundFile = loaded[s]["SoundFile"].StringValue;
                        Commands.Add(s, soundFile);
                    }
                    else
                    {
                        ProcessError(true, s);
                    }
                }
                RegisterMainCommand();
            }
            else
            {
                ProcessError(false);
            }
        }

        private void ProcessError(bool custom, string name = "")
        {
            PlaySoundFrontend(-1, "ERROR", "HUD_AMMO_SHOP_SOUNDSET", true);
            if (custom == true)
            {
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 219, 131, 48 },
                    multiline = true,
                    args = new[] { "SmartSounds", $"The {name} command has not been configured correctly." }
                });
            }
            else
            {
                
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 219, 131, 48 },
                    multiline = true,
                    args = new[] { "SmartSounds", $"The config has not been configured correctly." }
                });
            }
        }
    }
}
