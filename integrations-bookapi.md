# Integração real — Examples/BookApi.Api

O repositório inclui um exemplo ASP.NET Core que demonstra uso real de `Result<T>` com:

- conversão centralizada para `IActionResult`
- pipeline MediatR + FluentValidation retornando `InvalidObjectError`

---

## 1) Controller base: Result<T> -> IActionResult

O exemplo expõe helpers como:

- `HandleCommand(Result<TOut>)`
- `HandleQuery(Result<TOut>)`
- `HandleQueryable(Result<IQueryable<TSource>>)`
- `HandleFailure(BaseError)`

A ideia é: **o controller sempre recebe `Result<T>`** e transforma em HTTP usando `Match(...)`.

Padrão:

- sucesso => `Ok(...)`
- erro => `HandleFailure(...)`

---

## 2) Validation pipeline (MediatR + FluentValidation)

O Behavior de validação roda validators e:

- se houver falhas:
  - retorna `InvalidObjectError("Erro ao executar validations", new ValidationException(failures))`
- se não houver falhas:
  - chama `next()`

Isso permite que o controller base trate o caso de validação e devolva status/ProblemDetails de forma centralizada.

---

## 3) Recomendações de arquitetura

- Handlers: retornam `Result<T>`
- Domínio: retorna `Result<T>` para regras (evita throw)
- Repositório: pode retornar `Option<T>` quando “não encontrado” não é erro por si só
- Boundary (API): converte `Result<T>` em HTTP via `Match`
