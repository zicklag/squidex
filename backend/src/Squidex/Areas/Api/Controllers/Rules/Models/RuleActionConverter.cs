﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Squidex.Domain.Apps.Core.Rules;
using Squidex.Web.Json;

namespace Squidex.Areas.Api.Controllers.Rules.Models
{
    public sealed class RuleActionConverter : JsonInheritanceConverter<RuleAction>
    {
        public static IReadOnlyDictionary<string, Type> Mapping { get; set; }

        public RuleActionConverter()
            : base("actionType", Mapping)
        {
        }
    }
}
