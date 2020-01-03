﻿using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace ListPool.Benchmarks
{
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    [GcServer(true)]
    [GcConcurrent]
    public class EnumerableToListPoolBenchmark
    {
        private Enumerable<int> _items;

        [Params(10, 50, 100, 1000)]
        public int N { get; set; }

        [IterationSetup]
        public void IterationSetup()
        {
            int[] items = new int[N];

            for (int i = 0; i < N - 1; i++)
            {
                items[i] = 1;
            }

            _items = new Enumerable<int>(items);
        }

        [Benchmark]
        public void ListPool()
        {
            using var _ = _items.ToListPool();
        }

        [Benchmark]
        public void ListPoolValue()
        {
            using var _ = _items.ToListPoolValue();
        }

        [Benchmark(Baseline = true)]
        public void Linq()
        {
            _ = _items.ToList();
        }
    }
}
