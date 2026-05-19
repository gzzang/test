## Test.Ddd

基于 .NET 10 的典型领域驱动设计（DDD）项目骨架。

### 结构

- `src/Test.Ddd.Domain`：领域模型、聚合根、值对象、领域事件
- `src/Test.Ddd.Application`：应用服务、DTO、仓储抽象
- `src/Test.Ddd.Infrastructure`：仓储实现
- `src/Test.Ddd.Api`：Web API 接口
- `tests/Test.Ddd.Domain.Tests`：领域单元测试

### 常用命令

```bash
dotnet build Test.Ddd.sln
dotnet test Test.Ddd.sln
dotnet run --project src/Test.Ddd.Api
```
