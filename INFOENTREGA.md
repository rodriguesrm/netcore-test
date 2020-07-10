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