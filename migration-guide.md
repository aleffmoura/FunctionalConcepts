# Migration Guide — de Exceptions + null para Result/Option/Choice

Este guia mostra como migrar um código C# “tradicional” para um estilo funcional usando:

- `Result<TEntity>`
- `Option<T>`
- `Choice<TLeft,TRight>`

Inclui padrões recomendados e anti-patterns.

---

# 1) Migração de null para Option<T>

## Antes (null)

```csharp
public User? GetUser(Guid id)
{
    return _db.Users.SingleOrDefault(x => x.Id == id);
}
```

Chamador:

```csharp
var user = GetUser(id);
if (user == null) return NotFound();
```

## Depois (Option)

```csharp
using FunctionalConcepts.Options;

public Option<User> GetUser(Guid id)
{
    var user = _db.Users.SingleOrDefault(x => x.Id == id);
    return user is null ? NoneType.Value : user;
}
```

Chamador:

```csharp
return GetUser(id).Match(
    some => Ok(some),
    () => NotFound()
);
```

✅ Benefícios:
- Sem null
- Sem `NullReferenceException` surpresa
- Obriga tratamento explícito

---

# 2) Migração de exceptions de validação para Result<T>

## Antes (throw para validação)

```csharp
public Order CreateOrder(CreateOrderCommand cmd)
{
    if (cmd.Total <= 0)
        throw new InvalidOperationException("Invalid total");

    return new Order(...);
}
```

Isso força `try/catch` no chamador, e vira fluxo implícito.

## Depois (Result)

```csharp
using FunctionalConcepts.Results;

public Result<Order> CreateOrder(CreateOrderCommand cmd)
{
    if (cmd.Total <= 0)
        return (422, "Invalid total");

    return new Order(...);
}
```

Chamador:

```csharp
var result = CreateOrder(cmd);

return result.Match(
    order => Ok(order),
    err => StatusCode(err.Code, err.Message)
);
```

✅ Benefícios:
- Sem try/catch para fluxo normal
- API explícita
- Testes muito mais simples

---

# 3) Pipeline: Option -> Result

Cenário comum: repositório retorna Option, mas no serviço “não encontrar” é erro.

```csharp
using FunctionalConcepts.Errors;
using FunctionalConcepts.Options;
using FunctionalConcepts.Results;

public Result<User> GetOrFail(Guid id)
{
    Option<User> opt = _repo.Get(id);

    return opt.Match(
        user => Result.Of(user),
        () => (NotFoundError)"User not found"
    );
}
```

✅ Padrão:
- infra/storage: Option
- domínio/serviço: Result

---

# 4) Encadeando operações (substituindo if/try-catch)

## Antes

```csharp
try
{
    var user = _repo.Get(id);
    if (user == null) return NotFound();

    if (!user.Active) return Conflict();

    var invoice = _billing.Generate(user);
    _repo.Save(invoice);

    return Ok(invoice);
}
catch (Exception ex)
{
    return StatusCode(500, ex.Message);
}
```

## Depois (Result + composição)

```csharp
Result<User> userResult = GetOrFail(id)
    .FailWhen(u => !u.Active, (ConflictError)"Inactive user");

Result<Invoice> invoiceResult =
    userResult.Map(u => _billing.Generate(u))
              .Then(inv => _repo.Save(inv));

return invoiceResult.Match(
    inv => Ok(inv),
    err => StatusCode(err.Code, err.Message)
);
```

Observação:
- `Then` é ótimo para efeitos colaterais (persistência/log)
- `Map` para transformar valor
- `Bind` quando a função já retorna `Result<...>`

---

# 5) Quando usar Bind em vez de Map

## Use Map quando sua função retorna valor puro

```csharp
Result<int> r = 10;
Result<string> mapped = r.Map(x => (x * 2).ToString());
```

## Use Bind quando sua função já retorna Result

```csharp
Result<int> Parse(string text)
{
    if (!int.TryParse(text, out var value))
        return (422, "invalid number");
    return value;
}

Result<int> r = "10";
Result<int> parsed = r.Bind(Parse);
```

---

# 6) Erros tipados: substituindo strings soltas

## Antes

```csharp
return (404, "not found");
```

## Depois (mais semântico)

```csharp
using FunctionalConcepts.Errors;

return (NotFoundError)"User not found";
```

✅ Benefícios:
- Mensagens mais consistentes
- Código padronizado
- Melhor leitura do domínio

---

# 7) Choice: quando existe bifurcação válida (não erro)

Se você tem dois estados válidos e não quer “bool flag”:

## Antes (bool flag)

```csharp
public (bool isCached, User user) GetUser(Guid id) { ... }
```

Isso é ambíguo e frágil.

## Depois (Choice)

```csharp
using FunctionalConcepts.Choices;

public Choice<User, User> GetUser(Guid id)
{
    // Left = cache hit
    // Right = db hit
}
```

Consumindo:

```csharp
var choice = GetUser(id);

choice.Match(
    cached => Log("cache"),
    db => Log("db")
);
```

---

# 8) Anti-patterns comuns

## ❌ Usar Result para ausência de valor (quando não é erro)

Errado:

```csharp
public Result<User> Get(Guid id)
{
    var u = _db.Find(id);
    if (u == null) return (404, "not found");
    return u;
}
```

Melhor:

```csharp
public Option<User> Get(Guid id) => ...
```

E só converta para Result no serviço se “não existir” for erro.

---

## ❌ Usar Option para representar falha

Errado:

```csharp
public Option<User> Create(User u)
{
    if (!IsValid(u)) return NoneType.Value; // mas por quê falhou?
    ...
}
```

Aqui existe falha real e você precisa explicar erro.

Melhor:

```csharp
public Result<User> Create(User u)
{
    if (!IsValid(u)) return (422, "invalid user");
    ...
}
```

---

## ❌ Misturar HTTP dentro do domínio

Errado:

```csharp
public IActionResult Create(...) { ... }
```

Melhor:

- domínio retorna `Result<T>`
- controller converte com `Match(...)`

---

# 9) Estratégia de migração incremental

1. Comece no repositório: troque retornos `T?` por `Option<T>`
2. No domínio/serviço: troque `throw` de validação por `Result<T>`
3. No boundary: centralize `Result<T> -> IActionResult` com `Match`
4. Refatore fluxos para `Map/Bind/Then/Else`
5. Troque strings soltas por erros tipados (`NotFoundError`, etc.)

---

# 10) Checklist final

- [ ] Repositórios retornam `Option<T>`
- [ ] Domínio retorna `Result<T>` (não lança exception em regra normal)
- [ ] Controllers/Handlers convertem com `Match`
- [ ] Sem `null` no domínio
- [ ] Sem `try/catch` para fluxo normal
- [ ] Erros tipados em vez de strings soltas
