## __Sobre o ISBNQuery__
Guia prático de uso da dll ISBNQuery para consultas online do ISBN de livros e similares.

* A __Tech Team__ é uma pequena startup que traz quando possível, suas tecnologias para o público em geral, visando sempre compartilhar conhecimento.

* O __ISBNQuery__ é uma forma de facilitar a consulta de um ISBN e obter algumas informações que podem ser importantes para o usuário. Sua implementação busca ser simples e direta.

* Essa DLL é compatível com o Framework .NET 4.8 ou superior.

## Créditos:
A dll ISBNQuery faz consultas online do código ISBN-10 e ISBN-13 a partir da [API](https://openlibrary.org/developers/) disponibilizada pelo site __Open Library__, disponível em: [Open Library](https://openlibrary.org/).



## Consultas:
#### Abaixo estão descritos os métodos disponíveis para realizar as consultas utilizando a dll __ISBNQuery 2.0__:

Para pesquisar qualquer exemplar pelo seu código ISBN10 ou ISBN13:

```
Book book = Query.SearchBook("8551005197");
```

Neste caso, "8551005197" é um código ISBN, e corresponde ao livro: __O Labirinto do Fauno.__
Caso você deseje obter a capa do exemplar, a consulta também é simples:

```
Book book = Query.SearchBook("8551005197");
Image cover = Query.SearchCover(book, ImageSize.L);
```
Nesse contexto, `cover` contém a capa associada ao exemplar `book`, quando disponível. Se o desejado for converter um ISBN10 para ISBN13 ou vice versa, podemos usar um método da classe `ISBNParser`:

```
string result = ISBNParser.ParseISBN("658021001X");
> result == "9786580210015"

string result2 = ISBNParser.ParseISBN("9786580210015");
> result2 == "658021001X"
```

Obterve que os códigos são conversíveis entre si, obtendo sempre os valores correspondentes ao seu ISBN10/ISBN13. Métodos auxiliares para formatação de texto unicode, html, etc, podem ser encontrados na classe `StringHelp`, que pertence a namespace `ISBNQuery.Shared`. 


## Objeto __Book__
### Este objeto guarda as informações retornadas pela API. É por meio dele que você terá acesso às informações de retorno.

* Esta classe possue como __propriedades__:
    * public string Author { get; set; }
    * public string Title { get; set; }
    * public string ISBN10 { get; set; }
    * public string ISBN13 { get; set; }
    * public string Publish_Date { get; set; }
    * public string Source_Records { get; set; }
    * public string Publishers { get; set; }
    * public string Physical_Format { get; set; }
    * public string Latest_Revision { get; set; }
    * public string Description { get; set; }
    * public string TranslatedFrom { get; set; }
    * public string NumberOfPages { get; set; }
    * public string BibKey { get; set; }
    * public string InfoUrl { get; set; }
    * public string ThumbnailUrl { get; set; }
    * public bool HasCover { get; private set; }


## __ReturnType__
### Esse enum fornece o tipo de retorno que os métodos podem gerar. Os possíveis são:


| Nome              | Valor Padrão | Descrição                                                           |
| ----------------- | ------------ | ------------------------------------------------------------------- |
| ValidISBN13       | 0x0          | Indica que o ISBN-13 passado é válido                               |
| InvalidISBN13     | 0x1          | Indica que o ISBN-13 passado é inválido                             |
| ISBN13LenghtError | 0x2          | Indica que o tamanho do fluxo de caracteres do código ISBN-13 difere de 13 dígitos |
| ValidISBN10       | 0x3          | Indica que o ISBN-10 é válido                                       |
| InvalidISBN10     | 0x4          | Indica que o ISBN-10 é inválido                                     |
| ISBN10LenghtError | 0x5          | Indica que o tamanho do fluxo de caracteres do código ISBN-10 difere de 10 dígitos |
| InternalError     | 0x6          | Indica que ocorreu um erro na operação de validação                |
| NullArgumentException | 0x7      | Indica que a entrada do método de verificação foi `null`          |
| InvalidInputFormat | 0x8       | Indica que o formato de entrada do fluxo do código ISBN era inválido |



## __ImageSize__
### Responsável para indicar qual será o tamanho da imagem da capa baixada, podendo ser:

| Nome       | Valor Padrão | Descrição     |
| ---------- | ------------ | ------------- |
| S (Small)  | 83           | Small size    |
| M (Medium) | 77           | Medium size   |
| L (Large)  | 76           | Large size    |

#### Tech™, Inc.
