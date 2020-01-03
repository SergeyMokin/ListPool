``` ini

BenchmarkDotNet=v0.12.0, OS=elementary 5.1
Intel Core i7-4702MQ CPU 2.20GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.100
  [Host]     : .NET Core 3.1.0 (CoreCLR 4.700.19.56106, CoreFX 4.700.19.56202), X64 RyuJIT
  Job-FHWRME : .NET Core 3.1.0 (CoreCLR 4.700.19.56106, CoreFX 4.700.19.56202), X64 RyuJIT

Concurrent=True  Server=True  InvocationCount=1  
UnrollFactor=1  

```
|        Method |    N |      Mean |    Error |   StdDev |    Median | Ratio | RatioSD | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |----- |----------:|---------:|---------:|----------:|------:|--------:|-----:|------:|------:|------:|----------:|
| ListPoolValue |   10 |  9.399 us | 2.280 us | 6.432 us |  6.245 us |  1.25 |    1.02 |    1 |     - |     - |     - |      32 B |
|      ListPool |   10 |  9.945 us | 2.399 us | 6.922 us |  7.126 us |  1.61 |    1.74 |    1 |     - |     - |     - |      88 B |
|          Linq |   10 | 10.591 us | 2.613 us | 7.413 us |  7.939 us |  1.00 |    0.00 |    1 |     - |     - |     - |     248 B |
|               |      |           |          |          |           |       |         |      |       |       |       |           |
| ListPoolValue |   50 |  9.225 us | 1.783 us | 5.088 us |  7.196 us |  1.08 |    0.63 |    1 |     - |     - |     - |      32 B |
|          Linq |   50 |  9.464 us | 1.431 us | 4.036 us |  8.448 us |  1.00 |    0.00 |    1 |     - |     - |     - |     680 B |
|      ListPool |   50 | 10.746 us | 2.041 us | 5.856 us |  8.969 us |  1.33 |    0.89 |    1 |     - |     - |     - |      88 B |
|               |      |           |          |          |           |       |         |      |       |       |       |           |
| ListPoolValue |  100 |  9.669 us | 1.933 us | 5.576 us |  7.533 us |  1.11 |    0.81 |    1 |     - |     - |     - |      32 B |
|      ListPool |  100 | 10.084 us | 1.853 us | 5.316 us |  8.206 us |  1.15 |    0.81 |    1 |     - |     - |     - |      88 B |
|          Linq |  100 | 11.255 us | 1.978 us | 5.579 us |  9.206 us |  1.00 |    0.00 |    2 |     - |     - |     - |    1216 B |
|               |      |           |          |          |           |       |         |      |       |       |       |           |
|      ListPool | 1000 | 18.059 us | 1.724 us | 4.835 us | 17.582 us |  0.74 |    0.26 |    1 |     - |     - |     - |      88 B |
| ListPoolValue | 1000 | 18.726 us | 1.742 us | 4.914 us | 18.305 us |  0.77 |    0.26 |    1 |     - |     - |     - |      32 B |
|          Linq | 1000 | 25.796 us | 2.426 us | 6.923 us | 24.864 us |  1.00 |    0.00 |    2 |     - |     - |     - |    8456 B |