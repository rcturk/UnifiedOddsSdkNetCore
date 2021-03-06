﻿/*
* Copyright (C) Sportradar AG. See LICENSE for full license governing this code
*/
using System;
using Dawn;

namespace Sportradar.OddsFeed.SDK.API.Internal.Config
{
    /// <summary>
    /// Class TokenSetter
    /// </summary>
    /// <seealso cref="ITokenSetter" />
    internal class TokenSetter : ITokenSetter
    {
        /// <summary>
        /// A <see cref="IConfigurationSectionProvider"/> instance used to access <see cref="IOddsFeedConfigurationSection"/>
        /// </summary>
        private readonly IConfigurationSectionProvider _configurationSectionProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenSetter"/> class
        /// </summary>
        /// <param name="configurationSectionProvider">A <see cref="IConfigurationSectionProvider"/> instance used to access <see cref="IOddsFeedConfigurationSection"/></param>
        internal TokenSetter(IConfigurationSectionProvider configurationSectionProvider)
        {
            Guard.Argument(configurationSectionProvider, nameof(configurationSectionProvider)).NotNull();

            _configurationSectionProvider = configurationSectionProvider;
        }

        /// <summary>
        /// Sets the access token used to access feed resources (AMQP broker, Sports API, ...)
        /// </summary>
        /// <param name="accessToken">The access token used to access feed resources</param>
        /// <returns>The <see cref="IEnvironmentSelector" /> instance allowing the selection of target environment</returns>
        /// <exception cref="ArgumentException">Value cannot be a null reference or empty string - accessToken</exception>
        public IEnvironmentSelector SetAccessToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Value cannot be a null reference or empty string", nameof(accessToken));
            }
            return new EnvironmentSelector(accessToken, _configurationSectionProvider);
        }

        /// <summary>
        /// Sets the access token used to access feed resources (AMQP broker, Sports API, ...) to value read from configuration file
        /// </summary>
        /// <returns>The <see cref="IEnvironmentSelector" /> instance allowing the selection of target environment</returns>
        public IEnvironmentSelector SetAccessTokenFromConfigFile()
        {
            return new EnvironmentSelector(_configurationSectionProvider.GetSection().AccessToken, _configurationSectionProvider);
        }
    }
}
