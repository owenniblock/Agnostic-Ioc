﻿// --------------------------------------------------------------------------------------------------------------------
// Copyright (c) 2014, Simon Proctor and Nathanael Mann
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Cardinal.IoC.StructureMap;
using Cardinal.IoC.UnitTests.Helpers;
using NUnit.Framework;
using StructureMap;

namespace Cardinal.IoC.UnitTests.ResolutionTests
{
    public class StructureMapResolutionTests : IResolutionTestSuite
    {
        [Test]
        public void ResolveComponentByInterfaceOnly()
        {
            IContainerManager containerManager = new ContainerManager(TestConstants.StructureMapContainerName);
            IDependantClass dependency = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass), dependency.GetType());
        }

        [Test]
        public void ResolveComponentByName()
        {
            IContainerManager containerManager = new ContainerManager(TestConstants.StructureMapContainerName);
            IDependantClass dependency = containerManager.Resolve<IDependantClass>("DependentClass3");
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass), dependency.GetType());
        }

        [Test]
        public void ResolveComponentWithParameters()
        {
            IContainerManager containerManager = new ContainerManager(TestConstants.StructureMapContainerName);
            IDependantClass dependency = containerManager.Resolve<IDependantClass>(new Dictionary<string, object>());
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass), dependency.GetType());
        }

        [Test]
        public void ResolveComponentWithNameAndParameters()
        {
            IContainerManager containerManager = new ContainerManager(TestConstants.StructureMapContainerName);
            IDependantClass dependency = containerManager.Resolve<IDependantClass>("DependentClass3", new Dictionary<string, object>());
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass), dependency.GetType());
        }

        [Test]
        public void UseExternalContainer()
        {
            IContainer container = new Container();
            container.Configure(x => x.For<IDependantClass>().Use<DependantClass>());
            string containerKey = Guid.NewGuid().ToString();
            ContainerManager containerManager = new ContainerManager(new StructureMapContainerAdapter(containerKey, container));
            IDependantClass dependency = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass), dependency.GetType());
        }

        [Test]
        public void ResolveAll()
        {
            IContainer container = new Container();
            string containerKey = Guid.NewGuid().ToString();
            IContainerManager containerManager = new ContainerManager(new StructureMapContainerAdapter(containerKey, container));
            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            containerManager.Adapter.Register<IDependantClass, DependantClass2>();
            var resolved = containerManager.ResolveAll<IDependantClass>();
            Assert.Greater(resolved.Count(), 0);
        }
    }
}