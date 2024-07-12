// <copyright file="Extensions.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.API.Features;

using Exiled.API.Features;
using Exiled.CreditTags;
using NVorbis;

/// <summary>
/// A collection of API methods.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Gets path to audio file root directory. Leads to EXILED root directory (EXILED/Audio).
    /// </summary>
    public static string FolderPath { get; } = Path.Combine(Paths.Exiled, "Audio", "track.ogg");

    /// <summary>
    /// Checks the path for playable track.
    /// </summary>
    /// <param name="audioFile">AudioFile to check.</param>
    /// <returns>Is file exists and ready to be played.</returns>
    public static bool CheckTrack(AudioFile audioFile)
    {
        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }

        if (!File.Exists(audioFile.FilePath))
        {
            return false;
        }

        if (Path.GetExtension(audioFile.FilePath) != ".ogg")
        {
            VorbisReader vorbisReader = new(audioFile.FilePath);

            Log.Info(vorbisReader.Channels);
            Log.Info(vorbisReader.NominalBitrate);

            return ConvertToOgg(audioFile);
        }

        return true;
    }

    /// <summary>
    /// Convert file to playable track.
    /// </summary>
    /// <param name="audioFile">AudioFile to convert.</param>
    /// <returns>Is file can be converted and ready to be played.</returns>
    public static bool ConvertToOgg(AudioFile audioFile)
    {

        return true;
    }
}
