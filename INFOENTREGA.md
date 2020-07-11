# netcore-test

## Informações relevantes sobre a entrega

<br/>

Os serviços foram criados conforme a solicitação apresentada.
Cada um dos serviços estão em um controller separado.

<br/>

#### REQUISITOS:
- Desenvolvido utilizando `.net core 3.1`;
- Base de dados dos serviços está em `Sql Server`;
- Foi utilizado o `ORM Entity Framework Core` para acesso a base de dados.
**IMPORTANTE**: Estrutura da base será criada na execução da aplicação via `migration`;
- Aplicado `DDD - Domain Driven Design` para criação dos serviços;
- Aplicado princípios do `SOLID`;
- Criados testes unitários para as camadas de `Domain` e `Application` apenas, acredito que já é insumo suficiente para a avaliação, em um cenário real um número maior de testes seriam necessários para cobrir um percentual maior da solução.

<br/>

#### OPCIONAIS:
- Um mecanismo de controle de concorrência na criação de consultas foi implementado, tanto para os médicos quando para os paciêntes;
- Criado arquivos Dockerfile e docker-compose para subir a aplicação e suas dependências `Sql Server` e `MongoDb`;
- Um módulo de exames foi sugerido porém não foi implementado;
- Implementado mecanismo de Log através do `Serilog` que além de apresentar no `Console` da aplicação, também persiste o mesmo na base `MongoDb`;
- Utilização  mensageria para comunicação entre módulos também foi sugerida, contudo não acredito que nesse contexto faria sentido. Obviamente serviria para avalidar conhecimentos nessa implementação, ainda sim o esforço e complexidade aumentaria. Estou certo que um bate-papo técnico tal avaliação também pode ser feita, ainda que não tão profunda;
- Da mesma forma que mensageria, não foi implementado orquestradores de containeres, como Kubernetes, por exemplo, entretanto a justificativa aqui é por questão de *skill* mesmo.;

<br/>

#### MASSA DE DADOS:
- Ao subir a aplicação, seja rodando pelo Visual Studio ou ainda através do docker-compose, a base de dados será criada.
- Para testar a aplicação pode-se utilizar o `swagger` que está disponível, e para tanto pode-se usar a seguinte massa de testes:

##### Médicos

<br/>

| Id | Full name |
| ------------- |:-------------:|
| A5ECD70D-5486-40FD-91A4-18F5E88B22AA|NATASHA ROMANOFF |
| AF07A3FA-9CFA-4A33-801E-744269BBA4D8 | TONY STARK |
| 76D2B0F8-EB84-4CC4-87C0-780C7C3C79B7 | STEVE ROGERS |

##### Pacientes

<br/>

| Id | Full name |
| ------------- |:-------------:|
|515D7728-540F-4DF7-B7FA-2FE1D915059D | CLARK KENT |
|53A53E8A-F06A-4668-AC39-3CD344389564 | BRUCE WAYNE |
|D49051C4-ECC9-4893-B571-A104C34B450B | DAIANA PRINCE |