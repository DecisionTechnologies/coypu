﻿using System;
using Coypu.Finders;
using Coypu.Tests.TestBuilders;
using Coypu.Tests.TestDoubles;
using NUnit.Framework;

namespace Coypu.Tests.When_interacting_with_the_browser
{
    [TestFixture]
    public class When_finding_single_elements
    {
        protected IDriver driver;
        protected BrowserSession browserSession;
        protected SessionConfiguration sessionConfiguration;

        [SetUp]
        public void SetUp()
        {
            driver = new StubDriver();
            sessionConfiguration = new SessionConfiguration{ConsiderInvisibleElements = true, RetryInterval = TimeSpan.FromMilliseconds(123)};
            browserSession = TestSessionBuilder.Build(sessionConfiguration, driver, null, null,null, null);
        }
        
        [Test]
        public void FindButton_Should_find_robust_scope()
        {
            Should_find_robust_scope<ButtonFinder, ElementScope, SynchronisedElementScope>(browserSession.FindButton);
        }
        
        [Test]
        public void FindLink_Should_find_robust_scope()
        {
            Should_find_robust_scope<LinkFinder, ElementScope, SynchronisedElementScope>(browserSession.FindLink);
        }

        [Test]
        public void FindField_Should_find_robust_scope()
        {
            Should_find_robust_scope<FieldFinder, ElementScope, SynchronisedElementScope>(browserSession.FindField);
        }

        [Test]
        public void FindCss_Should_find_robust_scope()
        {
            Should_find_robust_scope<CssFinder, ElementScope, SynchronisedElementScope>(browserSession.FindCss);
        }

        [Test]
        public void FindId_Should_find_robust_scope()
        {
            Should_find_robust_scope<IdFinder, ElementScope, SynchronisedElementScope>(browserSession.FindId);
        }

        [Test]
        public void FindSection_Should_find_robust_scope()
        {
            Should_find_robust_scope<SectionFinder, ElementScope, SynchronisedElementScope>(browserSession.FindSection);
        }

        [Test]
        public void FindFieldset_Should_find_robust_scope()
        {
            Should_find_robust_scope<FieldsetFinder, ElementScope, SynchronisedElementScope>(browserSession.FindFieldset);
        }

        [Test]
        public void FindWindow_Should_find_robust_scope()
        {
            Should_find_robust_scope<WindowFinder, BrowserWindow, BrowserWindow>(browserSession.FindWindow);
        }

        [Test]
        public void FindFrame_Should_find_robust_scope()
        {
            Should_find_robust_scope<FrameFinder, ElementScope, SynchronisedElementScope>(browserSession.FindFrame);
        }

        [Test]
        public void FindXPath_Should_find_robust_scope()
        {
            Should_find_robust_scope<XPathFinder,ElementScope,SynchronisedElementScope>(browserSession.FindXPath);
        }

        private void Should_find_robust_scope<T,TScope,TReturn>(Func<string, Options, TScope> findButton) where TScope : DriverScope
        {
            var options = new Options { TextPrecision = TextPrecision.Exact, Timeout = TimeSpan.FromMilliseconds(999) };
            var scope = findButton("Some locator", options);
            Assert.That(scope, Is.TypeOf<TReturn>());

            GetValue<T>(scope, options);
        }

        private void GetValue<T>(DriverScope scope, Options options)
        {
            Assert.That(scope.OuterScope, Is.SameAs(browserSession));
            Assert.That(scope.ElementFinder, Is.TypeOf<T>());
            Assert.That(scope.ElementFinder.Driver, Is.EqualTo(driver));
            Assert.That(scope.ElementFinder.Locator, Is.EqualTo("Some locator"));
            Assert.That(scope.ElementFinder.Options, Is.EqualTo(Options.Merge(options, sessionConfiguration)));
        }
    }
}