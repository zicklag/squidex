﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Squidex.Infrastructure;

namespace Squidex.Domain.Apps.Entities.Apps.Commands
{
    public abstract class AppUpdateCommand : AppCommand, IAppCommand
    {
        public NamedId<DomainId> AppId { get; set; }

        public override DomainId AggregateId
        {
            get => AppId.Id;
        }
    }
}
