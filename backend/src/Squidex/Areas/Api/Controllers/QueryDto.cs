﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using System.Text.Json;
using System.Text.Json.Serialization;
using Squidex.Domain.Apps.Entities;
using Squidex.Infrastructure;

namespace Squidex.Areas.Api.Controllers
{
    public sealed class QueryDto
    {
        /// <summary>
        /// The optional list of ids to query.
        /// </summary>
        public DomainId[]? Ids { get; set; }

        /// <summary>
        /// The optional odata query.
        /// </summary>
        public string? OData { get; set; }

        /// <summary>
        /// The optional json query.
        /// </summary>
        [JsonPropertyName("q")]
        public JsonDocument? JsonQuery { get; set; }

        /// <summary>
        /// The parent id (for assets).
        /// </summary>
        public DomainId? ParentId { get; set; }

        public Q ToQuery()
        {
            var result = Q.Empty;

            if (Ids != null)
            {
                result = result.WithIds(Ids);
            }

            if (OData != null)
            {
                result = result.WithODataQuery(OData);
            }

            if (JsonQuery != null)
            {
                result = result.WithJsonQuery(JsonQuery.RootElement.ToString());
            }

            return result;
        }
    }
}
