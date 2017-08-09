using System;
using System.Diagnostics;
using System.IO;
using AutofacContrib.NSubstitute;
using Ltcgr.IO;
using Ltcgr.MaxColumnSumByKey.Tests.Properties;
using NSubstitute;
using Xunit;

namespace Ltcgr.MaxColumnSumByKey.Tests
{
    public class Tests
    {
        [Theory]
        [InlineData("set1", 1, 2, 5000, 21)]
        public void ProcessFile_ReturnsExpected_GivenInputs(
            string set, int keyPosition, int valuePosition, int expectedKey, int expectedTotal)
        {
            var autoSubstitute = new AutoSubstitute();

            var setBytes = (set == "set2") ? Resources.set2 : Resources.set1;

            autoSubstitute
                .Resolve<IFile>()
                .OpenText(string.Empty)
                .Returns(new StreamReader(new MemoryStream(setBytes)));

            var actual = autoSubstitute
                .Resolve<Processor>()
                .ProcessFile(string.Empty, keyPosition, valuePosition);
            
            Assert.Equal(expectedKey, actual.Key);
            Assert.Equal(expectedTotal, actual.Value);
        }

        [Fact]
        public void CreateRecordMap_ReturnsPropMapsForKeyAndValue_WhenCalled()
        {
            var autoSubstitute = new AutoSubstitute();

            autoSubstitute
                .Resolve<IFile>();

            var actual = autoSubstitute
                .Resolve<Processor>()
                .CreateRecordMap(1, 2);

            Assert.Equal(actual.PropertyMaps[0].Data.Index, 1); // Key Pos
            Assert.Equal(actual.PropertyMaps[1].Data.Index, 2); // Value Pos
        }

        [Fact]
        public void ProcessFile_ReturnsInLessThan2Sec_Given1and2forSet1()
        {
            var timer = new Stopwatch();
            timer.Start();

            ProcessFile_ReturnsExpected_GivenInputs("set1", 1, 2, 5000, 21);

            timer.Stop();

            Console.WriteLine(timer.ElapsedMilliseconds);

            Assert.True(timer.ElapsedMilliseconds < 2000);
        }

        [Fact]
        public void ProcessFile_ReturnsInLessThan30Sec_Given1and2forSet2()
        {
            var timer = new Stopwatch();
            timer.Start();

            ProcessFile_ReturnsExpected_GivenInputs("set2", 1, 2, 2006, 22569013);

            timer.Stop();

            Console.WriteLine(timer.ElapsedMilliseconds);

            Assert.True(timer.ElapsedMilliseconds < 30000);
        }
    }
}