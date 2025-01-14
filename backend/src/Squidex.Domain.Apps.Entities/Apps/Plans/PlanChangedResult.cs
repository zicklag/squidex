﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

#pragma warning disable SA1313 // Parameter names should begin with lower-case letter

namespace Squidex.Domain.Apps.Entities.Apps.Plans
{
    public sealed record PlanChangedResult(string PlanId, bool Unsubscribed = false, Uri? RedirectUri = null)
    {
    }
}
