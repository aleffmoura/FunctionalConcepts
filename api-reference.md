# API Reference — FunctionalConcepts (net6.0)

Este documento lista **todas as assinaturas públicas** encontradas no código do pacote (conforme ZIP enviado).

> Convenção:
> - Se um método existe em versões sync/async, ambas aparecem.
> - Métodos `internal` não aparecem aqui.

---

# Namespace: FunctionalConcepts

## `Success`

```csharp
public struct Success
{
}
```

Uso: “unit type” (equivalente funcional de `void`) em `Result<Success>`.

---

# Namespace: FunctionalConcepts.Constants

## `ErrorConstant`

```csharp
public class ErrorConstant
{
    public static string RESULT_IS_BOTTOM { get; }
    public static BaseError BOTTOM { get; }
}
```

- `RESULT_IS_BOTTOM` é a mensagem padrão.
- `BOTTOM` é um `BaseError` `(500, RESULT_IS_BOTTOM)`.

---

# Namespace: FunctionalConcepts.Enums

## `EErrorCode`

```csharp
public enum EErrorCode
{
    Unauthorized = 401,
    Forbidden = 0403,
    NotFound = 0404,
    Conflict = 0409,
    NotAllowed = 0405,
    InvalidObject = 0422,
    Unhandled = 0500,
    ServiceUnavailable = 0503,
}
```

Observação: os valores com zero à esquerda (ex.: `0403`) continuam sendo inteiros (403).

---

# Namespace: FunctionalConcepts.Errors

## `BaseError`

```csharp
public class BaseError
{
    public int Code { get; private set; }
    public string Message { get; private set; }
    public Exception? Exception { get; private set; }

    public static implicit operator BaseError((int Code, string Message) tuple);
    public static implicit operator BaseError((int Code, string Message, Exception? Exn) tuple);

    public static BaseError New(int code, string msg);
    public static BaseError New(int code, string msg, Exception? ex);
}
```

### Notas de comportamento
- `BaseError` é o “carrier” principal de falhas.
- Tuplas convertem automaticamente em `BaseError`.

---

## Erros tipados (todos herdam `BaseError`)

Cada erro tipado possui o mesmo padrão público:

- `implicit operator` de `string`
- `implicit operator` de `(string, Exception)`
- `New(string)` e `New(string, Exception)`

### `UnauthorizedError`

```csharp
public class UnauthorizedError : BaseError
{
    public static implicit operator UnauthorizedError(string Message);
    public static implicit operator UnauthorizedError((string Message, Exception Exn) tuple);

    public static UnauthorizedError New(string msg);
    public static UnauthorizedError New(string msg, Exception exn);
}
```

### `ForbiddenError`

```csharp
public class ForbiddenError : BaseError
{
    public static implicit operator ForbiddenError(string Message);
    public static implicit operator ForbiddenError((string Message, Exception Exn) tuple);

    public static ForbiddenError New(string msg);
    public static ForbiddenError New(string msg, Exception exn);
}
```

### `NotFoundError`

```csharp
public class NotFoundError : BaseError
{
    public static implicit operator NotFoundError(string Message);
    public static implicit operator NotFoundError((string Message, Exception Exn) tuple);

    public static NotFoundError New(string msg);
    public static NotFoundError New(string msg, Exception exn);
}
```

### `ConflictError`

```csharp
public class ConflictError : BaseError
{
    public static implicit operator ConflictError(string Message);
    public static implicit operator ConflictError((string Message, Exception Exn) tuple);

    public static ConflictError New(string msg);
    public static ConflictError New(string msg, Exception exn);
}
```

### `NotAllowedError`

```csharp
public class NotAllowedError : BaseError
{
    public static implicit operator NotAllowedError(string Message);
    public static implicit operator NotAllowedError((string Message, Exception Exn) tuple);

    public static NotAllowedError New(string msg);
    public static NotAllowedError New(string msg, Exception exn);
}
```

### `InvalidObjectError`

```csharp
public class InvalidObjectError : BaseError
{
    public static implicit operator InvalidObjectError(string Message);
    public static implicit operator InvalidObjectError((string Message, Exception Exn) tuple);

    public static InvalidObjectError New(string msg);
    public static InvalidObjectError New(string msg, Exception exn);
}
```

### `UnhandledError`

```csharp
public class UnhandledError : BaseError
{
    public static implicit operator UnhandledError(string Message);
    public static implicit operator UnhandledError((string Message, Exception Exn) tuple);

    public static UnhandledError New(string msg);
    public static UnhandledError New(string msg, Exception exn);
}
```

### `ServiceUnavailableError`

```csharp
public class ServiceUnavailableError : BaseError
{
    public static implicit operator ServiceUnavailableError(string Message);
    public static implicit operator ServiceUnavailableError((string Message, Exception Exn) tuple);

    public static ServiceUnavailableError New(string msg);
    public static ServiceUnavailableError New(string msg, Exception exn);
}
```

---

## `FunctionalConcepts.Errors.Methods.ErrorHelper`

Factory pública (atalhos) para instanciar erros:

```csharp
public static class ErrorHelper
{
    public static BaseError CustomError(int statusCode, string msg);
    public static BaseError CustomError(int statusCode, string msg, Exception exn);

    public static ConflictError Conflict(string msg);
    public static ConflictError Conflict(string msg, Exception exn);

    public static ForbiddenError Forbidden(string msg);
    public static ForbiddenError Forbidden(string msg, Exception exn);

    public static InvalidObjectError InvalidObject(string msg);
    public static InvalidObjectError InvalidObject(string msg, Exception exn);

    public static NotAllowedError NotAllowed(string msg);
    public static NotAllowedError NotAllowed(string msg, Exception exn);

    public static NotFoundError NotFound(string msg);
    public static NotFoundError NotFound(string msg, Exception exn);

    public static ServiceUnavailableError ServiceUnavailable(string msg);
    public static ServiceUnavailableError ServiceUnavailable(string msg, Exception exn);

    public static UnauthorizedError Unauthorized(string msg);
    public static UnauthorizedError Unauthorized(string msg, Exception exn);

    public static UnhandledError Unhandled(string msg);
    public static UnhandledError Unhandled(string msg, Exception exn);
}
```

---

# Namespace: FunctionalConcepts.Results

## `Result<TEntity>` (readonly partial struct)

### Estado e conversões

```csharp
public readonly partial struct Result<TEntity>
{
    public Result();

    public readonly bool IsSuccess { get; }
    public readonly bool IsFail { get; }

    public readonly Option<BaseError> AsOptionFail { get; }
    public readonly Option<TEntity> AsOption { get; }

    public static implicit operator Result<TEntity>(TEntity success);
    public static implicit operator Result<TEntity>(BaseError error);
}
```

#### Nota importante — Bottom
- `new Result<TEntity>()` cria um `Result` em estado **Bottom**, usando `ErrorConstant.BOTTOM`.
- Em vários cenários, passar `null` para `Result<TEntity>` também resulta em Bottom (o construtor interno trata `null` como inválido, conforme testes).

---

### Match (sync)

```csharp
public TR? Match<TR>(Func<TEntity, TR> onSome, Func<BaseError, TR> onError);
```

### MatchAsync (todas as sobrecargas públicas)

```csharp
public Task<TR> MatchAsync<TR>(Func<TEntity, Task<TR>> onSome, Func<BaseError, Task<TR>> onError);
public Task<TR> MatchAsync<TR>(Func<TEntity, Task<TR>> onSome, Func<BaseError, TR> onError);
public ValueTask<TR> MatchAsync<TR>(Func<TEntity, TR> onSome, Func<BaseError, Task<TR>> onError);
```

---

### Then / Else (efeitos colaterais) + Async (todas as sobrecargas públicas)

```csharp
public readonly Result<TEntity> Then(Action<TEntity> execute);
public readonly Result<TEntity> Else(Action<BaseError> execute);

public readonly ValueTask<Result<TEntity>> ThenAsync(Func<TEntity, Task> execute);
public readonly ValueTask<Result<TEntity>> ElseAsync(Func<BaseError, Task> execute);
```

**Comportamento real:** se o callback lançar exception, o retorno vira `UnhandledError`.

---

### Map / MapAsync

```csharp
public readonly Result<TR> Map<TR>(Func<TEntity, TR> execute);
public readonly ValueTask<Result<TR>> MapAsync<TR>(Func<TEntity, Task<TR>> execute);
```

**Comportamento real:** exceptions dentro de `execute` são convertidas para `UnhandledError` (não propagam).

---

### Bind / BindAsync

```csharp
public readonly Result<TB> Bind<TB>(Func<TEntity, Result<TB>> execute);
public readonly ValueTask<Result<TB>> BindAsync<TB>(Func<TEntity, Task<Result<TB>>> execute);
```

**Comportamento real:** exceptions dentro de `execute` são convertidas para `UnhandledError` (via Try/TryCatch internos).

---

### FailWhen

```csharp
public readonly Result<TEntity> FailWhen(Expression<Func<TEntity, bool>> expression, BaseError baseError);
```

- Avalia apenas em sucesso.
- Usa `expression.Compile().Invoke(...)`.

---

## Static helper `Result` (factory)

```csharp
public static class Result
{
    public static Success Success { get; }

    public static Result<TSuccess> Of<TSuccess>(TSuccess value);
    public static Result<TSuccess> Of<TSuccess>(BaseError error);
}
```

---

# Namespace: FunctionalConcepts.Options

## `NoneType`

```csharp
public struct NoneType
{
    public static readonly NoneType Value;
}
```

---

## `Option<T>` (readonly partial struct)

### Estado e conversões

```csharp
public readonly partial struct Option<T>
{
    public Option();

    public readonly bool IsSome { get; }
    public readonly bool IsNone { get; }

    public static implicit operator Option<T>(NoneType _);
    public static implicit operator Option<T>(T value);
}
```

---

### Match + Async (todas as sobrecargas)

```csharp
public TR Match<TR>(Func<T, TR> some, Func<TR> none);

public Task<TR> MatchAsync<TR>(Func<T, Task<TR>> some, Func<Task<TR>> none);
public ValueTask<TR> MatchAsync<TR>(Func<T, Task<TR>> some, Func<TR> none);
public ValueTask<TR> MatchAsync<TR>(Func<T, TR> some, Func<Task<TR>> none);
```

---

### Then / Else + Async

```csharp
public Option<T> Then(Action<T> execute);
public ValueTask<Option<T>> ThenAsync(Func<T, Task> execute);

public Option<T> Else(Action execute);
public ValueTask<Option<T>> ElseAsync(Func<Task> execute);
```

**Comportamento real:** Option não converte exceptions aqui; se o callback lançar, a exception sobe.

---

### Map / MapAsync

```csharp
public readonly Option<TR> Map<TR>(Func<T, TR> execute);
public readonly ValueTask<Option<TR>> MapAsync<TR>(Func<T, Task<TR>> execute);
```

**Comportamento real:** usa `Result.Run(...)` (sem try/catch). Exceptions sobem.

---

### Bind / BindAsync

```csharp
public readonly Option<TB> Bind<TB>(Func<T, Option<TB>> execute);
public readonly ValueTask<Option<TB>> BindAsync<TB>(Func<T, Task<Option<TB>>> execute);
```

**Comportamento real:** também usa `Result.Run(...)`. Exceptions sobem.

---

### FailWhen (retorna Result<T>)

```csharp
public Result<T> FailWhen(
    Expression<Func<T, bool>> expression,
    BaseError baseError,
    string defaulMessageIfNone = "object not found");
```

Comportamento real:
- Se `None` => Fail com `NotFoundError(defaulMessageIfNone)`
- Se `Some` e expressão true => Fail com `baseError`
- Se `Some` e expressão false => Success com valor

---

### Equality

```csharp
public readonly partial struct Option<T> : IEquatable<Option<T>>
{
    public static bool operator ==(Option<T> left, Option<T> right);
    public static bool operator !=(Option<T> left, Option<T> right);

    public readonly bool Equals(Option<T> other);
    public override readonly bool Equals(object? obj);
    public readonly override int GetHashCode();
}
```

---

## Static helper `Option` (factory + extensions)

```csharp
public static class Option
{
    public static Option<T> Of<T>(T value);

    public static ValueTask<Option<TR>> MapAsync<TEntity, TR>(
        this ValueTask<Option<TEntity>> resultTask,
        Func<TEntity, Task<TR>> execute);

    public static ValueTask<Option<TB>> BindAsync<TEntity, TB>(
        this ValueTask<Option<TEntity>> resultTask,
        Func<TEntity, Task<Option<TB>>> execute);

    public static ValueTask<Option<TEntity>> ThenAsync<TEntity>(
        this ValueTask<Option<TEntity>> resultTask,
        Func<TEntity, Task<Option<TEntity>>> execute);
}
```

Nota técnica:
- `ThenAsync` acima aceita `Func<TEntity, Task<Option<TEntity>>>`, mas é usado como `Func<TEntity, Task>` (Task<T> herda de Task). O retorno `Option<TEntity>` da Task é ignorado pelo `Option.ThenAsync(Func<T,Task>)`.

---

# Namespace: FunctionalConcepts.Choices

## `Choice<TLeft,TRight>` (readonly partial struct)

### Estado e conversões

```csharp
public readonly partial struct Choice<TLeft, TRight>
{
    public Choice();

    public readonly bool IsLeft { get; }
    public readonly bool IsRight { get; }
    public readonly bool IsBottom { get; }

    public static implicit operator Choice<TLeft, TRight>(TLeft left);
    public static implicit operator Choice<TLeft, TRight>(TRight right);
    public static implicit operator Choice<TLeft, TRight>(BaseError value);
}
```

Nota: o `Choice()` padrão inicializa em Bottom com erro `(500, "Choice was initialize with bottom")`.

---

### Match + Async (todas as sobrecargas)

```csharp
public readonly TR? Match<TR>(Func<TLeft, TR> onLeft, Func<TRight, TR> onRight);

public readonly ValueTask<TR?> MatchAsync<TR>(Func<TLeft, Task<TR>> onLeft, Func<TRight, TR> onRight);
public readonly ValueTask<TR?> MatchAsync<TR>(Func<TLeft, TR> onLeft, Func<TRight, Task<TR>> onRight);
public readonly Task<TR?> MatchAsync<TR>(Func<TLeft, Task<TR>> onLeft, Func<TRight, Task<TR>> onRight);
```

Em Bottom: retorna `default`.

---

### ThenLeft / ThenRight / Else + Async

```csharp
public readonly Choice<TLeft, TRight> ThenLeft(Action<TLeft> execute);
public readonly ValueTask<Choice<TLeft, TRight>> ThenLeftAsync(Func<TLeft, Task> execute);

public readonly Choice<TLeft, TRight> ThenRight(Action<TRight> execute);
public readonly ValueTask<Choice<TLeft, TRight>> ThenRightAsync(Func<TRight, Task> execute);

public readonly Choice<TLeft, TRight> Else(Action<BaseError> execute);
public readonly ValueTask<Choice<TLeft, TRight>> ElseAsync(Func<BaseError, Task> execute);
```

Comportamento real:
- callbacks são executados via `Choice.Run(...)` (com try/catch)
- exceptions viram `UnhandledError`

---

### MapLeft / MapRight + Async

```csharp
public readonly Choice<TR, TRight> MapLeft<TR>(Func<TLeft, TR> execute);
public readonly ValueTask<Choice<TR, TRight>> MapLeftAsync<TR>(Func<TLeft, Task<TR>> execute);

public readonly Choice<TLeft, TR> MapRight<TR>(Func<TRight, TR> execute);
public readonly ValueTask<Choice<TLeft, TR>> MapRightAsync<TR>(Func<TRight, Task<TR>> execute);
```

Comportamento real:
- Bottom permanece Bottom
- exceptions viram `UnhandledError`

---

### BindLeft / BindRight + Async

```csharp
public readonly Choice<TL, TRight> BindLeft<TL>(Func<TLeft, Choice<TL, TRight>> execute);
public readonly ValueTask<Choice<TL, TRight>> BindLeftAsync<TL>(Func<TLeft, Task<Choice<TL, TRight>>> execute);

public readonly Choice<TLeft, TR> BindRight<TR>(Func<TRight, Choice<TLeft, TR>> execute);
public readonly ValueTask<Choice<TLeft, TR>> BindRightAsync<TR>(Func<TRight, Task<Choice<TLeft, TR>>> execute);
```

Comportamento real:
- Bottom permanece Bottom
- exceptions viram `UnhandledError`

---

### FailWhen (Left e Right)

```csharp
public readonly Choice<TLeft, TRight> FailWhen(Expression<Func<TLeft, bool>> expression, BaseError baseError);
public readonly Choice<TLeft, TRight> FailWhen(Expression<Func<TRight, bool>> expression, BaseError baseError);
```

- Se Bottom: não avalia e retorna `this`
- Se estiver no lado oposto: não avalia e retorna `this`
- Se expressão for true: retorna `baseError` (Bottom)

---

## Static helper `Choice` (factory)

```csharp
public static class Choice
{
    public static Choice<TLeft, TRight> Of<TLeft, TRight>(TLeft value);
    public static Choice<TLeft, TRight> Of<TLeft, TRight>(TRight value);
    public static Choice<TLeft, TRight> Of<TLeft, TRight>(BaseError value);
}
```
