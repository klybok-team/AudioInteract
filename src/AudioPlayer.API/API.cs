// <copyright file="API.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.API;

using System.Reflection;
using AudioPlayer.API.Features;
using CentralAuth;
using Exiled.API.Features;
using HarmonyLib;
using PlayerRoles;
using UnityEngine;

/// <summary>
/// Main API class to interact with NPC and SCPSLAudioPlayer.
/// </summary>
public static class API
{
    /// <summary>
    /// Gets harmony ID.
    /// </summary>
    public const string HarmonyID = "Klybok.Team - AudioPlayer.API";

    /// <summary>
    /// Harmony main class.
    /// </summary>
    private static Harmony? harmony;

    /// <summary>
    /// Gets settings of NPC.
    /// </summary>
    public static Dictionary<Npc, MusicInstance> NpcToSettings { get; private set; } = new();

    private static List<Assembly> UsingAssemblys { get; set; } = new();

    /// <summary>
    /// Register patches to handle some extra-features of API.
    /// </summary>
    public static void RegisterPatches()
    {
        var callingAssembly = Assembly.GetCallingAssembly();

        if (callingAssembly == null)
        {
            return;
        }

        UsingAssemblys.Add(callingAssembly);

        harmony = new(HarmonyID);
        harmony.PatchAll();
    }

    /// <summary>
    /// Unregister patches.
    /// </summary>
    public static void UnregisterPatches()
    {
        var callingAssembly = Assembly.GetCallingAssembly();

        if (callingAssembly == null)
        {
            return;
        }

        UsingAssemblys.Remove(callingAssembly);

        if (UsingAssemblys.Count > 0)
        {
            return;
        }

        harmony?.UnpatchAll();
        harmony = null;
    }

    /// <summary>
    /// Creates new NPC and create settings for him.
    /// </summary>
    /// <param name="name">Name setted to NPC.</param>
    /// <param name="role">Role setted to NPC.</param>
    /// <param name="id">ID setted to NPC.</param>
    /// <param name="userID">UserID setted to NPC. DO NOT CHANGE THIS IF YOU NOT WANT TO BREAK VSR. Default value hides NPC from list.</param>
    /// <param name="position">Position setted to NPC. null = don't set.</param>
    /// <returns>Indicates NPC is successfuly created or not.</returns>
    public static bool CreateNPC(string name, RoleTypeId role = RoleTypeId.Overwatch, int id = 0, string userID = PlayerAuthenticationManager.DedicatedId, Vector3? position = null)
    {
        try
        {
            var npc = Npc.Spawn(name, role, id, userID, position);

            NpcToSettings.Add(npc, new());

            return npc != null;
        }
        catch (System.Exception ex)
        {
            Log.Error(ex);
            return false;
        }
    }

    /// <summary>
    /// Create settings for NPC.
    /// </summary>
    /// <param name="npc">NPC to set settings.</param>
    /// <returns>Indicates settings is successfuly created or not.</returns>
    public static bool AddSetingsToNPC(Npc npc)
    {
        if (npc == null)
        {
            return false;
        }

        if (NpcToSettings.ContainsKey(npc))
        {
            return false;
        }

        NpcToSettings.Add(npc, new());

        return NpcToSettings.ContainsKey(npc);
    }

    /// <summary>
    /// Destroy NPC.
    /// </summary>
    /// <param name="npc">NPC to destroy.</param>
    /// <returns>Indicates NPC is successfuly destroyed or not.</returns>
    public static bool DestroyNPC(Npc npc)
    {
        if (npc == null)
        {
            return false;
        }

        if (NpcToSettings.TryGetValue(npc, out MusicInstance settings))
        {
            settings.AudioPlayerBase?.OnDestroy();

            NpcToSettings.Remove(npc);
        }

        try
        {
            npc.Destroy();

            return true;
        }
        catch (System.Exception ex)
        {
            Log.Error(ex);

            return false;
        }
    }
}
