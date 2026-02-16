# Choice<TLeft,TRight> — Guia técnico

Namespace: `FunctionalConcepts.Choices`

Choice representa dois caminhos válidos (`Left` ou `Right`) e um estado inválido (`Bottom`).

---

## 1) Estado

- `IsLeft`
- `IsRight`
- `IsBottom`

Construtor padrão:

```csharp
var c = new Choice<string,int>();
// c.IsBottom == true (por design do construtor)
```

Conversões implícitas:

```csharp
Choice<string,int> left = "x";
Choice<string,int> right = 1;
Choice<string,int> bottom = (BaseError)(500, "any");
```

---

## 2) Match

```csharp
TR? Match<TR>(Func<TLeft, TR> onLeft, Func<TRight, TR> onRight)
```

Em Bottom: retorna `default`.

Async overloads:

```csharp
ValueTask<TR?> MatchAsync<TR>(Func<TLeft, Task<TR>> onLeft, Func<TRight, TR> onRight)
ValueTask<TR?> MatchAsync<TR>(Func<TLeft, TR> onLeft, Func<TRight, Task<TR>> onRight)
Task<TR?> MatchAsync<TR>(Func<TLeft, Task<TR>> onLeft, Func<TRight, Task<TR>> onRight)
```

---

## 3) ThenLeft/ThenRight/Else

```csharp
Choice<TLeft,TRight> ThenLeft(Action<TLeft> execute)
ValueTask<Choice<TLeft,TRight>> ThenLeftAsync(Func<TLeft, Task> execute)

Choice<TLeft,TRight> ThenRight(Action<TRight> execute)
ValueTask<Choice<TLeft,TRight>> ThenRightAsync(Func<TRight, Task> execute)

Choice<TLeft,TRight> Else(Action<BaseError> execute)
ValueTask<Choice<TLeft,TRight>> ElseAsync(Func<BaseError, Task> execute)
```

✅ Aqui a biblioteca captura exceptions via `Choice.Run(...)`:
- callback lança => vira `UnhandledError`

---

## 4) MapLeft/MapRight

```csharp
Choice<TR,TRight> MapLeft<TR>(Func<TLeft, TR> execute)
ValueTask<Choice<TR,TRight>> MapLeftAsync<TR>(Func<TLeft, Task<TR>> execute)

Choice<TLeft,TR> MapRight<TR>(Func<TRight, TR> execute)
ValueTask<Choice<TLeft,TR>> MapRightAsync<TR>(Func<TRight, Task<TR>> execute)
```

- Bottom permanece Bottom
- exceptions => `UnhandledError`

---

## 5) BindLeft/BindRight

```csharp
Choice<TL,TRight> BindLeft<TL>(Func<TLeft, Choice<TL,TRight>> execute)
ValueTask<Choice<TL,TRight>> BindLeftAsync<TL>(Func<TLeft, Task<Choice<TL,TRight>>> execute)

Choice<TLeft,TR> BindRight<TR>(Func<TRight, Choice<TLeft,TR>> execute)
ValueTask<Choice<TLeft,TR>> BindRightAsync<TR>(Func<TRight, Task<Choice<TLeft,TR>>> execute)
```

---

## 6) FailWhen

```csharp
Choice<TLeft,TRight> FailWhen(Expression<Func<TLeft, bool>> expression, BaseError baseError)
Choice<TLeft,TRight> FailWhen(Expression<Func<TRight, bool>> expression, BaseError baseError)
```

- Se Bottom => retorna `this`
- Se estiver no lado oposto => retorna `this`
- Se expressão true => retorna `baseError` (Bottom)
