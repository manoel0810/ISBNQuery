## __Sobre o ISBNQuery__
Guia prático de uso da dll ISBNQuery para consultas online do ISBN de livros e similares.

* A __Tech Team__ é uma pequena startup que traz quando possível, suas tecnologias para o público em geral, visando sempre compartilhar conhecimento.

* O __ISBNQuery__ é uma forma de facilitar a consulta de um ISBN e obter algumas informações que podem ser importantes para o usuário. Sua implementação busca ser simples e direta.

* Essa DLL é compatível com o Framework .NET 4.8 ou superior.

## Créditos:
A dll ISBNQuery faz consultas online do código ISBN-10 e ISBN-13 a partir da [API](https://openlibrary.org/developers/) disponibilizada pelo site __Open Libraly__, disponível em: [Open Libraly](https://openlibrary.org/).

## Nota do desenvolvedor
Estamos iniciando esse projeto a pouco tempo! Se você desejar colaborar com a dll, estaremos sempre disponíveis para diálogo pelo nosso email __td.techdevops@gmail.com__. Toda ajuda crítica construtiva será bem-vinda.
* O arquivo xml com as informações do _summary_ está disponível junto do pacote NuGet do ISBNQuery. Está ocorrendo das descrições do _summary_ não aparecerem nas informações dos métodos. __Para resolver isto, copie o arquivo ISBNQuery.xml__ para a pasta _debug_ do seu projeto.

# Descrições dos métodos > Class Consultas

## Método -  ConsultarISBN13(args[ ]);
* Este método efetua uma consulta na API da __Open Library__, buscando informações sobre um _ISBN13_.

## Sobrecargas:
* Possue tres sobrecargas:
    * ConsultarISBN13(string ISBN13)
    * ConsultarISBN13(string ISBN13, bool InternetCheck)
    * ConsultarISBN13(string ISBN13, Uri PingAt)

### 1º  ConsultarISBN13(string ISBN13)
* Efetua uma consulta online de um __ISBN13__ e retorna as informações associadas ao mesmo (Caso seja identificado pela API).
> Retorna um objeto do tipo __Book__ se tudo ocorrer bem.

### 2º ConsultarISBN13(string ISBN13, bool InternetCheck)
* Efetua uma consulta online de um __ISBN13__ e antes de tentar retornar as informações, verifica a conexão com a internet se o parâmetro __InternetCheck__ for _true_. Caso haja conexão, retornará as informações associadas ao ISBN (Caso seja identificado pela API). 
> Retorna um objeto do tipo __Book__ se tudo ocorrer bem.

### 3º ConsultarISBN13(string ISBN13, Uri PingAt)
* Efetua uma consulta online de um __ISBN13__ e antes de tentar retornar as informações, verifica a conexão com a internet, enviando um pacote para o endereço passado em __PingAt__ e aguarda o retorno. Caso haja conexão, retornará as informações associadas ao ISBN (Caso seja identificado pela API).
> Retorna um objeto do tipo __Book__ se tudo ocorrer bem.

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

## Método - ConsultarISBN10(args[ ]);
* Este método efetua uma consulta na API da __Open Library__, buscando informações sobre um _ISBN10_.

## Sobrecargas:
* Possue tres sobrecargas:
    * ConsultarISBN10(string ISBN10)
    * ConsultarISBN10(string ISBN10, bool InternetCheck)
    * ConsultarISBN10(string ISBN10, Uri PingAt)

### 1º ConsultarISBN10(string ISBN10)
* Efetua uma consulta online de um __ISBN10__ e retorna as informações associadas ao mesmo (Caso seja identificado pela API).
> Retorna um objeto do tipo __Book__ se tudo ocorrer bem.

### 2º ConsultarISBN10(string ISBN10, bool InternetCheck)
* Efetua uma consulta online de um __ISBN10__ e antes de tentar retornar as informações, verifica a conexão com a internet se o parâmetro __InternetCheck__ for _true_. Caso haja conexão, retornará as informações associadas ao ISBN (Caso seja identificado pela API). 
> Retorna um objeto do tipo __Book__ se tudo ocorrer bem.

### 3º ConsultarISBN10(string ISBN10, Uri PingAt)
* Efetua uma consulta online de um __ISBN10__ e antes de tentar retornar as informações, verifica a conexão com a internet, enviando um pacote para o endereço passado em __PingAt__ e aguarda o retorno. Caso haja conexão, retornará as informações associadas ao ISBN (Caso seja identificado pela API).
> Retorna um objeto do tipo __Book__ se tudo ocorrer bem.

## Exemplos de código:
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

# Descrições dos métodos > Class Validacoes

## Método - Internet(args[ ]); __obsolet!__
* Este método verifica a conexão com a internet. 
    > Não possue sobrecarga.

    >Sua sintax é: Internet(Uri UserDefine = null)

    > Retorna um valor booleano

## Exemplos de código:

```
bool Con = Validacoes.Internet();
//Se houver internet, o método retorna 'true'.
```
```
Uri.TryParse("www.google.com", UriKind.Absolut, out Uri link);
bool Con = Validacoes.Internet(link);

//Usa o Uri fornecido para teste de conexão. Se houver internet, o método retorna 'true'.
```

## Método - CheckInternet(string Url, int Timeout);
* Este método verifica a conexão com a internet. 
    > Não possue sobrecarga.

    >Sua sintax é: Internet(string Url = "google.com.br", int Timeout = 3000)

    > _true_ se houver conexão


## Método - CheckISBN13(args[ ]);
* Verifica por meio de cálculos se o __ISBN13__ é válido ou não.
> Retorna um __enum__ do tipo __ReturnType__

## Método - CheckISBN10(args[ ]);
* Verifica por meio de cálculos se o __ISBN10__ é válido ou não.
> Retorna um __enum__ do tipo __ReturnType__

## Método - FormatUF8(args[ ]);
* Formata um fluxo de caracteres com acentos em formato _char_ para seu correspondente caractere acentuado.

> Retorna uma string

## Exemplos de código:

```
string nonformated = "Mem\u00f3rias P\u00f3stumas de Br\u00e1s Cubas";
string ret = FormatUF8(nonformated);

// ret terá como retorno > "Memórias Póstumas de Brás Cubas"
```

## Método - FormatUnicodeCaracters(args[ ]);
* Formata um fluxo de caracteres com acentos de combinação em formato _char_ para seu correspondente caractere acentuado.

> Retorna uma string

## Método - FormatISBN(args[ ]);
* Formata um _ISBN10_ para _ISBN13_ ou vice-versa

> Retorna o ISBN-X formatado


## Método - WindowsVersion(args[ ]);
* Busca no registro do _windows_ por sua versão do SO e retorna para quem chamou o método.
> Retorna uma string

## Objeto __Book__
* Este objeto guarda as informações retornadas pela API. É por meio dele que você terá acesso às informações de retorno.

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

### Obs:
> É importante saber que nem todos esses dados podem estar disponíveis. Logo, é possível que só alguns sejam carregados. 

# Enum
## __ReturnType__
* Esse enum fornece o tipo de retorno que os métodos podem gerar. Os possíveis são:
    * ISBN13_VALIDO
        > Lançado sempre que o ISBN13 é __válido__
    * ISBN13_INVALIDO
        > Lançado sempre que o ISBN13 é __inválido__
    * ISBN13_TAM_NON_MATCH
        > Lançado sempre que o ISBN13 __não__ possue comprimento de 13 dígitos
    * ISBN10_VALIDO
        > Lançado sempre que o ISBN10 é __válido__
    * ISBN10_INVALIDO
        > Lançado sempre que o ISBN10 é __inválido__
    * ISBN10_TAM_NON_MATCH
        > Lançado sempre que o ISBN10 __não__ possue comprimento de 10 dígitos
    * OPERATION_ERROR
        > Lançado sempre que o ocorre um erro na operação de cálculo
    * ARGUMENT_NULL
        > Lançado sempre que o parâmetro de entrada é __null__
    * FORMAT_INPUT_INCORRECT
        > Lançado sempre que o formato de entrada do ISBN é __incorreto__


## __ImageSize__
* Responsável para indicar qual será o tamanho da imagem da capa baixada, podendo ser:
   * S (Small size)
   * M (Medium size)
   * L (Large size)

#### Tech™, Inc.
