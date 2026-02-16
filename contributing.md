# Contributing

## Requisitos

- .NET SDK 6+

## Build

```bash
dotnet build
```

## Testes

```bash
dotnet test
```

## Estrutura

- `src/` — biblioteca
- `tests/` — testes (NUnit + FluentAssertions)
- `Examples/` — exemplo BookApi (ASP.NET Core + MediatR + FluentValidation)

## Diretrizes

- Não usar `throw` para fluxo normal no domínio (prefira Result + errors tipados)
- Manter consistência de comportamento entre sync/async
- Cobrir mudanças com testes
- Atenção ao comportamento “Bottom”
