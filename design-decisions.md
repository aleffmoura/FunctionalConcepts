# Design Decisions (Decisões de Design)

Este documento explica as principais decisões técnicas do FunctionalConcepts, baseadas no comportamento real do código do projeto.

---

## 1) Por que existe o estado “Bottom”?

### O que é Bottom

“Bottom” é o estado que representa um valor inválido / não inicializado corretamente dentro das uniões discriminadas.

Na prática:

- `Result<TEntity>` pode estar em Bottom
- `Choice<TLeft,TRight>` pode estar em Bottom

Esse estado é representado por:

- `ErrorConstant.BOTTOM` (um `BaseError`)
- Mensagem padrão: `ErrorConstant.RESULT_IS_BOTTOM`

### Por que isso existe?

Porque em C# é fácil acabar com estados “vazios” por:

- `default(Result<T>)` / `new Result<T>()`
- atribuir `null` em tipos referencia (quando o tipo não deveria ser “nulo”)
- inicialização incorreta acidental (especialmente em structs)

A escolha do projeto foi:

✅ tratar isso como **falha explícita** e previsível  
❌ não permitir “sucesso sem valor” escondido

Assim o estado inválido não vira um “sucesso silencioso” nem um `NullReferenceException` surpresa.

### Consequência importante

- `Result<TEntity>` em Bottom → `IsFail == true` e erro = `ErrorConstant.BOTTOM`
- `Choice<TLeft,TRight>` em Bottom → `IsBottom == true` e `Match(...)` retorna `default`

> **Por que Choice.Match retorna default em Bottom?**  
> Porque Choice é modelado como “dois caminhos válidos”, e Bottom é tratado como estado inválido/indeterminado. O design escolheu não “forçar” uma exceção nem obrigar um terceiro delegate em `Match`, mas oferecer `Else(...)` / `ElseAsync(...)` para tratar Bottom explicitamente.

---

## 2) Por que Result captura exceptions e Option não?

Isso é uma decisão central da biblioteca.

### Result<TEntity>: captura exceptions e converte para UnhandledError

Operações como:

- `Map`
- `Bind`
- `Then`
- `Else`
- e suas versões async

foram desenhadas para serem **seguras**:

- Se o callback lançar exception, a exception **não vaza**.
- O retorno vira um `Result` de falha com `UnhandledError(ex.Message, ex)`.

✅ Benefícios:
- O fluxo funcional nunca quebra inesperadamente
- Você pode encadear com segurança
- O erro é propagado no mesmo “canal” de falha (`BaseError`)

📌 Motivação prática:
Em aplicações enterprise, é comum uma operação do domínio “explodir” por erro de programação ou dependência.
Em vez disso interromper request/handler, o erro é capturado e retorna um `UnhandledError`.

---

### Option<T>: NÃO captura exceptions

`Option<T>` é focado em representar **presença/ausência**.

Se você está usando `Option<T>` para “buscar algo” ou “ter ou não ter valor”, a ausência já é o caso esperado.

Se a função do callback lança exception, isso normalmente significa:

- bug
- configuração errada
- erro de infraestrutura
- algo que *não é ausência*, e sim falha real

Por isso, o design escolheu:

✅ não capturar exceptions em `Option`  
✅ deixar exception subir (falha rápida)  
✅ e incentivar conversão para `Result<T>` quando você quer “canal de erro”

Ou seja:

- `Option<T>` é *semântica de null*
- `Result<T>` é *semântica de erro*

---

## 3) Por que Option.FailWhen retorna Result<T>?

Esse design é extremamente útil na prática:

- `Option<T>` representa “pode não existir”
- porém, ao validar uma condição, o retorno natural é “sucesso/falha”
- então `FailWhen` converte o contexto de Option para Result

Comportamento real:

- se `Option` é None → retorna `NotFoundError(defaulMessageIfNone)`
- se `Option` é Some e falha na expressão → retorna `baseError`
- caso contrário → sucesso com valor

Isso permite pipelines elegantes:

```csharp
return await repository.Get(id)
    .FailWhen(x => x.IsInactive, (ConflictError)"inactive");
```

---

## 4) Por que FailWhen usa Expression<...> e não Func<...>?

O projeto escolheu `Expression<Func<...>>`, e isso tem implicações:

- permite representar a condição como expressão (potencialmente logável/inspecionável)
- cria uma API “semântica” de regra declarativa

Porém, há custo:

- `expression.Compile()` acontece em runtime
- pode ser caro em hot paths

📌 Possível evolução (sem quebrar compatibilidade):
Adicionar overloads com `Func<T,bool>` para performance.

---

## 5) Por que Result/Choice têm async overloads variados?

Porque em C# existe um “friction point” real:

- às vezes sucesso é sync e erro é async (log remoto, observabilidade)
- às vezes o sucesso é async e o erro é sync
- às vezes ambos async

Então existem overloads que evitam:
- `Task.FromResult(...)`
- `async`/`await` desnecessário

Isso melhora performance e reduz boilerplate.

---

## 6) Por que existe Success?

`Success` é o “unit type” da biblioteca:

- equivalente funcional de `void`
- usado como `Result<Success>`

Isso permite pipelines consistentes:

```csharp
public Result<Success> Execute()
{
    return Result.Success;
}
```

Sem precisar criar `Result<bool>` ou `Result<object>` apenas para representar “deu certo”.

---

## 7) Boundary: onde transformar Result em HTTP?

O design do repo (Exemplo BookApi) mostra a decisão recomendada:

- Dentro do domínio e handlers: use `Result<T>`
- No boundary (Controllers): converta com `Match(...)`

Isso evita:

- espalhar `StatusCode(...)` pelo código
- misturar infra (HTTP) com domínio

O exemplo real centraliza isso em um `ApiControllerBase`.

---

## Resumo (em 5 linhas)

- Bottom existe para tornar estados inválidos explícitos
- Result captura exceptions e converte em UnhandledError para garantir fluxo previsível
- Option não captura exceptions porque representa “presença/ausência”, não “erro”
- FailWhen em Option retorna Result porque validação é erro, não ausência
- Overloads async existem para minimizar boilerplate e overhead
