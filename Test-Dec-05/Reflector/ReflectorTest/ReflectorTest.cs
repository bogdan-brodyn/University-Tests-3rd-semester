// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace ReflectorTest;

using Reflector;

[TestFixture]
public class ReflectorTest
{
    private readonly Reflector reflector;
    private readonly string workingDirectory = "../../../";

    public ReflectorTest()
    {
        this.reflector = new (this.workingDirectory);
    }

    [Test]
    public void PrintStructureTest()
    {
        this.reflector.PrintStructure(typeof(Reflector));
        var actualText = File.ReadAllText(Path.Combine(this.workingDirectory, "Reflector.txt"));
        var expectedText = File.ReadAllText(Path.Combine(this.workingDirectory, "Expected_Reflector.txt"));
        Assert.That(actual: actualText, Is.EqualTo(expected: expectedText));
    }

    [Test]
    public void DiffClassesTest()
    {
        this.reflector.DiffClasses(typeof(Reflector), typeof(ReflectorTest));
        var actualText = File.ReadAllText(Path.Combine(this.workingDirectory, "Diff_Reflector-ReflectorTest.txt"));
        var expectedText = File.ReadAllText(Path.Combine(this.workingDirectory, "Expected_Diff_Reflector-ReflectorTest.txt"));
        Assert.That(actual: actualText, Is.EqualTo(expected: expectedText));
    }
}
