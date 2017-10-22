#region License
// Copyright (c) Niklas Wendel 2016-2017
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
#endregion
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using FluentRegistration.Tests.Issues.Issue_2.Classes;

namespace FluentRegistration.Tests.Issues.Issue_2
{

    /// <summary>
    /// 
    /// </summary>
    public class Tests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact(Skip = "Something is strange with open generic types of open generic types")]
        public void CanInstantiate()
        {
            var openValidatorType = typeof(IValidator<>);
            var openContentAwareCommandType = typeof(IContentAwareCommand<,>);
            var openContentAwareCommandValidatorType = openValidatorType.MakeGenericType(openContentAwareCommandType);

            // ISSUE: Cannot close type, IsGenericTypeDefintion returns false
            var closedType = openContentAwareCommandValidatorType.MakeGenericType(typeof(AbstractPageData), typeof(AbstractPageExtensionProperties));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact(Skip = "Something is strange with open generic types of open generic types")]
        public void CanRegister()
        {
            var tested = new ServiceCollection();

            tested.Register(r => r
                .FromThisAssembly()
                .Where(c => c.InSameNamespaceAs<IValidator>() && c.AssignableTo<IValidator>())
                .WithServices.AllInterfaces());

            // ISSUE: Does not compile
            //Assert.Contains(tested, x => x.ServiceType == typeof(IValidator<IContentAwareCommand<,>>));

            var openValidatorType = typeof(IValidator<>);
            var openContentAwareCommandType = typeof(IContentAwareCommand<,>);
            var openContentAwareCommandValidatorType = openValidatorType.MakeGenericType(openContentAwareCommandType);

            // ISSUE: Returns a different type than the one created above
            var registeredServiceType = tested.Single(x => x.ImplementationType == typeof(ContentAwareCommandValidator<,>)).ServiceType;

            Assert.Contains(tested, x => x.ServiceType == openContentAwareCommandValidatorType);
            Assert.Contains(tested, x => x.ImplementationType == typeof(ContentAwareCommandValidator<,>));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact(Skip = "Something is strange with open generic types of open generic types")]
        public void CanResolve()
        {
            var tested = new ServiceCollection();
            tested.Register(r => r
                .FromThisAssembly()
                .Where(c => c.InSameNamespaceAs<IValidator>() && c.AssignableTo<IValidator>())
                .WithServices.AllInterfaces());
            var serviceProvider = tested.BuildServiceProvider();

            // ISSUE: Cannot instantiate implementation type
            var resolved = serviceProvider.GetService<IValidator<IContentAwareCommand<AbstractPageData, AbstractPageExtensionProperties>>>();

            Assert.NotNull(resolved);
        }

    }

}
