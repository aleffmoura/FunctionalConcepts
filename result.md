# Result<TEntity> — Guia técnico

Namespace: `FunctionalConcepts.Results`

`Result<TEntity>` é um `readonly partial struct` que representa sucesso/falha com `BaseError`.

---

## 1) Estado e invariantes (comportamento real)

- `IsSuccess == true` quando existe valor válido.
- `IsFail == true` quando existe erro.
- “Bottom” é um caso especial: `Result` inválido / não inicializado corretamente.
  - `new Result<TEntity>()` cria Bottom via `ErrorConstant.BOTTOM`
  - A mensagem padrão é `ErrorConstant.RESULT_IS_BOTTOM`

O código foi desenhado para evitar `Result` “nulo” ou sem valor.

---

## 2) Criação (conversões implícitas)

```csharp
Result<int> ok = 10;
Result<int> fail = (404, "not found");     // tuple -> BaseError -> Result
BaseError err = (500, "error");
Result<int> fail2 = err;
```

Factory:

```csharp
Result<Success> unit = Result.Success;
Result<string> ok2 = Result.Of("value");
Result<string> fail3 = Result.Of((BaseError)(500, "error"));
```

---

## 3) Match (sync e async)

### Sync

```csharp
TR? Match<TR>(Func<TEntity, TR> onSome, Func<BaseError, TR> onError)
```

### Async (todas)

```csharp
Task<TR> MatchAsync<TR>(Func<TEntity, Task<TR>> onSome, Func<BaseError, Task<TR>> onError)
Task<TR> MatchAsync<TR>(Func<TEntity, Task<TR>> onSome, Func<BaseError, TR> onError)
ValueTask<TR> MatchAsync<TR>(Func<TEntity, TR> onSome, Func<BaseError, Task<TR>> onError)
```

---

## 4) Then / Else (efeito colateral controlado)

```csharp
Result<TEntity> Then(Action<TEntity> execute)
Result<TEntity> Else(Action<BaseError> execute)

ValueTask<Result<TEntity>> ThenAsync(Func<TEntity, Task> execute)
ValueTask<Result<TEntity>> ElseAsync(Func<BaseError, Task> execute)
```

### Comportamento real de exceções
Se o callback lançar exception, **não propaga**: o retorno vira `UnhandledError(exn.Message, exn)`.

Isso é garantido via `Result.Try(...)` e `Result.TryCatch(...)` internos.

---

## 5) Map / MapAsync

```csharp
Result<TR> Map<TR>(Func<TEntity, TR> execute)
ValueTask<Result<TR>> MapAsync<TR>(Func<TEntity, Task<TR>> execute)
```

- Se o `Result` já é falha, apenas propaga o erro.
- Se `execute` lança, retorna `UnhandledError`.

---

## 6) Bind / BindAsync

```csharp
Result<TB> Bind<TB>(Func<TEntity, Result<TB>> execute)
ValueTask<Result<TB>> BindAsync<TB>(Func<TEntity, Task<Result<TB>>> execute)
```

- Se falha: propaga erro.
- Se `execute` lança: retorna `UnhandledError`.

---

## 7) FailWhen

```csharp
Result<TEntity> FailWhen(Expression<Func<TEntity, bool>> expression, BaseError baseError)
```

- Só avalia em sucesso.
- Usa `expression.Compile().Invoke(...)`.

⚠️ Nota de performance: compilar expressão em hot path tem custo. Se isso virar gargalo, uma evolução natural é oferecer overloads com `Func<TEntity,bool>`.

---

## 8) AsOption / AsOptionFail

```csharp
Option<TEntity> AsOption        // Some no sucesso, None na falha
Option<BaseError> AsOptionFail  // Some na falha, None no sucesso
```

Útil quando você quer interoperar com APIs que já trabalham com `Option<T>`.
