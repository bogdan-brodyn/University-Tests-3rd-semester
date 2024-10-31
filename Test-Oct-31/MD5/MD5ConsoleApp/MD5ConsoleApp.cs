// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

#pragma warning disable SA1200 // Using directives should be placed correctly
using System.Diagnostics;
using System.Text;
#pragma warning restore SA1200 // Using directives should be placed correctly

var stopwatchParallel = new Stopwatch();
stopwatchParallel.Start();
var checkSumParallel = await DirectoryChecksumMD5.DirectoryChecksumMD5.GetDirectoryMD5Async(Environment.CurrentDirectory);
stopwatchParallel.Stop();

Console.WriteLine("##############################################################################");
Console.WriteLine("Parallel get working directory checksum method results:");
Console.WriteLine($"Time: {stopwatchParallel.ElapsedMilliseconds}");
Console.WriteLine("Result:");
Console.WriteLine(Encoding.UTF8.GetString(checkSumParallel, 0, checkSumParallel.Length));
Console.WriteLine();

var stopwatchSequentially = new Stopwatch();
stopwatchSequentially.Start();
var checkSumSequentially = DirectoryChecksumMD5.DirectoryChecksumMD5.GetDirectoryMD5Sequentially(Environment.CurrentDirectory);
stopwatchSequentially.Stop();

Console.WriteLine("##############################################################################");
Console.WriteLine("Sequential get working directory checksum method results:");
Console.WriteLine($"Time: {stopwatchSequentially.ElapsedMilliseconds}");
Console.WriteLine("Result:");
Console.WriteLine(Encoding.UTF8.GetString(checkSumSequentially, 0, checkSumSequentially.Length));
Console.WriteLine();
