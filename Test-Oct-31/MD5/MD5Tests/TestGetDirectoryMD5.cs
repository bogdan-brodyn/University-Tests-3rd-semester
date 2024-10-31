// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace MD5Tests;

using System.Threading.Tasks;
using DirectoryChecksumMD5;

public class TestGetDirectoryMD5
{
    [Test]
    public async Task TestGetDirectoryMD5HashAsync_ResultsMustBeTheSame()
    {
        var checkSumParallel = await DirectoryChecksumMD5.GetDirectoryMD5Async(Environment.CurrentDirectory);
        var checkSumSequentially = DirectoryChecksumMD5.GetDirectoryMD5Sequentially(Environment.CurrentDirectory);

        CollectionAssert.AreEqual(expected: checkSumSequentially, actual: checkSumParallel);
    }
}
