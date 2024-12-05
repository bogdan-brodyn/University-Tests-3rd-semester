﻿// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace Reflector;

using System.Reflection;

/// <summary>
/// Represents some reflection-based functionality.
/// </summary>
/// <param name="workingDirectory">The directory to be used in files' paths.</param>
public class Reflector(string workingDirectory)
{
    private readonly string workingDirectory = workingDirectory;

    /// <summary>
    /// Allows one to print a class provided.
    /// </summary>
    /// <param name="someClass">The class provided.</param>
    public void PrintStructure(Type someClass)
    {
        using var streamWriter = File.CreateText(
            Path.Combine(this.workingDirectory, $"{someClass.Name}.txt"));

        streamWriter.WriteLine($"class {someClass.Name}");
        streamWriter.WriteLine("{");

        foreach (var memberInfo in someClass.GetTypeInfo().DeclaredMembers)
        {
            var memberName = GetToStringPresentation(memberInfo);
            streamWriter.WriteLine($"    {memberName}: {memberInfo}");
        }

        streamWriter.WriteLine("}");
    }

    /// <summary>
    /// Allows one to compare classes provided.
    /// </summary>
    /// <param name="a">The first class provided.</param>
    /// <param name="b">The second class provided.</param>
    public void DiffClasses(Type a, Type b)
    {
        using var streamWriter = File.CreateText(
            Path.Combine(this.workingDirectory, $"Diff_{a.Name}-{b.Name}.txt"));
        streamWriter.WriteLine($"Unique for: {a.Name}");
        DiffClasses(streamWriter, a, b, '+');
        streamWriter.WriteLine();
        streamWriter.WriteLine($"Unique for: {b.Name}");
        DiffClasses(streamWriter, b, a, '-');
    }

    private static void DiffClasses(StreamWriter streamWriter, Type a, Type b, char @char)
    {
        foreach (var aMemberInfo in a.GetTypeInfo().DeclaredMembers)
        {
            var isUnique = true;
            foreach (var bMemberInfo in b.GetTypeInfo().DeclaredMembers)
            {
                if (aMemberInfo == bMemberInfo)
                {
                    isUnique = false;
                    break;
                }
            }

            if (isUnique)
            {
                var memberName = GetToStringPresentation(aMemberInfo);
                streamWriter.WriteLine($"{@char} {memberName}: {aMemberInfo}");
            }
        }
    }

    private static string GetToStringPresentation(MemberInfo memberInfo)
    {
        return memberInfo switch
        {
            FieldInfo => "FieldInfo",
            MethodInfo => "MethodInfo",
            ConstructorInfo => "ConstructorInfo",
            _ => string.Empty
        };
    }
}
