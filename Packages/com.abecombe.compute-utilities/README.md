# Compute Utilities

Small runtime utilities for working with Unity `ComputeShader` and `GraphicsBuffer`.

## Features

- `ComputeProgram` and `ComputeKernel` wrappers for kernel and property ID caching.
- `DispatchThreads` for dispatching by thread count instead of thread group count.
- Buffer wrappers for common compute workflows:
  - `StructuredBuffer<T>`
  - `AppendConsumeBuffer<T>`
  - `CounterBuffer`
  - `IndirectArgumentsBuffer`
  - `DispatchIndirectArgumentsBuffer`
  - `ConstantBuffer<T>`
- HLSL helpers for structured buffer metadata, append/consume buffers, counters, and direct/indirect dispatch bounds checks.

## Quick Start

```csharp
using Abecombe.ComputeUtilities;
using Unity.Mathematics;
using UnityEngine;

public class ComputeExample : MonoBehaviour
{
    [SerializeField] private ComputeProgram program;

    private ComputeKernel _kernel;
    private StructuredBuffer<float4> _points = new();

    private void Start()
    {
        program.Init();
        _kernel = program.FindKernel("Main");

        _points.Init(1024);
        _kernel.SetStructuredBuffer("_Points", _points);
        _kernel.DispatchThreads(_points.Length);
    }

    private void OnDestroy()
    {
        _points.Dispose();
    }
}
```

Assign a `ComputeShader` to `ComputeProgram` in the Inspector, or initialize it from code with `program.Init(computeShader)` or `program.Init("ResourcesPath")`.

Use `program.IsInitialized` when you need to guard setup code after loading a shader dynamically.

## HLSL Helpers

Include `BufferUtils.hlsl` and `DispatchHelper.hlsl` from the package path.

```hlsl
#pragma kernel Main
#pragma multi_compile _ DIRECT_DISPATCH INDIRECT_DISPATCH

#include "Packages/com.abecombe.compute-utilities/Runtime/ComputeShaders/BufferUtils.hlsl"
#include "Packages/com.abecombe.compute-utilities/Runtime/ComputeShaders/DispatchHelper.hlsl"

RW_BUFFER(float4, _Points)

[numthreads(128, 1, 1)]
void Main(uint3 id : SV_DispatchThreadID)
{
    RETURN_IF_INVALID_THREAD(id);

    float4 value = READ_DATA(_Points, id.x);
    WRITE_DATA(_Points, id.x, value + 1.0);
}
```

`DispatchThreads` sets `_DispatchThreadSize` for direct dispatch. It returns without dispatching when any requested dimension is `0`, and logs an error for negative sizes. `DispatchIndirectThreads` switches to the `INDIRECT_DISPATCH` keyword, so the same `RETURN_IF_INVALID_THREAD` macro works for indirect dispatch.

## StructuredBuffer

`StructuredBuffer<T>` sends length, size, index range, and position/index conversion data to HLSL.

```csharp
var cells = new StructuredBuffer<float4>();
cells.Init(new int3(32, 32, 8));

kernel.SetStructuredBuffer("_Cells", cells);
kernel.DispatchThreads(cells.Size);
```

In HLSL:

```hlsl
RW_BUFFER(float4, _Cells)

float4 value = READ_DATA(_Cells, int3(0, 0, 0));
WRITE_DATA(_Cells, int3(0, 0, 0), value);
```

## AppendConsumeBuffer

`AppendConsumeBuffer<T>` owns an append/consume buffer and a count buffer.

```csharp
var hits = new AppendConsumeBuffer<int>();
hits.Init(4096);

kernel.SetAppendBuffer("_Hits", hits, resetBuffer: true);
kernel.DispatchThreads(1024);

uint count = hits.GetCounterValue();
```

In HLSL:

```hlsl
APPEND_BUFFER(int, _Hits)

APPEND_DATA(_Hits, 1)
```

Use `SetConsumeBuffer` with `CONSUME_BUFFER` when reading from the stack.

## Indirect Dispatch

`IndirectArgumentsBuffer` stores a count and owns a `DispatchIndirectArgumentsBuffer` that can build dispatch arguments from that count.

```csharp
var args = new IndirectArgumentsBuffer();
args.Init(new uint[] { 0, 1, 1 }, countBufferOffset: 0, countBufferSize: 1);

args.SetCount(1024);
kernel.DispatchIndirectThreads(args.DispatchIndirectArgumentsBuffer);
```

## Internal Compute Utilities

The package uses `Runtime/Resources/ComputeShaders/ComputeUtilities.compute` internally for buffer copy, clear, and indirect dispatch argument generation.

By default, `Resources/ComputeUtilities/ComputeShaderConfig.asset` points to this compute shader through the `UtilityShader` field.

## Disposal

All buffer wrappers own native `GraphicsBuffer` resources. Call `Dispose()` when the owner is destroyed, for example in `MonoBehaviour.OnDestroy` or a renderer feature cleanup method.