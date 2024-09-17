// <copyright file="FixNpcIntanceMode.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioInteract.API.Patches;

using CentralAuth;
using Exiled.API.Features;
using HarmonyLib;

/// <summary>
/// Fix NPC changing instace mode to not cause issued with VSR.
/// </summary>
[HarmonyPatch(typeof(PlayerAuthenticationManager), "InstanceMode", MethodType.Setter)]
public static class FixNpcIntanceMode
{
    /// <summary>
    /// ....
    /// </summary>
    /// <param name="__instance">..</param>
    /// <param name="value">.</param>
    /// <returns>.//.</returns>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
    public static bool Prefix(PlayerAuthenticationManager __instance, ClientInstanceMode value)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    {
        if (!Npc.TryGet(__instance._hub, out Player? npc) || !npc.IsNPC)
        {
            return true;
        }

        if (value != ClientInstanceMode.Unverified && value != ClientInstanceMode.Host && value != ClientInstanceMode.DedicatedServer)
        {
            Log.Info(npc.Nickname);
            Log.Info(value);
            return false;
        }

        return true;
    }
}