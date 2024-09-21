// <copyright file="IDFix.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioInteract.API.Patches;

using HarmonyLib;

/// <summary>
/// Fixs problems with ID.
/// </summary>
[HarmonyPatch(typeof(ReferenceHub), nameof(ReferenceHub.Network_playerId), MethodType.Setter)]
public static class IDFix
{
    /// <summary>
    /// Gets or sets auto Increment ID.
    /// </summary>
    public static int AutoIncrement { get; set; } = 0;

    /// <summary>
    /// xz.
    /// </summary>
    /// <param name="value">xzzzz.</param>
    [HarmonyPrefix]
    public static void Prefix(ref RecyclablePlayerId value)
    {
        // value.Destroy();
        // value = new RecyclablePlayerId(++AutoIncrement);
    }

    /// <summary>
    /// Refresh ID on round restart.
    /// </summary>
    public static void OnRestartRound_RefreshID()
    {
        AutoIncrement = 0;
    }
}