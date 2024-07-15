// <copyright file="RAPlayerListShowNPC.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.API.Patches;

using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Exiled.API.Features;
using Exiled.API.Features.Pools;
using HarmonyLib;
using RemoteAdmin.Communication;
using static HarmonyLib.AccessTools;
using StringBuilderPool = NorthwoodLib.Pools.StringBuilderPool;

/// <summary>
/// Show NPC in remote admin panel.
/// </summary>
[HarmonyPatch(typeof(RaPlayerList), nameof(RaPlayerList.ReceiveData), [typeof(CommandSender), typeof(string)])]
public static class RAPlayerListShowNPC
{
    /// <summary/>
    /// <param name="instructions">.</param>
    /// <param name="generator">..</param>
    /// <returns>fuck docs, please fix summary for this method.</returns>
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Pool.Get(instructions);

        int index = newInstructions.FindIndex(instruction =>
            instruction.opcode == OpCodes.Callvirt
            && (MethodInfo)instruction.operand == Method(typeof(StringBuilder), nameof(StringBuilderPool.Shared.Rent)));

        index += -1;

        newInstructions.InsertRange(
            84,
            new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldloc_S, 7),
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
            foreach (Features.MusicInstance musicInstance in AudioPlayer.Features.API.MusicInstance)
            {
                stringBuilder.AppendLine($"({musicInstance.Npc.Id}) {musicInstance.Npc.ReferenceHub.nicknameSync.CombinedName.Replace("\n", string.Empty).Replace("RA_", string.Empty)}");
            }
        }
        catch (System.Exception ex)
        {
            Log.Error(ex);
        }
    }
}