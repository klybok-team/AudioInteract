namespace AudioPlayer.API.Patches;

using CentralAuth;
using Exiled.API.Features;
using HarmonyLib;
using MEC;

/// <summary/>
[HarmonyPatch(typeof(ServerConsole), nameof(ServerConsole.HandlePlayerJoin))]
public class HideBotFromlist
{
#pragma warning disable SA1600
    [HarmonyPrefix]
    public static bool Prefix(ReferenceHub rh, ClientInstanceMode mode)
    {
#pragma warning restore SA1600

        Log.Info(1);

        Timing.CallDelayed(0.3f, () =>
        {
            Log.Info(2);


            if (mode == ClientInstanceMode.ReadyClient && !Npc.List.Any(x => x.ReferenceHub == rh))
            {
                Log.Info(4);
                ServerConsole.NewPlayers.Add(rh);
                ServerConsole.RefreshOnlinePlayers();
            }
            Log.Info(3);
        });

        return false;
    }
}
