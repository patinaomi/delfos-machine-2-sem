## MASTERING RELATIONAL AND NON RELATIONAL DATABASE
1. Correções
• Realizar a correção dos apontamentos realizados na sprint anterior.
Valor: 5 pontos.
2. Empacotamento de Objetos
• Criar o empacotamento (Package) de todos os objetos criados no projeto.
Valor: 20 pontos.
3. Criação de Procedures para Relatórios(Consultas)
• Desenvolver 2 procedures para geração de relatórios e exibição dos dados no front-end da
aplicação.
Valor: 25 pontos.
As procedures deverão atender aos seguintes requisitos mínimos:
• Utilizar 2 inner joins.
• Utilizar pelo menos 1 left join ou 1 right join.
• Incluir 1 cursor.
• Implementar 1 função de agregação.
• Aplicar 1 função de tratamento de dados.
• Ordenar os dados para melhorar a visualização.
4. Criação de Trigger para Auditorias
•Desenvolver uma trigger de auditoria que registre em uma tabela auxiliar todas as alterações
(INSERT, UPDATE e DELETE) realizadas nas 3 principais tabelas do projeto.
Valor: 30 pontos.
5. Testes e Demonstração em Vídeo
Valor: 20 pontos.
•Gravar um vídeo demonstrando e testando todos os itens entregues, atendendo aos seguintes
critérios:
• Testar as procedures dentro das packages via aplicação com as seguintes operações:
• 3 chamadas para INSERT.
• 2 chamadas para UPDATE.
• 2 chamadas para DELETE.
• 2 Exceptions
• Demonstrar os registros de auditoria na tabela auxiliar.
• Extrair um relatório na aplicação com os dados processados.
• Observação: Não utilizar Postman ou Swagger para as demonstrações. Todo o processo
deve ser realizado diretamente na aplicação.

6. Entregáveis
•Enviar um arquivo .zip contendo os seguintes itens:
• Um documento PDF com a documentação do projeto, incluindo:
• Descrição detalhada dos componentes desenvolvidos.
• Localização do vídeo de demonstração.
• Nome de todos integrantes do projeto
• Todos os códigos desenvolvidos (procedures, triggers, packages, etc.)
organizados.
Obs: A boa organização do conteúdo do arquivo é de suma importância para
apontamento da nota, caso haja falha nesta organização a correção pode vir a
ser comprometida acarretando diminuição da nota

## COMPLIANCE & QUALITY ASSURANCE
Utilizando a ferramenta de planejamento de projeto ágil em nuvem, a qual foi apresentada em
suas aulas, aplicando SCRUM, faça:
▪ Criação do backlog de produto, organizado em Épicos (Epic), Funcionalidades (Feature) e Itens de
entrega de produto (Product backlog itens) - peso 25%
▪ Descrição de cada item do planejamento (Epic, Feature, Product backlog itens) - peso 25%
▪ Critérios de aceite de cada item do planejamento (Epic, Feature, Product backlog itens) - peso 25%
▪ Organização do backlog na ordem de prioridade de execução/realização do desenvolvimento por
SPRINT– peso 25%
Entrega:
▪ Coloque o link de acesso ao seu plano de projeto, o qual precisa estar configurado para acesso
público, permitindo o acesso integral do seu professor para correção.

## DEVOPS TOOLS E CLOUD COMPUTING
Utilizando uma das tecnologias aprendidas: Serviços de Aplicativos ou ACR/ACI (deve-se utilizar ambos ACR e
ACI nessa opção):
• Implantar a solução ou parte dela (um módulo do sistema) da Odontoprev em Nuvem;
• Serão aceitas soluções com Front ou API;
• Obrigatório um Banco de Dados em Nuvem que não seja o H2. Serão aceitos: Oracle, Mysql, SQL da Azure
ou PostgreSQL;
• Realize um CRUD (Inclusão, Alteração, Exclusão e Consulta) sobre pelo menos duas tabelas com um
relacionamento entre elas;
• Inclua e movimente no mínimo 5 linhas com conteúdo significativo nessas tabelas (testes).

Entregas obrigatórias:
1) Uma breve descrição de sua solução; (0 pontos– Na falta a nota será subtraída em 05 pontos);
2) O desenho da arquitetura da solução proposta (desenho baseado em nossa disciplina, mostrando os recursos,
fluxos da informação etc); (até 20 pontos);
3) Uma breve descrição dos benefícios a serem alcançados em relação ao negócio (com a implantação da
solução proposta); (0 pontos– Na falta a nota será subtraída em 05 pontos);
4) DDL das tabelas (tabelas, colunas, chave primária, comentários etc) - Criar um arquivo de texto somente
com esse DDL; (até 05 pontos);
5) Código-Fonte da aplicação no Github (e com tudo que é necessário para a execução do App/API) com um
Readme.md para realizar o Deploy e testes (O Prof. irá reproduzir e testar o deploy com base nesse
Readme.md). Incluir os scripts JSON do CRUD, se a solução for uma API; (até 05 pontos);

Entregas obrigatórias:
7) Uma gravação de um vídeo mostrando o funcionamento da solução, desde o Clone do Repositório (ou do
build da Imagem), mostrando o funcionamento até persistência de dados em Nuvem MS Azure. Inclusive, o
vídeo deve mostrar os dados persistidos no banco de dados (CRUD); (até 70 pontos);
Obs: O vídeo precisa ter qualidade de no mínimo 720p, e conter áudio ou legenda explicando o
desenvolvimento da solução em nuvem.
8) Arquivo PDF, contendo o nome e RM dos integrantes do time, link do Github e Link do Vídeo. (0 pontos);
Obs: O professor precisa ter acesso ao vídeo.
A PONTUAÇÃO DE CADA QUESITO VAI VARIAR DE ACORDO COM A QUALIDADE DA TAREFA ENTREGUE!

## ADVANCED BUSINESS DEVELOPMENT WITH .NET
Objetivo Geral
Desenvolver uma API utilizando ASP.NET Core Web API, aplicando princípios
de arquitetura de software, design patterns, técnicas de documentação, testes
e integração com banco de dados.
Requisitos
• O grupo deve criar os seguintes artefatos:
o (até 15 pontos)- Definir a arquitetura da API, explicando a escolha
entre uma abordagem monolítica ou microservices e justificando a
decisão.
o (até 15 pontos)- Implementar a API seguindo a arquitetura escolhida
e explicar as diferenças.
o (até 30 pontos)- Endpoints CRUD (ORACLE) para os recursos de
escolha (ex: produtos, usuários).
Requisitos (continuação)
• O grupo deve criar os seguintes artefatos:
o (até 25 pontos)- Implementar pelo menos um padrão de criação na
API (ex: Singleton para o gerenciador de configurações).
o (até 15 pontos)- Configurar a documentação da API utilizando
Swagger/OpenAPI, com descrições claras dos endpoints e modelos
de dados.
Entrega
• O grupo deve criar um repositório público no GitHub com o código da API e
incluir no repositório um README.md com integrantes do grupo, explicação
da arquitetura, design patterns utilizados, instruções para rodar a API, além
de exemplos de testes.
o Se os endpoints CRUD não estiverem funcionando corretamente,
será aplicada uma penalidade de 40 pontos.

## DISRUPTIVE ARCHITECTURES: IOT, IOB & GENERATIVE IA
Objetivo Principal
Apresentação do Protótipo Funcional e Análise da Arquitetura de IA.
Objetivos Específicos
• Demonstração do protótipo funcional até o momento: apresentar o estado atual do projeto,
demonstrando as funcionalidades implementadas até o momento. Apontar as dificuldades e as
conclusões sobre os problemas apurados até o momento deste sprint.
• Detalhamento da arquitetura de IA: descrever a arquitetura da IA utilizada, explicar porque essa
arquitetura foi escolhida e como ela foi / será implementada.
• Apresentar a base de dados utilizada para o treinamento/teste da atual versão do projeto.
Observações:
• Não explique os conceitos e como os frameworks funcionam, e sim como poderão ser utilizados
no projeto.
• É FUNDAMENTAL que nessa etapa sejam demonstradas funcionalidades do projeto e
evoluções em relação à entrega anterior.Erros de funcionamento podem ser admitidos e
devem explicados para as correções a serem efetuadas.
• O vídeo deverá ser gravado por um (ou mais) integrantes do grupo. Utilizem pequenos filmes
funcionais, imagens do produto e seus fluxos podem ser um bom roteiro de storytelling.

Requisitos
• [até 40 pontos] Evoluções do protótipo funcional, com foco na análise detalhada e clara da
arquitetura de ML / IA / Análise de Dados e sua implementação.
• [até 20 pontos] Integração do modelo ao projeto principal e às demais disciplinas.
• [até 20 pontos] Organização e estrutura da documentação do projeto no GitHub.
• [até 20 pontos] Criatividade da apresentação e da solução demonstrada.
Entrega
Arquivos entregáveis (todos obrigatórios):
• Link ou arquivo do novo vídeo com a apresentação, conforme objetivos acima.
• Link para a documentação organizada no GitHub.
Condições de entrega
• A integridade e o conteúdo do arquivo entregue são de responsabilidade dos integrantes do
grupo. Arquivos entregues sem conteúdo ou com arquivos corrompidos não serão
considerados.
• Não serão aceitos arquivos enviados pelo Teams ou fora do prazo.

## JAVA ADVANCED
Requisitos
• Aplicação MVC inicial / parcial para a solução proposta.
• Nesta 3ª Sprint, a equipe precisa desenvolver uma aplicação web MVC que
suporte a solução proposta. O grupo deve criar os seguintes artefatos:
o Código fonte da aplicação (70 pts)
▪ A aplicação deve utilizar o Thymeleaf em conjunto com o
framework Spring Boot;
▪ É importante que a solução de software já apresente templates
dinâmicos;
▪ Integração com o banco de dados;
▪ Realizar pelos menos 2 CRUDs.
o Vídeo com a apresentação da aplicação funcionando com
duração máxima de 10 minutos (30 pts)
▪ O vídeo deve abordar os seguintes tópicos:
• Objetivo, Arquitetura da solução, Funcionamento da
aplicação, Dificuldades encontradas até o
momento, Próximos passos.
Entrega
o A entrega deve ser feita via repositório público do GitHub com
arquivo ReadMe apresentando toda a solução proposta (nome dos
integrantes do grupo, diagramas, desenvolvimento e exemplos de
testes).

## MOBILE APP DEVELOPMENT
• Requisitos:
o Construa um App com 5 telas (40 pontos);
o Crie chamadas as APIs que façam sentido ao teu projeto de App (20 pontos);
o Crie um diagrama de arquitetura (estrutura de pastas) do seu aplicativo (20 pontos);
o Crie recursos atuais de navegabilidade e usabilidade (20 pontos).
• Entrega:
o Entregar o caminho do repositório do git contendo no readme.md as informações de nome do
grupo, explanação do projeto
