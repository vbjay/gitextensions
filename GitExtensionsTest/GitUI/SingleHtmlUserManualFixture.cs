using System;
using FluentAssertions;
using GitUI.UserManual;
using NUnit.Framework;

namespace GitExtensionsTest.GitUI
{
    [TestFixture]
    public class SingleHtmlUserManualFixture
    {
        [TestCase((string)null)]
        [TestCase("merge-conflicts")]
        public void GetUrl(string anchor)
        {
            var sut = new SingleHtmlUserManual(anchor);

            var expected = SingleHtmlUserManual.Location + "/index.html".Combine("#", anchor);

            sut.GetUrl().Should().Be(expected);
        }
    }
}
