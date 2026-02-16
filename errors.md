# Errors — Guia técnico

Namespace: `FunctionalConcepts.Errors`

A biblioteca padroniza falhas com `BaseError` e erros tipados derivados.

---

## BaseError

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

---

## EErrorCode (códigos padrão)

Enum em `FunctionalConcepts.Enums`:

- Unauthorized 401
- Forbidden 403
- NotFound 404
- Conflict 409
- NotAllowed 405
- InvalidObject 422
- Unhandled 500
- ServiceUnavailable 503

---

## Erros tipados

Todos seguem o mesmo padrão:
- `implicit operator` de string
- `implicit operator` de (string, Exception)
- `New(...)`

Exemplo:

```csharp
NotFoundError nf = "not found";
ConflictError cf = ("already exists", new Exception("ex"));
```

---

## ErrorHelper

Namespace: `FunctionalConcepts.Errors.Methods`

Factory pública para padronizar criação de erros:

```csharp
var e = ErrorHelper.CustomError(418, "I'm a teapot");
var nf = ErrorHelper.NotFound("book not found");
var inv = ErrorHelper.InvalidObject("validation error");
```

---

## Bottom

`ErrorConstant.BOTTOM` é um `BaseError` `(500, ErrorConstant.RESULT_IS_BOTTOM)`.

Esse erro aparece quando:
- `new Result<TEntity>()` (não inicializado corretamente)
- `Result<TEntity>` recebe null (em cenários que o valor interno fica inválido)
