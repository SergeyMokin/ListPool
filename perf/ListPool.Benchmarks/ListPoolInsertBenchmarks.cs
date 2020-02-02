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
    public class ListPoolInsertBenchmarks
    {
        private List<int> _list;
        private ListPool<int> _listPool;
        private ValueListPool<int> _valueListPool;

        [Params(100, 1000, 10000)]
        public int N { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _list = new List<int>(N);
            _listPool = new ListPool<int>(N);
            _valueListPool = new ValueListPool<int>(N);
            for (int i = 1; i <= N; i++)
            {
                _list.Add(i);
                _listPool.Add(i);
                _valueListPool.Add(i);
            }
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _listPool.Dispose();
            _valueListPool.Dispose();
        }

        [Benchmark(Baseline = true)]
        public void List()
        {
            _list.Insert(N / 2, 22222);
        }

        [Benchmark]
        public void ListPool()
        {
            _listPool.Insert(N / 2, 22222);
        }

        [Benchmark]
        public void ValueListPool()
        {
            _valueListPool.Insert(N / 2, 22222);
        }
    }
}
