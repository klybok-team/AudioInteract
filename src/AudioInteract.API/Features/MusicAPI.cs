// <copyright file="MusicAPI.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioInteract.Features;

using System.Reflection;
using System.Runtime.InteropServices;
using AudioInteract.Features;
using CentralAuth;
using Exiled.API.Features;
using HarmonyLib;
using Mirror;
using NVorbis;
using NVorbis.Contracts;
using PlayerRoles;
using SCPSLAudioApi;

/// <summary>
/// Main API class.
/// </summary>
public static class MusicAPI
{
    /// <summary>
    /// Gets harmony ID.
    /// </summary>
    public const string HarmonyID = "Klybok.Team - AudioInteract.API";

    /// <summary>
    /// Harmony main class.
    /// </summary>
    private static Harmony? harmony;
    private static bool isEventsRegistered = false;

    static MusicAPI()
    {
        Startup.SetupDependencies();
    }

    /// <summary>
    /// Gets a value indicating whether events registered or not.
    /// </summary>
    public static bool IsEventsRegistered { get => isEventsRegistered; internal set => isEventsRegistered = value; }

    /// <summary>
    /// Gets settings of NPC.
    /// </summary>
    public static List<MusicInstance> MusicInstances { get; private set; } = new();

    private static List<Assembly> UsingAssemblys { get; set; } = new();

    /// <summary>
    /// Register patches to handle some extra-features of API.
    /// </summary>
    public static void RegisterPatches()
    {
        Assembly callingAssembly = Assembly.GetCallingAssembly();

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
        Assembly callingAssembly = Assembly.GetCallingAssembly();

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
    /// Get file tags.
    /// </summary>
    /// <param name="filePath">Path to file.</param>
    /// <returns><see cref="ITagData"/>.</returns>
    public static ITagData GetFileTags(string filePath)
    {
        return new VorbisReader(filePath).Tags;
    }

    /// <summary>
    /// Get file tags.
    /// </summary>
    /// <param name="audioFile">Audio File.</param>
    /// <returns><see cref="ITagData"/>.</returns>
    public static ITagData GetFileTags(AudioFile audioFile)
    {
        return new VorbisReader(audioFile.FilePath).Tags;
    }

    /// <summary>
    /// Creates new NPC and create settings for him.
    /// </summary>
    /// <param name="name">Name setted to NPC.</param>
    /// <param name="role">Role setted to NPC.</param>
    /// <param name="id">ID setted to NPC. 0 - select new ID.</param>
    /// <param name="userID">UserID setted to NPC.</param>
    /// <returns>Returns Music Instance.</returns>
    public static MusicInstance CreateNPC(string name, RoleTypeId role = RoleTypeId.None, int id = 0, string userID = PlayerAuthenticationManager.DedicatedId)
    {
        Npc npc = CreateDefaultNPC(name, role, id, userID);

        return AddSetingsToNPC(npc);
    }

    /// <summary>
    /// Creates new NPC.
    /// </summary>
    /// <param name="name">Name setted to NPC.</param>
    /// <param name="role">Role setted to NPC.</param>
    /// <param name="id">ID setted to NPC. 0 - select new ID.</param>
    /// <param name="userID">UserID setted to NPC. DO NOT CHANGE THIS IF YOU NOT WANT TO BREAK VSR. Default value hides NPC from list.</param>
    /// <returns>Returns Npc.</returns>
    public static Npc CreateDefaultNPC(string name, RoleTypeId role = RoleTypeId.None, int id = 0, string userID = PlayerAuthenticationManager.DedicatedId)
    {
        if (!RecyclablePlayerId.FreeIds.Contains(id) && RecyclablePlayerId._autoIncrement >= id)
        {
            Log.Warn($"{Assembly.GetCallingAssembly().GetName().Name} tried to spawn an NPC with a duplicate PlayerID. Using auto-incremented ID instead to avoid issues.");

            id = RecyclablePlayerId._autoIncrement++;
        }

        Npc npc = Npc.Spawn(name, role, id, userID);

        npc.RemoteAdminPermissions = PlayerPermissions.AFKImmunity;

        try
        {
            if (userID == PlayerAuthenticationManager.DedicatedId)
            {
                npc.ReferenceHub.authManager.SyncedUserId = userID;
                try
                {
                    npc.ReferenceHub.authManager.InstanceMode = ClientInstanceMode.DedicatedServer;
                }
                catch (Exception e)
                {
                    Log.Debug($"Ignore: {e}");
                }
            }
            else
            {
                npc.ReferenceHub.authManager.UserId = userID == string.Empty ? $"Dummy@localhost" : userID;
            }
        }
        catch (Exception e)
        {
            Log.Debug($"Ignore: {e}");
        }

        return npc;
    }

    /// <summary>
    /// Create settings for NPC.
    /// </summary>
    /// <param name="npc">NPC to set settings.</param>
    /// <returns>Indicates settings is successfuly created or not.</returns>
    public static MusicInstance AddSetingsToNPC(Npc npc)
    {
        MusicInstances.Add(new(npc));

        return MusicInstances.FirstOrDefault(x => x.Npc == npc);
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

        MusicInstance musicInstance = MusicInstances.FirstOrDefault(x => x.Npc == npc);
        if (musicInstance != null)
        {
            musicInstance.AudioPlayerBase?.OnDestroy();

            MusicInstances.Remove(musicInstance);
        }

        try
        {
            // lol
            npc.Disconnect();

            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex);

            return false;
        }
    }

    /// <summary>
    /// Destroy NPC.
    /// </summary>
    /// <param name="musicInstance">Music Instance.</param>
    /// <returns>Indicates NPC is successfuly destroyed or not.</returns>
    public static bool DestroyNPC(MusicInstance musicInstance)
    {
        if (musicInstance == null || musicInstance.Npc == null)
        {
            return false;
        }

        musicInstance.AudioPlayerBase?.OnDestroy();

        MusicInstances.Remove(musicInstance);

        try
        {
            musicInstance?.Npc.Destroy();

            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex);

            return false;
        }
    }
}
