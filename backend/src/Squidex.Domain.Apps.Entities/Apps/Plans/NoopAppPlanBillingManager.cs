﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Squidex.Infrastructure;

namespace Squidex.Domain.Apps.Entities.Apps.Plans
{
    public sealed class NoopAppPlanBillingManager : IAppPlanBillingManager
    {
        public bool HasPortal
        {
            get => false;
        }

        public Task<string> GetPortalLinkAsync(string userId,
            CancellationToken ct = default)
        {
            return Task.FromResult(string.Empty);
        }

        public Task<Uri?> MustRedirectToPortalAsync(string userId, NamedId<DomainId> appId, string? planId,
            CancellationToken ct = default)
        {
            return Task.FromResult<Uri?>(null);
        }

        public Task SubscribeAsync(string userId, NamedId<DomainId> appId, string planId,
            CancellationToken ct = default)
        {
            return Task.CompletedTask;
        }

        public Task UnsubscribeAsync(string userId, NamedId<DomainId> appId,
            CancellationToken ct = default)
        {
            return Task.CompletedTask;
        }
    }
}
