﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Squidex.Domain.Apps.Core.Tags;
using Squidex.Domain.Apps.Entities.Apps;
using Squidex.Infrastructure;
using Squidex.Infrastructure.States;
using Squidex.Infrastructure.UsageTracking;

#pragma warning disable CS0649

namespace Squidex.Domain.Apps.Entities.Assets
{
    public partial class AssetUsageTracker : IAssetUsageTracker, IDeleter
    {
        private const string CounterTotalCount = "TotalAssets";
        private const string CounterTotalSize = "TotalSize";
        private static readonly DateTime SummaryDate;
        private readonly IAssetLoader assetLoader;
        private readonly ISnapshotStore<State> store;
        private readonly ITagService tagService;
        private readonly IUsageTracker usageTracker;

        [CollectionName("Index_TagHistory")]
        public sealed class State
        {
            public HashSet<string>? Tags { get; set; }
        }

        public AssetUsageTracker(IUsageTracker usageTracker, IAssetLoader assetLoader, ITagService tagService,
            ISnapshotStore<State> store)
        {
            this.assetLoader = assetLoader;
            this.tagService = tagService;
            this.store = store;
            this.usageTracker = usageTracker;

            ClearCache();
        }

        Task IDeleter.DeleteAppAsync(IAppEntity app,
            CancellationToken ct)
        {
            var key = GetKey(app.Id);

            return usageTracker.DeleteAsync(key, ct);
        }

        public async Task<long> GetTotalSizeAsync(DomainId appId)
        {
            var key = GetKey(appId);

            var counters = await usageTracker.GetAsync(key, SummaryDate, SummaryDate, null);

            return counters.GetInt64(CounterTotalSize);
        }

        public async Task<IReadOnlyList<AssetStats>> QueryAsync(DomainId appId, DateTime fromDate, DateTime toDate)
        {
            var enriched = new List<AssetStats>();

            var usages = await usageTracker.QueryAsync(GetKey(appId), fromDate, toDate);

            if (usages.TryGetValue(usageTracker.FallbackCategory, out var byCategory1))
            {
                AddCounters(enriched, byCategory1);
            }
            else if (usages.TryGetValue("Default", out var byCategory2))
            {
                // Fallback for older versions where default was uses as tracking category.
                AddCounters(enriched, byCategory2);
            }

            return enriched;
        }

        private static void AddCounters(List<AssetStats> enriched, List<(DateTime, Counters)> details)
        {
            foreach (var (date, counters) in details)
            {
                var totalCount = counters.GetInt64(CounterTotalCount);
                var totalSize = counters.GetInt64(CounterTotalSize);

                enriched.Add(new AssetStats(date, totalCount, totalSize));
            }
        }
    }
}
