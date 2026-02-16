# FunctionalConcepts

FunctionalConcepts é uma biblioteca .NET (net6.0) que traz composição funcional e uniões discriminadas para C#, com foco em **fluxos explícitos**, **tratamento previsível de falhas**, e **evitar `throw` como controle de fluxo** no domínio.

Tipos principais do pacote:

- `Result<TEntity>` — sucesso (`TEntity`) ou falha (`BaseError`)
- `Option<T>` — `Some(T)` ou `None`
- `Choice<TLeft, TRight>` — `Left`, `Right` ou `Bottom` (estado inválido)

Além disso, a biblioteca inclui erros tipados (ex.: `NotFoundError`, `InvalidObjectError`, etc.) e utilitários para criação de erros via `ErrorHelper`.

---

## Target Framework

- .NET 6.0

---

## Instalação

```bash
dotnet add package FunctionalConcepts
```

---

## Começando rápido

### Result<TEntity>

Criação (conversões implícitas reais do código):

```csharp
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;

// sucesso
Result<int> ok = 10;

// falha via tupla -> BaseError (implicit)
Result<int> fail = (404, "not found");

// falha via BaseError
BaseError err = (500, "error");
Result<int> fail2 = err;
```

Consumindo com `Match`:

```csharp
string msg = ok.Match(
    some => $"value={some}",
    error => $"error={error.Code} {error.Message}"
);
```

---

### Option<T>

```csharp
using FunctionalConcepts.Options;

Option<string> some = "value";
Option<string> none = NoneType.Value;

var text = some.Match(v => v, () => "none");
```

`FailWhen` retorna `Result<T>` e, se for `None`, vira `NotFoundError("object not found")` por padrão:

```csharp
using FunctionalConcepts.Errors;
using FunctionalConcepts.Options;

Option<string> opt = NoneType.Value;

var r = opt.FailWhen(
    v => v.Length < 5,
    (InvalidObjectError)"too short"
);

// r é Fail com NotFoundError("object not found")
```

---

### Choice<TLeft,TRight>

```csharp
using FunctionalConcepts.Choices;

Choice<string, int> left = "FunctionalConcepts";
Choice<string, int> right = 123;

var x = left.Match(l => l.Length, r => r);
```

`Choice` também pode ficar em estado `Bottom`. Em `Bottom`, `Match` retorna `default`.

---

## Documentação

Veja:

- `docs/index.md` (índice)
- `docs/api-reference.md` (referência completa, método por método)

---

## Build e testes

```bash
dotnet build
dotnet test
```

---

## Licença

GPL-3.0
