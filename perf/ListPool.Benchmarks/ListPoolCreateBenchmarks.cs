﻿using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace ListPool.Benchmarks
{
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    [GcServer(true)]
    [GcConcurrent]
    public class ListPoolCreateBenchmarks
    {
        [Params(50, 60, 70, 80, 100, 1000, 10000)]
        public int N { get; set; }

        [Benchmark(Baseline = true)]
        public void List()
        {
            _ = new List<int>(N);
        }

        [Benchmark]
        public void ListPool()
        {
            using var list = new ListPool<int>(N);
        }

        [Benchmark]
        public void ListPoolValue()
        {
            using var list = new ValueListPool<int>(N);
        }
    }
}
