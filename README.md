# ISBNQuery 📚

[![NuGet Version](https://img.shields.io/nuget/v/ISBNQuery.svg)](https://www.nuget.org/packages/ISBNQuery/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![.NET Multi-Targeting](https://img.shields.io/badge/.NET-8.0%20%7C%209.0%20%7C%2010.0-purple.svg)](https://dotnet.microsoft.com/)

**ISBNQuery** é uma biblioteca C# moderna, assíncrona e multiplataforma (.NET 8.0, .NET 9.0 e .NET 10.0) para consulta de informações sobre **Livros**, **Autores** e **Capas/Fotos** diretamente da [Open Library API](https://openlibrary.org/developers/api).

---

## 🚀 Instalação

Instale o pacote via **NuGet Package Manager** ou pelo terminal com o comando:

```bash
dotnet add package ISBNQuery
```

Ou no **Package Manager Console** do Visual Studio:

```powershell
Install-Package ISBNQuery
```

---

## ✨ Principais Recursos

- 📖 **Busca Completa de Livros**: Obtenha títulos, subtítulos, autores, editoras, datas de publicação, páginas, formato físico, descrição, assuntos/tópicos, `EditionKey` (OLID) e `WorkKey`.
- 👤 **Busca de Autores**: Consulte informações detalhadas de autores por chave da Open Library (ex: `OL26320A`) ou por pesquisa por nome (ex: `"Tolkien"`).
- 🖼️ **Download de Capas e Fotos de Autores**: Obtenha capas de livros por ISBN, OLID, ID ou fotos de autores nos tamanhos Pequeno (`S`), Médio (`M`) e Grande (`L`) como imagens cross-platform `SkiaSharp.SKImage`.
- 🔢 **Validação e Conversão de ISBN**: Valide dígitos verificadores de ISBN-10 e ISBN-13 e faça a conversão bidirecional via `ISBNParser`.
- ⚡ **Alta Performance e Taxa de Requisição Elevada**: Emissão automática do cabeçalho HTTP `User-Agent` respeitando os limites da Open Library de até **3 requisições por segundo**.

---

## 💻 Exemplos de Uso

### 1. Consulta de Livro por ISBN

```csharp
using ISBNQuery;

// Consulta assíncrona por ISBN-13 ou ISBN-10
Book book = await Query.SearchBookAsync("978-8551005194");

Console.WriteLine($"Título: {book.Title}");
Console.WriteLine($"Autor: {book.Author}");
Console.WriteLine($"Editora: {book.Publishers}");
Console.WriteLine($"Páginas: {book.NumberOfPages}");
Console.WriteLine($"Formato: {book.Physical_Format}");
Console.WriteLine($"OLID: {book.EditionKey}");
Console.WriteLine($"Descrição: {book.Description}");
```

### 2. Consulta de Autores (Por Chave ou Nome)

```csharp
using ISBNQuery;
using ISBNQuery.Models;

// Consulta autor específico por chave OLID
AuthorInfo author = await Query.SearchAuthorAsync("OL26320A");
Console.WriteLine($"Nome: {author.Name}");
Console.WriteLine($"Nascimento: {author.BirthDate}");
Console.WriteLine($"Biografia: {author.Bio}");

// Pesquisa de autores por nome
IReadOnlyList<AuthorInfo> authors = await Query.SearchAuthorsByNameAsync("Tolkien");
foreach (var item in authors)
{
    Console.WriteLine($"[{item.Key}] {item.Name} - Obras: {item.WorkCount}");
}
```

### 3. Download de Capa de Livro e Foto de Autor

```csharp
using ISBNQuery;
using ISBNQuery.Models;
using SkiaSharp;

// Download da capa de um livro em tamanho Grande (L)
using SKImage coverImage = await Query.SearchCoverAsync("9788551005194", CoverKeyType.Isbn, ImageSize.L);
Console.WriteLine($"Dimensões da capa: {coverImage.Width}x{coverImage.Height}");

// Salvar a capa em arquivo local
using var data = coverImage.Encode(SKEncodedImageFormat.Png, 100);
using var stream = File.OpenWrite("capa.png");
data.SaveTo(stream);

// Download da foto de um autor
using SKImage authorPhoto = await Query.SearchAuthorPhotoAsync("OL26320A", ImageSize.L);
```

### 4. Validação e Conversão de ISBN

```csharp
using ISBNQuery;
using ISBNQuery.ISBNSearch;

// Validação de dígito verificador
var isbn13Checker = new ISBN13();
ReturnType result = isbn13Checker.ValidateISBN("9786580210015");

if (result.IsSuccess())
{
    Console.WriteLine($"ISBN Válido! ({result})");
}

// Conversão bidirecional entre ISBN-10 e ISBN-13
string isbn13 = ISBNParser.ParseISBN("658021001X");  // Retorna "9786580210015"
string isbn10 = ISBNParser.ParseISBN("9786580210015"); // Retorna "658021001X"
```

---

## 📋 Tabelas de Referência

### Enum `ReturnType` (Resultados de Validação)

| Nome | Valor | Descrição |
| --- | --- | --- |
| `ValidISBN13` | 0 | ISBN-13 válido com dígito de verificação correto |
| `InvalidISBN13` | 1 | ISBN-13 inválido |
| `ISBN13LengthError` | 2 | Código ISBN-13 com tamanho incorreto (diferente de 13 dígitos) |
| `ValidISBN10` | 3 | ISBN-10 válido com dígito de verificação correto |
| `InvalidISBN10` | 4 | ISBN-10 inválido |
| `ISBN10LengthError` | 5 | Código ISBN-10 com tamanho incorreto (diferente de 10 dígitos) |
| `InternalError` | 6 | Erro interno na operação de validação |
| `NullArgument` | 7 | Entrada fornecida nula ou vazia |
| `InvalidFormat` | 8 | Formato de entrada incorreto |

*Nota: Utilize o método de extensão `result.IsSuccess()` para verificar se a validação retornou um ISBN-10 ou ISBN-13 válido.*

### Enum `ImageSize` (Tamanhos de Imagem)

| Nome | Valor ASCII | Tamanho |
| --- | --- | --- |
| `S` | 83 ('S') | Small (Pequeno) |
| `M` | 77 ('M') | Medium (Médio) |
| `L` | 76 ('L') | Large (Grande) |

### Enum `CoverKeyType` (Chaves de Capa)

| Nome | Descrição |
| --- | --- |
| `Isbn` | Busca por código ISBN-10 ou ISBN-13 |
| `Olid` | Busca por chave de edição/obra/autor da Open Library |
| `Id` | Busca por ID interno numérico da capa |
| `Oclc` | Busca por identificador OCLC |
| `Lccn` | Busca por identificador LCCN |

---

## 📄 Licença e Créditos

- **Fonte dos Dados**: [Open Library APIs](https://openlibrary.org/developers/api) mantida pelo [Internet Archive](https://archive.org/).
- **Licença**: Código disponibilizado sob a licença [MIT](LICENSE).

Manoel Lira.
