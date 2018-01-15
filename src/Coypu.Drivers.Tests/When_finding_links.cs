﻿using Shouldly;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    public class When_finding_links : DriverSpecs
    {
        [Test]
        public void Finds_link_by_text()
        {
            Link("first link").Id.ShouldBe("firstLinkId");
            Link("second link").Id.ShouldBe("secondLinkId");
        }
        [Test]
        public void Finds_link_by_href()
        {
            Link("#link1href").Id.ShouldBe("firstLinkId");
            Link("#link2href").Id.ShouldBe("secondLinkId");
        }

        [Test]
        public void Does_not_find_display_none()
        {
            Assert.Throws<MissingHtmlException>(() => Link("I am an invisible link by display"));
        }

        [Test]
        public void Does_not_find_visibility_hidden_links()
        {
            Assert.Throws<MissingHtmlException>(() => Link("I am an invisible link by visibility"));
        }

        [Test]
        public void Finds_a_link_with_both_types_of_quote_in_its_text()
        {
            Assert.That(Link("I'm a link with \"both\" types of quote in my text").Id, Is.EqualTo("linkWithBothQuotesId"));
        }
    }
}