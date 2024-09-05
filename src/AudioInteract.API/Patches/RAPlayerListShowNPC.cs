// <copyright file="RAPlayerListShowNPC.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioInteract.API.Patches;

using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using AudioInteract.Features;
using Exiled.API.Features;
using Exiled.API.Features.Pools;
using HarmonyLib;
using RemoteAdmin.Communication;
using static HarmonyLib.AccessTools;

/// <summary>
/// Show NPC in remote admin panel.
/// </summary>
[HarmonyPatch(typeof(RaPlayerList), nameof(RaPlayerList.ReceiveData), [typeof(CommandSender), typeof(string)])]
public static class RAPlayerListShowNPC
{
    /// <summary>
    /// ....
    /// </summary>
    /// <param name="instructions">.</param>
    /// <param name="generator">..</param>`
    /// <returns>...</returns>
    /// don't work if patch is simply deny by prefix :(.
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Pool.Get(instructions);

        int offset = 0;

        var index = newInstructions.FindLastIndex(x => x.opcode == OpCodes.Callvirt && x.Calls(Method(typeof(StringBuilder), nameof(StringBuilder.AppendLine)))) + offset;

        newInstructions.InsertRange(
            index,
            new CodeInstruction[]
            {
                // loads stringbuilder in stack
                new CodeInstruction(OpCodes.Ldloc_S, 7),

                // execute method (idk how to work with foreach in transpiler)
                new CodeInstruction(OpCodes.Call, Method(typeof(RAPlayerListShowNPC), nameof(AddNPC))),
            });

        for (int z = 0; z < newInstructions.Count; z++)
        {
            yield return newInstructions[z];
        }

        ListPool<CodeInstruction>.Pool.Return(newInstructions);
    }

    /// <summary>
    /// Show NPC in RA List.
    /// </summary>
    /// <param name="stringBuilder">String Builder.</param>
    public static void AddNPC(StringBuilder stringBuilder)
    {
        try
        {
            foreach (MusicInstance musicInstance in MusicAPI.MusicInstances)
            {
                if (musicInstance.Npc.ReferenceHub.Mode != CentralAuth.ClientInstanceMode.DedicatedServer)
                {
                    // if any other mode, there already displayed.
                    continue;
                }

                stringBuilder.AppendLine($"({musicInstance.Npc.Id}) {musicInstance.Npc.ReferenceHub.nicknameSync.CombinedName.Replace("\n", string.Empty).Replace("RA_", string.Empty)}");
            }
        }
        catch (System.Exception ex)
        {
            Log.Error(ex);
        }
    }
}