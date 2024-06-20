[![NuGet](https://img.shields.io/nuget/v/FunctionalConcepts.svg)](https://www.nuget.org/packages/FunctionalConcepts)

[![Build](https://github.com/aleffmoura/FunctionalConcepts/actions/workflows/build.yml/badge.svg)](https://github.com/aleffmoura/FunctionalConcepts/actions/workflows/build.yml) [![publish FunctionalConcepts to nuget](https://github.com/aleffmoura/FunctionalConcepts/actions/workflows/publish.yml/badge.svg)](https://github.com/aleffmoura/FunctionalConcepts/actions/workflows/publish.yml)

[![GitHub contributors](https://img.shields.io/github/contributors/aleffmoura/FunctionalConcepts)](https://github.com/aleffmoura/FunctionalConcepts/graphs/contributors/) [![GitHub Stars](https://img.shields.io/github/stars/aleffmoura/FunctionalConcepts.svg)](https://github.com/aleffmoura/FunctionalConcepts/stargazers) [![GitHub license](https://img.shields.io/github/license/aleffmoura/FunctionalConcepts)](https://github.com/aleffmoura/FunctionalConcepts/blob/main/LICENSE)
[![codecov](https://codecov.io/gh/FunctionalConcepts/branch/main/graph/badge.svg?token=123)](https://codecov.io/gh/aleffmoura/FunctionalConcepts)

---

### Fluente Discriminated Union para padrão Result.

`dotnet add package FunctionalConcepts`

- [Dê uma estrela ⭐!](#dê-uma-estrela-)  
- [Começando 🏃](#começando-)  
- [Contribuição 🤲](#contribuição-)  
- [Créditos 🙏](#créditos-)  
- [Licença 📃](#licença-)

# Dê uma estrela ⭐!

Gostou? Mostre seu apoio dando uma estrela :D

# Começando 🏃

Temos 3 tipos de união discriminada: `Result<T>`, `Choice<TLeft, TRight>` e `Option<T>`. Escolha qual utilizar. A documentação ainda está em desenvolvimento para maior clareza.

## Substitua `Throw` por retorno de `Result<T>`

Isso aqui 👇

```cs
public float Operation(int num1, int num2)
{
    if (num2 == 0)
    {
        throw new Exception("Impossível dividir por zero");
    }

    return num1 / num2;
}

try
{
    var result = Operation(4, 2);
    Console.WriteLine(result * 3);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    return;
}
```
Se torna isso 👇

```cs
public Result<float> Operation(int a, int b)
{
    if (b == 0)
    {
        return (Code: 500, Message: "Impossível dividir por zero");
    }

    return a / b;
}

var result = Operation(4, 2);

var msg = result.Match(
    val => $"{(val * 3)}",
    fail => $"código: {fail.Code} msg: {fail.Message}");

Console.WriteLine(msg);
```

# Métodos
O objeto Result possui alguns métodos que permitem um trabalho funcional, por serem métodos puros, não modificam o estado do resultado atual.

## Exemplo real
Um exemplo real de manipulação, onde a busca na camada de repositório é feita e retorna uma opção. Caso essa opção tenha um valor válido, o livro é excluído; caso contrário, retorna uma mensagem de erro NotFound para a camada superior, que neste caso é uma API.

```cs

public async Task<Result<Success>> Handle(
    BookRemoveCommand cmd,
    CancellationToken cancellationToken)
{

    var maybeBook = await _repositoryBook_.Get(cmd.Id);

    return await maybeBook.MatchAsync(
        async book => Result.Of(await _repositoryBook_.Delete(book, cancellationToken)),
        () => (NotFoundError)$"Livro com id: {cmd.Id} não encontrado");
}
```
# Criando um Result
## Conversão implícita
Existem conversores implícitos de TSuccess ou de BaseError para Result<TSuccess>. Por exemplo:

```cs
Result<Success> resultado = Result.Success; // pode ser feito com resultado = default(Success)
```
Success é uma estrutura vazia para representação de retorno, substituindo o "void". Será abordado no futuro.

Aqui está um exemplo com classe complexa:

```cs
string msg = "mensagem de teste";
ExemploTeste teste = new ExemploTeste(msg);
Result<ExemploTeste> resultado = teste;
```
Pode ser feito como retorno em um método também, caso necessário:

```cs
public Result<int> ResultAsInt()
{
    return 5;
}
```
Aqui está um exemplo de resultado que será lido como erro. É possível fazer uma conversão de tupla para o objeto result e passar seus valores para Código e Mensagem:

```cs

public Result<int> ResultAsErro()
{
    return (404, "objeto não encontrado");
}
```
Caso precise propagar uma exceção com o result, também é possível adicioná-lo à tupla, evitando assim a propagação de exceção e dando mais controle sobre as falhas. Como no exemplo abaixo:

```cs

public Result<float> Operation(int a, int b)
{
    try {
       return a / b;
    }
    catch(Exception ex)
    {
        return (Code: 500, Message: "Impossível dividir por zero", Exception: ex);
        // também é possível retornar apenas: (500, "Impossível dividir por zero", ex)
    }
}
```

## Utilizando o `Factory`

### Result
Em algumas situações, como interfaces por exemplo, a conversão implicita não é possivel, dessa forma é possivel a criação por um metodo especifico, o Of, abaixo o exemplo.

```cs
IQueryable<int> query = new List<int> { 1, 2, 3}.AsQueryable();
//cria um result de sucesso
Result<IQueryable<int>> result = Result.Of(query);

//cria um result de falha
Result<int> result = Result.Of<IQueryable<int>>(404, "object not found");
```

Também é possivel utilizar com tipos comuns.
```cs
Result<int> result = Result.Of(5);//cria um result de sucesso
Result<int> result = Result.Of<int>(404, "object not found");//cria um result de falha
```

E em retorno de metodos.
```cs
public Result<int> GetValue()
{
    return Result.Of(5);
}
```

Quando um cenario de falha, deve ser expecificado o tipo entre <> pois do contrario o tipo ficaria Resul<ConflictError>, redundante, pois Result por si ja assume o papel de Erro e deve ser especificado a o sucesso entre <>
```cs
public Result<int> ErrorAsResult()
{
    return Result.Of<int>((ConflictError)"Mensagem de conflito");
}
```
### Erros
Caso queira, é possivel a inicialização de novas instancias de erros com o BaseError.New()

```cs
BaseError error = BaseError.New(404, "404 em base error");
```
### Options
Em options segue a mesma ideia do result.
```cs
var opt = Option.Of<int>(16); // opt é do tipo Option<int>
```


# Propriedades
Caso o resultado seja uma falha, existe uma propriedade boolean que indica essa condição, caso queira utilizar.

## Result

### `IsFail`
```cs
int userId = 19;
Result<int> result = userId;

if (result.IsFail)
{
    // se result for erro, esse trecho será executado
}
```

### `Error`´

Não é possivel acessar o erro ou o valor diretamente, para isso é preciso utilizar de alguns metodos existentes dentro da biblioteca para acessalos de maneira segura.
Dessa forma, evitamos ifs de comparação de nulavel dentro do codigo e garatimos o fluxo correto de acordo com seus valores, segue exemplos abaixo.

### Metodos.

#### `Then`
Método que permite seguir um fluxo mais fluente com base no resultado em caso de situação de sucesso.

```cs
Result<int> foo = result.Then(v => v + 5);

```
Também é possível aumentar a fluência, ou seja, adicionar mais operadores para seguir o fluxo. Claro, se algum dos cenários retornar um erro ou se o resultado já for um erro, o fluxo não executará a função passada.

```cs
Result<string> foo = result
    .Then(val => val + 5)
    .Then(val => val + 2)
```

#### `Else` metodo para o fluxo em caso de falha

```cs
Result<Company> result = Company.GetFirst();

result.Else(fail => {
     Console.WriteLine(fail.Message)
});
```

#### `Match` e `MatchAsync`
O Match recebe duas funções como parametros, onSome and onError, onSome é executado quando Result for um Sucesso, do contrario é executada a função passada em onError.

#### `Match`

```cs
string foo = result.Match(
    some => some,
    error => $"Msg: {error.Message}");
//Em caso de sucess, Foo assume o valor da mensagem dentro de result, em caso de falha Foo fica com valor "Msg: mensagem"
```

#### Async no `Match`
Mesma coisa que Match normal, contudo, aceita funções que retornam Task para executar.
```cs
string foo = await result.MatchAsync(
    async some => await Task.FromResult(some),
    async error => await Task.FromResult($"Msg: {error.Message}"));
```

# Error Types

## Built in error types
Temos um enum de erros. No entanto, esse enum é apenas utilizado para indicar quais erros cada classe de erro retorna. A classe base BaseError recebe um inteiro para possibilitar erros personalizados por cada programa.

```cs
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
## Tipos de erros e como inicializalos

Primeiro, a classe base permite a criação de erros além dos já existentes.

```cs
private Result<int> FuncReturnError(){
     return (501, "erro com codigo 501")
}

Result<int> result = 1;

Result<string> foo = result
    .Then(val => val + 5)
    .Then(_ => FuncReturnError())
    .Then(v => Console.WriteLine($"value is: {v}"))
```
Nota: Na última situação, onde o valor seria mostrado no console, não será executada porque FuncReturnError retorna um tipo de erro.

Erros são criados implicitamente com base em uma tupla informada entre parênteses. Também é possível propagar uma exceção em casos que sejam necessários.
```cs
private Result<int> FuncReturnError()
{
     try
     {
         //algum erro acontece aqui
     }
     catch(Exception exn)
     {
           return (501, "erro com codigo 501", exn)
     }
}
```
Os seguintes erros já estão presentes, com seus respectivos códigos de erro. É importante lembrar que tudo é feito implicitamente; ou seja, apenas crie a mensagem que estará presente no erro.

```cs
ConflictError conflict = "mensagem de conflict";
ForbiddenError forbidden = "mensagem de forbidden";
InvalidObjectError invalidObj = "mensagem de invalidObj";
NotAllowedError notAllowed = "mensagem de notAllowed";
NotFoundError norFound = "mensagem de norFound";
ServiceUnavailableError unavailable = "mensagem de unavailable";
UnauthorizedError unauth = "mensagem de unauth";
UnhandledError unhandled = "mensagem de unhandled";
```
Também é possível passar Exception para um erro padrão, contudo, será necessário inicializá-lo como uma tupla.

```cs
ConflictError conflict = ("mensagem de conflict", exn);
```

E claro, para acessar a mensagem ou o código de erro, basta acessar as propriedades correspondentes.

```cs
conflict.Code;
conflict.Message;
conflict.Exception;
```

# [Mediator](https://github.com/jbogard/MediatR) + [FluentValidation](https://github.com/FluentValidation/FluentValidation) + `FunctionalConcepts` 🤝
Quando se utiliza MediatR, é bastante comum usar FluentValidation para validar o request. As validações ocorrem com Behavior que lançam exceções se o request estiver inválido.

Usando conceitos funcionais com Result, criamos um Behavior que retorna um erro em vez de lançar uma exceção.

Um exemplo de Behavior: 👇

```cs
public class ValidatorBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, Result<TResponse>>
    where TRequest : notnull
{
    private readonly IValidator<TRequest>[] _validators;

    public ValidatorBehavior(IValidator<TRequest>[] validators) => _validators = validators;

    public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
    {
        List<FluentValidation.Results.ValidationFailure> failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        return failures.Any()
               ? (InvalidObjectError)("Erro ao executar validations", new ValidationException(failures))
               : await next();
    }
}
```

# Contribution 🤲
Se tiver alguma pergunta, comentário ou sugestão, por favor, abra uma issue ou crie um pull request.🙂

# Creditos🙏

- [LanguageExt](https://github.com/louthy/language-ext) - Library with complexy approch arround results and functional programming in C#
- [ErrorOr](https://github.com/amantinband/error-or/issues) - Simple way to functional with errors, amazing library.
- [OneOf](https://github.com/mcintyre321/OneOf/tree/master/OneOf) - Provides F# style discriminated unions behavior for C#

# License 🪪

Licensed under the terms of [GNU General Public License v3.0](https://github.com/aleffmoura/FunctionalConcepts/blob/master/LICENSE.txt) license.
