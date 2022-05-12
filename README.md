# __Guia de instruções de uso__

## __Sobre o ISBNQuery__
Guia prático de uso da dll ISBNQuery para consultas online do ISBN de livros e similares.

* A __Tech Team__ é uma pequena startup que traz quando possível, suas tecnologias para o público em geral, visando sempre compartilhar conhecimento.

* O __ISBNQuery__ é uma forma de facilitar a consulta de um ISBN e obter algumas informações que podem ser importantes para o usuário. Sua implementação busca ser simples e direta.

* Essa DLL é compatível com o <span style="color:lightblue"> Framework .NET 4.8</span> ou superior.

## Créditos:
A dll ISBNQuery faz consultas online do código ISBN-10 e ISBN-13 a partir da [API](https://openlibrary.org/developers/) disponibilizada pelo site __Open Libraly__, disponível em: [Open Libraly](https://openlibrary.org/).

## Nota do desenvolvedor
Estamos iniciando esse projeto a pouco tempo! Se você desejar colaborar com a dll, estaremos sempre disponíveis para diálogo pelo nosso email __td.techdevops@gmail.com__. Toda ajuda crítica construtiva será bem-vinda.
* O arquivo xml com as informações do _summary_ está disponível junto do pacote NuGet do ISBNQuery. Está ocorrendo das descrições do _summary_ não aparecerem nas informações dos métodos. __Para resolver isto, copie o arquivo ISBNQuery.xml__ para a pasta _debug_ do seu projeto.

# Descrições dos métodos > Class Consultas

## Método - <span style="color:#D4D17C"> ConsultarISBN13</span>(args[ ]);
* Este método efetua uma consulta na API da __Open Library__, buscando informações sobre um _ISBN13_.

## Sobrecargas:
* Possue tres sobrecargas:
    * ConsultarISBN13(<span style="color:#1E90FF">string</span> ISBN13)
    * ConsultarISBN13(<span style="color:#1E90FF">string</span> ISBN13, <span style="color:#1E90FF">bool</span> InternetCheck)
    * ConsultarISBN13(<span style="color:#1E90FF">string</span> ISBN13, <span style="color:#8FBC8F">Uri</span> PingAt)

### 1º <span style="color:#D4D17C"> ConsultarISBN13</span>(<span style="color:#1E90FF">string</span> ISBN13)
* Efetua uma consulta online de um __ISBN13__ e retorna as informações associadas ao mesmo (Caso seja identificado pela API).
> Retorna um objeto do tipo <span style="color:#228B22">Book</span> se tudo ocorrer bem.

### 2º <span style="color:#D4D17C"> ConsultarISBN13</span>(<span style="color:#1E90FF">string</span> ISBN13, <span style="color:#1E90FF">bool</span> InternetCheck)
* Efetua uma consulta online de um __ISBN13__ e antes de tentar retornar as informações, verifica a conexão com a internet se o parâmetro __InternetCheck__ for <span style="color:#1E90FF">true</span>. Caso haja conexão, retornará as informações associadas ao ISBN (Caso seja identificado pela API). 
> Retorna um objeto do tipo <span style="color:#228B22">Book</span> se tudo ocorrer bem.

### 3º <span style="color:#D4D17C"> ConsultarISBN13</span>(<span style="color:#1E90FF">string</span> ISBN13, <span style="color:#8FBC8F">Uri</span> PingAt)
* Efetua uma consulta online de um __ISBN13__ e antes de tentar retornar as informações, verifica a conexão com a internet, enviando um pacote para o endereço passado em <span style="color:#1E90FF">PingAt</span> e aguarda o retorno. Caso haja conexão, retornará as informações associadas ao ISBN (Caso seja identificado pela API).
> Retorna um objeto do tipo <span style="color:#228B22">Book</span> se tudo ocorrer bem.

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

## Método - <span style="color:#D4D17C"> ConsultarISBN10</span>(args[ ]);
* Este método efetua uma consulta na API da __Open Library__, buscando informações sobre um _ISBN10_.

## Sobrecargas:
* Possue tres sobrecargas:
    * ConsultarISBN10(<span style="color:#1E90FF">string</span> ISBN10)
    * ConsultarISBN10(<span style="color:#1E90FF">string</span> ISBN10, <span style="color:#1E90FF">bool</span> InternetCheck)
    * ConsultarISBN10(<span style="color:#1E90FF">string</span> ISBN10, <span style="color:#8FBC8F">Uri</span> PingAt)

### 1º <span style="color:#D4D17C"> ConsultarISBN10</span>(<span style="color:#1E90FF">string</span> ISBN10)
* Efetua uma consulta online de um __ISBN10__ e retorna as informações associadas ao mesmo (Caso seja identificado pela API).
> Retorna um objeto do tipo <span style="color:#228B22">Book</span> se tudo ocorrer bem.

### 2º <span style="color:#D4D17C"> ConsultarISBN10</span>(<span style="color:#1E90FF">string</span> ISBN10, <span style="color:#1E90FF">bool</span> InternetCheck)
* Efetua uma consulta online de um __ISBN10__ e antes de tentar retornar as informações, verifica a conexão com a internet se o parâmetro __InternetCheck__ for <span style="color:#1E90FF">true</span>. Caso haja conexão, retornará as informações associadas ao ISBN (Caso seja identificado pela API). 
> Retorna um objeto do tipo <span style="color:#228B22">Book</span> se tudo ocorrer bem.

### 3º <span style="color:#D4D17C"> ConsultarISBN10</span>(<span style="color:#1E90FF">string</span> ISBN10, <span style="color:#8FBC8F">Uri</span> PingAt)
* Efetua uma consulta online de um __ISBN10__ e antes de tentar retornar as informações, verifica a conexão com a internet, enviando um pacote para o endereço passado em <span style="color:#1E90FF">PingAt</span> e aguarda o retorno. Caso haja conexão, retornará as informações associadas ao ISBN (Caso seja identificado pela API).
> Retorna um objeto do tipo <span style="color:#228B22">Book</span> se tudo ocorrer bem.

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

## Método - <span style="color:#D4D17C"> Internet</span>(args[ ]);
* Este método verifica a conexão com a internet. 
    > Não possue sobrecarga.

    >Sua sintax é: <span style="color:#D4D17C"> Internet</span>(<span style="color:#8FBC8F">Uri</span> UserDefine = null)

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

## Método - <span style="color:#D4D17C"> CheckISBN13</span>(args[ ]);
* Verifica por meio de cálculos se o __ISBN13__ é válido ou não.
> Retorna um <span style="color:lightblue">enum</span> do tipo <span style="color:#8FBC8F">ReturnType</span>

## Método - <span style="color:#D4D17C"> CheckISBN10</span>(args[ ]);
* Verifica por meio de cálculos se o __ISBN10__ é válido ou não.
> Retorna um <span style="color:lightblue">enum</span> do tipo <span style="color:#8FBC8F">ReturnType</span>

## Método - <span style="color:#D4D17C"> FormatUF8</span>(args[ ]);
* Formata um fluxo de caracteres com acentos em formato _char_ para seu correspondente caractere acentuado.

> Retorna uma <span style="color:lightblue">string</span>


## Exemplos de código:

```
string nonformated = "Mem\u00f3rias P\u00f3stumas de Br\u00e1s Cubas";
string ret = FormatUF8(nonformated);

// ret terá como retorno > "Memórias Póstumas de Brás Cubas"
```

## Método - <span style="color:#D4D17C"> WindowsVersion</span>(args[ ]);
* Busca no registro do _windows_ por sua versão do SO e retorna para quem chamou o método.
> Retorna uma <span style="color:lightblue">string</span>

## Objeto <span style="color:#228B22">Book</span>
* Este objeto guarda as informações retornadas pela API. É por meio dele que você terá acesso às informações de retorno.

* Esta classe possue como __propriedades__:
    * <span style="color:#1E90FF">public string</span> Author { get; set; }
    * <span style="color:#1E90FF">public string</span> Title { get; set; }
    * <span style="color:#1E90FF">public string</span> ISBN10 { get; set; }
    * <span style="color:#1E90FF">public string</span> ISBN13 { get; set; }
    * <span style="color:#1E90FF">public string</span> Publish_Date { get; set; }
    * <span style="color:#1E90FF">public string</span> Source_Records { get; set; }
    * <span style="color:#1E90FF">public string</span> Publishers { get; set; }
    * <span style="color:#1E90FF">public string</span> Physical_Format { get; set; }
    * <span style="color:#1E90FF">public string</span> Latest_Revision { get; set; }

### Obs:
> <span style="color:#A9303C">É importante saber que nem todos esses dados podem estar disponíveis. Logo, é possível que só alguns sejam carregados.</span> 

# Enum
## <span style="color:#8FBC8F">ReturnType</span>
* Esse <span style="color:lightblue">enum</span> fornece o tipo de retorno que os métodos podem gerar. Os possíveis são:
    * <span style="color:#8FBC8F">ISBN13_VALIDO</span>
        > Lançado sempre que o ISBN13 é __válido__
    * <span style="color:#8FBC8F">ISBN13_INVALIDO</span>
        > Lançado sempre que o ISBN13 é __inválido__
    * <span style="color:#8FBC8F">ISBN13_TAM_NON_MATCH</span>
        > Lançado sempre que o ISBN13 __não__ possue comprimento de 13 dígitos
    * <span style="color:#8FBC8F">ISBN10_VALIDO</span>
        > Lançado sempre que o ISBN10 é __válido__
    * <span style="color:#8FBC8F">ISBN10_INVALIDO</span>
        > Lançado sempre que o ISBN10 é __inválido__
    * <span style="color:#8FBC8F">ISBN10_TAM_NON_MATCH</span>
        > Lançado sempre que o ISBN10 __não__ possue comprimento de 10 dígitos
    * <span style="color:#8FBC8F">OPERATION_ERROR</span>
        > Lançado sempre que o ocorre um erro na operação de cálculo
    * <span style="color:#8FBC8F">ARGUMENT_NULL</span>
        > Lançado sempre que o parâmetro de entrada é __null__
    * <span style="color:#8FBC8F">FORMAT_INPUT_INCORRECT</span>
        > Lançado sempre que o formato de entrada do ISBN é __incorreto__


#### Tech™, Inc.
