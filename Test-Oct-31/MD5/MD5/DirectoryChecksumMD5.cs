// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace DirectoryChecksumMD5;

using System.Text;
using System.Security.Cryptography;

public class DirectoryChecksumMD5
{
    public static byte[] GetDirectoryMD5Sequentially(string directoryPath)
    {
        var dir = new DirectoryInfo(directoryPath);
        return GetDirectoryMD5Sequentially(dir);
    }

    public static async Task<byte[]> GetDirectoryMD5Async(string directoryPath)
    {
        var dir = new DirectoryInfo(directoryPath);
        return await GetDirectoryMD5Async(dir);
    }

    private static byte[] GetDirectoryMD5Sequentially(DirectoryInfo directoryInfo)
    {
        var result = new List<byte>();
        foreach (var subDirectoryInfo in directoryInfo.EnumerateDirectories())
        {
            foreach (var @byte in GetDirectoryMD5Sequentially(subDirectoryInfo))
            {
                result.Add(@byte);
            }
        }

        foreach (var fileInfo in directoryInfo.EnumerateFiles())
        {
            foreach (var @byte in GetStringMD5(fileInfo))
            {
                result.Add(@byte);
            }
        }

        return result.ToArray();
    }

    private static async Task<byte[]> GetDirectoryMD5Async(DirectoryInfo directoryInfo)
    {
        var tasks = new List<Task<byte[]>>();

        foreach (var subDirectoryInfo in directoryInfo.EnumerateDirectories())
        {
            tasks.Add(Task.Run(() => GetDirectoryMD5Async(subDirectoryInfo)));
        }

        foreach (var fileInfo in directoryInfo.EnumerateFiles())
        {
            tasks.Add(Task.Run(() => GetStringMD5(fileInfo)));
        }

        var result = new List<byte>();
        foreach (var task in tasks)
        {
            result.AddRange(await task);
        }

        return result.ToArray();
    }

    private static byte[] GetStringMD5(FileInfo fileInfo) =>
        MD5.HashData(Encoding.UTF8.GetBytes(fileInfo.OpenText().ReadToEnd()));
}
