# Option<T> — Guia técnico

Namespace: `FunctionalConcepts.Options`

`Option<T>` representa presença (`Some`) ou ausência (`None`) sem usar `null`.

---

## 1) Estado

- `IsSome`
- `IsNone` (`!IsSome`)

Criação:

```csharp
Option<string> some = "value";
Option<string> none = NoneType.Value;
```

---

## 2) Match (sync e async)

```csharp
TR Match<TR>(Func<T, TR> some, Func<TR> none)
```

Async overloads:

```csharp
Task<TR> MatchAsync<TR>(Func<T, Task<TR>> some, Func<Task<TR>> none)
ValueTask<TR> MatchAsync<TR>(Func<T, Task<TR>> some, Func<TR> none)
ValueTask<TR> MatchAsync<TR>(Func<T, TR> some, Func<Task<TR>> none)
```

---

## 3) Then / Else (efeitos colaterais)

```csharp
Option<T> Then(Action<T> execute)
ValueTask<Option<T>> ThenAsync(Func<T, Task> execute)

Option<T> Else(Action execute)
ValueTask<Option<T>> ElseAsync(Func<Task> execute)
```

✅ Aqui a biblioteca **não captura exception**. Se o callback lança, a exception sobe.

---

## 4) Map / Bind

Map:

```csharp
Option<TR> Map<TR>(Func<T, TR> execute)
ValueTask<Option<TR>> MapAsync<TR>(Func<T, Task<TR>> execute)
```

Bind:

```csharp
Option<TB> Bind<TB>(Func<T, Option<TB>> execute)
ValueTask<Option<TB>> BindAsync<TB>(Func<T, Task<Option<TB>>> execute)
```

⚠️ Map/Bind usam `Result.Run(...)` (sem try/catch). Exceptions sobem.

---

## 5) FailWhen (retorna Result<T>)

Assinatura:

```csharp
Result<T> FailWhen(Expression<Func<T, bool>> expression, BaseError baseError, string defaulMessageIfNone = "object not found")
```

Comportamento real:

- `None` => `NotFoundError(defaulMessageIfNone)`
- `Some` e expressão true => `baseError`
- `Some` e expressão false => sucesso com valor

---

## 6) Equality

Option implementa `IEquatable<Option<T>>` e operadores `==` / `!=`.

- `None == None` é true
- `Some(a) == Some(b)` compara via `Equals` de `T`
