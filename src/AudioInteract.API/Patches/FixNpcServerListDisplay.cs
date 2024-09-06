// <copyright file="FixNpcServerListDisplay.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioInteract.API.Patches;

using CentralAuth;
using Exiled.API.Features;
using HarmonyLib;

/// <summary>
/// Fix NPC counts as players **IN** server list.
/// </summary>
[HarmonyPatch(typeof(ServerConsole), nameof(ServerConsole.RefreshOnlinePlayers))]
public static class FixNpcServerListDisplay
{
    /// <summary>
    /// ....
    /// </summary>
    /// <returns>.//.</returns>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
    public static bool Prefix()
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    {
        try
        {
            foreach (ReferenceHub allHub in ReferenceHub.AllHubs)
            {
                if (!Player.TryGet(allHub, out var player))
                {
                    continue;
                }

                if (player.IsNPC)
                {
                    continue;
                }

                if (allHub.Mode == ClientInstanceMode.ReadyClient && !string.IsNullOrEmpty(allHub.authManager.UserId) && (!allHub.isLocalPlayer || !ServerStatic.IsDedicated))
                {
                    ServerConsole.PlayersListRaw.objects.Add(allHub.authManager.UserId);
                }
            }

            ServerConsole._verificationPlayersList = JsonSerialize.ToJson(ServerConsole.PlayersListRaw);

            ServerConsole._playersAmount = Player.List.Count(x => !x.IsNPC);
            ServerConsole.PlayersListRaw.objects.Clear();
        }
        catch (Exception ex)
        {
            ServerConsole.AddLog("[VERIFICATION] Exception in Players Online processing: " + ex.Message);
            ServerConsole.AddLog(ex.StackTrace);
        }

        return false;
    }
}