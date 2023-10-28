## __Sobre o ISBNQuery__
Guia prático de uso da dll ISBNQuery para consultas online do ISBN de livros e similares.

* A __Tech Team__ é uma pequena startup que traz quando possível, suas tecnologias para o público em geral, visando sempre compartilhar conhecimento.

* O __ISBNQuery__ é uma forma de facilitar a consulta de um ISBN e obter algumas informações que podem ser importantes para o usuário. Sua implementação busca ser simples e direta.

* Essa DLL é compatível com o Framework .NET 4.8 ou superior.

## Créditos:
A dll ISBNQuery faz consultas online do código ISBN-10 e ISBN-13 a partir da [API](https://openlibrary.org/developers/) disponibilizada pelo site __Open Library__, disponível em: [Open Library](https://openlibrary.org/).

## Exemplos de código:

```
Book book = ConsultarISBN13("978-8551005194");
//Retorna um objeto do tipo Book com as informações do livro (O Labirinto do Fauno).
```

```
Book book = ConsultarISBN13("978-8551005194", true);
//Varifica a conexão com a internet, caso obtenha êxito, tenta retorna um objeto do tipo Book com as informações do livro (O Labirinto do Fauno).
```
```
Uri.TryParse("www.google.com", UriKind.Absolut, out Uri link);
//Crio o meu Uri com o endereço que desejo

Book book = ConsultarISBN13("978-8551005194", link);
//Varifica a conexão com a internet, caso obtenha êxito, tenta retorna um objeto do tipo Book com as informações do livro (O Labirinto do Fauno).
```

## Para ISBN10

```
Book book = ConsultarISBN10("8551005197");
//Retorna um objeto do tipo Book com as informações do livro (O Labirinto do Fauno).
```
```
Book book = ConsultarISBN10("8551005197", true);
//Varifica a conexão com a internet, caso obtenha êxito, tenta retorna um objeto do tipo Book com as informações do livro (O Labirinto do Fauno).
```
```
Uri.TryParse("www.google.com", UriKind.Absolut, out Uri link);
//Crio o meu Uri com o endereço que desejo

Book book = ConsultarISBN10("8551005197", link);
//Varifica a conexão com a internet, caso obtenha êxito, tenta retorna um objeto do tipo Book com as informações do livro (O Labirinto do Fauno).
```

## Método - CheckISBN13(string ISBN);
* Verifica por meio de cálculos se o __ISBN13__ é válido ou não.
> Retorna um __enum__ do tipo __ReturnType__

## Método - CheckISBN10(string ISBN);
* Verifica por meio de cálculos se o __ISBN10__ é válido ou não.
> Retorna um __enum__ do tipo __ReturnType__

## Método - FormatUTF8(string sequo);
* Formata um fluxo de caracteres com acentos em formato _char_ para seu correspondente caractere acentuado.

> Retorna uma string

## Exemplos de código:

```
string nonformated = "Mem\u00f3rias P\u00f3stumas de Br\u00e1s Cubas";
string ret = FormatUTF8(nonformated);

// ret terá como retorno > "Memórias Póstumas de Brás Cubas"
```

## Método - FormatUnicodeCaracters(string sequo);
* Formata um fluxo de caracteres com acentos de combinação em formato _char_ para seu correspondente caractere acentuado.

> Retorna uma string

## Método - FormatISBN(string GenericISBN);
* Formata um _ISBN10_ para _ISBN13_ ou vice-versa

> Retorna o ISBN-XX formatado

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
