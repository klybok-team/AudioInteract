// <copyright file="Extensions.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.API.Features;

using Exiled.API.Features;
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
    /// <param name="editedFile">Gets the edited file if he was edited.</param>
    /// <returns>Is file exists and ready to be played.</returns>
    public static bool CheckTrack(AudioFile audioFile, out AudioFile? editedFile)
    {
        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }

        if (!File.Exists(audioFile.FilePath))
        {
            editedFile = null;
            return false;
        }

        if (Path.GetExtension(audioFile.FilePath) != ".ogg")
        {
            VorbisReader vorbisReader = new(audioFile.FilePath);

            Log.Info(vorbisReader.Channels);
            Log.Info(vorbisReader.NominalBitrate);

            editedFile = ConvertToOgg(audioFile);
            return editedFile != null;
        }

        editedFile = null;
        return true;
    }

    /// <summary>
    /// Convert file to playable track.
    /// </summary>
    /// <param name="audioFile">AudioFile to convert.</param>
    /// <returns>Returns null if file wasn't be converted. Returns new file class with edited path if was.</returns>
    public static AudioFile? ConvertToOgg(AudioFile audioFile)
    {
        return null;
    }
}
