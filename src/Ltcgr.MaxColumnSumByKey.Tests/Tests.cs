using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutofacContrib.NSubstitute;
using Xunit;
using Ltcgr.IO;
using Ltcgr.MaxColumnSumByKey.Tests.Properties;
using NSubstitute;

namespace Ltcgr.MaxColumnSumByKey.Tests
{
    public class Tests
    {
        [Fact]
        public void ProcessFile_Returns5000and21_Given1and2forSet1()
        {
            const int expectedKey = 5000;
            const int expectedTotal = 21;

            var autoSubstitute = new AutoSubstitute();

            autoSubstitute
                .Resolve<IFile>()
                .OpenText(string.Empty)
                .Returns(new StreamReader(new MemoryStream(Resources.set1)));

            var result = autoSubstitute
                .Resolve<Processor>()
                .ProcessFile(string.Empty, 1, 2);

            Assert.Equal(expectedKey, result.Key);
            Assert.Equal(expectedTotal, result.Value);
        }
    }
}
