SET SERVEROUTPUT ON;
-- Nome dos Integrantes: 
-- Claudio Silva Bispo RM553472
-- Patricia Naomi Yamagishi RM552981

-- Deletar tabelas caso já existam

DROP TABLE Notificacao CASCADE CONSTRAINTS;
DROP TABLE Agenda CASCADE CONSTRAINTS;
DROP TABLE Formulario_Detalhado CASCADE CONSTRAINTS;
DROP TABLE Feedback CASCADE CONSTRAINTS;
DROP TABLE Sinistro CASCADE CONSTRAINTS;
DROP TABLE Consulta CASCADE CONSTRAINTS;
DROP TABLE Dentista CASCADE CONSTRAINTS;
DROP TABLE Especialidade CASCADE CONSTRAINTS;
DROP TABLE Clinica CASCADE CONSTRAINTS;
DROP TABLE Cliente CASCADE CONSTRAINTS;
DROP TABLE Estado_Civil CASCADE CONSTRAINTS;
DROP TABLE Tipo_Notificacao CASCADE CONSTRAINTS;

-- Criação das Tabelas

-- Tabela Cliente 
CREATE TABLE Cliente ( 
    id_cliente INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    nome VARCHAR2(100) NOT NULL, 
    sobrenome VARCHAR2(100) NOT NULL,
    email VARCHAR2(100) CONSTRAINT email_unique UNIQUE NOT NULL, 
    telefone VARCHAR2(15) NOT NULL, 
    data_nasc DATE NOT NULL, 
    endereco VARCHAR2(255) NOT NULL
); 

-- Tabela Clínica 
CREATE TABLE Clinica ( 
    id_clinica INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    nome VARCHAR2(100) NOT NULL, 
    endereco VARCHAR2(255) NOT NULL, 
    telefone VARCHAR2(15) NOT NULL, 
    avaliacao DECIMAL(2, 1) NOT NULL,
    preco_medio DECIMAL(10, 2) NOT NULL
); 

-- Tabela Especialidade
CREATE TABLE Especialidade (
    id_especialidade INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    nome VARCHAR2(100) NOT NULL
);

-- Tabela Dentista
CREATE TABLE Dentista (
    id_dentista INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    nome VARCHAR2(100) NOT NULL,
    sobrenome VARCHAR2(100) NOT NULL,
    telefone VARCHAR2(15) NOT NULL,
    id_clinica INTEGER NOT NULL,
    id_especialidade INTEGER NOT NULL,
    avaliacao DECIMAL(2, 1) NOT NULL,
    
    CONSTRAINT fk_dentista_clinica FOREIGN KEY (id_clinica) REFERENCES Clinica(id_clinica),
    CONSTRAINT fk_dentista_especialidade FOREIGN KEY (id_especialidade) REFERENCES Especialidade(id_especialidade)
);

-- Tabela Consulta 
CREATE TABLE Consulta ( 
    id_consulta INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    id_cliente INTEGER NOT NULL, 
    id_clinica INTEGER NOT NULL, 
    id_dentista INTEGER NOT NULL,
    tipo_servico VARCHAR2(100) NOT NULL,
    data_consulta TIMESTAMP NOT NULL, 
    status_consulta CHAR(1), -- 'S' ou 'N'
    observacoes VARCHAR2(250), 
    sintomas VARCHAR2(250), 
    tratamento_recomendado VARCHAR2(250), 
    custo DECIMAL(10, 2),
    prescricao VARCHAR2(250), 
    data_retorno DATE,
    
    CONSTRAINT fk_consulta_cliente FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
    CONSTRAINT fk_consulta_clinica FOREIGN KEY (id_clinica) REFERENCES Clinica(id_clinica),
    CONSTRAINT fk_consulta_dentista FOREIGN KEY (id_dentista) REFERENCES Dentista(id_dentista)
);
 
-- Tabela Sinistro
CREATE TABLE Sinistro (
    id_sinistro INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    id_consulta INTEGER NOT NULL, 
    nome VARCHAR2(100) NOT NULL,
    descricao VARCHAR2(250),
    status_sinistro CHAR(1), -- 'S' ou 'N'
    descricao_status VARCHAR2(250),
    valor_sinistro DECIMAL(10, 2),
    data_abertura DATE NOT NULL,
    data_resolucao DATE,
    documentacao VARCHAR2(250),
    
    CONSTRAINT fk_sinistro_consulta FOREIGN KEY (id_consulta) REFERENCES Consulta(id_consulta)
);

-- Tabela Feedback
CREATE TABLE Feedback (
    id_feedback INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    id_cliente INTEGER NOT NULL,
    id_dentista INTEGER NOT NULL,
    id_clinica INTEGER NOT NULL,
    avaliacao DECIMAL(2, 1) NOT NULL,
    comentario VARCHAR2(250),
    
    CONSTRAINT fk_feedback_cliente FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
    CONSTRAINT fk_feedback_clinica FOREIGN KEY (id_clinica) REFERENCES Clinica(id_clinica),
    CONSTRAINT fk_feedback_dentista FOREIGN KEY (id_dentista) REFERENCES Dentista(id_dentista)
);
  
-- Tabela Estado Civil
CREATE TABLE Estado_Civil (
    id_estado_civil INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    descricao VARCHAR2(50) NOT NULL
);

-- Tabela Formulário Detalhado
CREATE TABLE Formulario_Detalhado (
    id_formulario INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    id_cliente INTEGER NOT NULL,
    id_estado_civil INTEGER NOT NULL,
    historico_familiar VARCHAR2(250),
    profissao VARCHAR2(100),
    renda_mensal DECIMAL(10, 2),
    historico_medico VARCHAR2(250),
    alergia VARCHAR2(250),
    condicao_preexistente VARCHAR2(250),
    uso_medicamento VARCHAR2(250),
    familiar_com_doencas_dentarias VARCHAR2(255),
    participacao_em_programas_preventivos CHAR(1), -- 'Y' ou 'N'
    contato_emergencial VARCHAR2(250),
    pesquisa_satisfacao CHAR(1), -- 'Y' ou 'N'
    data_ultima_atualizacao DATE,
    frequencia_consulta_periodica CHAR(1), -- 'Y' ou 'N'
    sinalizacao_de_risco VARCHAR2(250),
    historico_de_viagem VARCHAR2(250),
    historico_de_mudancas_de_endereco VARCHAR2(250),
    preferencia_de_contato VARCHAR2(50),

    CONSTRAINT fk_formulario_cliente FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
    CONSTRAINT fk_formulario_estado_civil FOREIGN KEY (id_estado_civil) REFERENCES Estado_Civil(id_estado_civil)
);

-- Tabela Tipo Notificação
CREATE TABLE Tipo_Notificacao (
    id_tipo_notificacao INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    descricao VARCHAR2(40) NOT NULL
);

-- Tabela Agenda
CREATE TABLE Agenda (
    id_agenda INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    id_cliente INTEGER NOT NULL,
    id_consulta INTEGER NOT NULL,
    status_consulta CHAR(1),
    observacoes VARCHAR2(250),

    CONSTRAINT fk_agenda_cliente FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
    CONSTRAINT fk_agenda_consulta FOREIGN KEY (id_consulta) REFERENCES Consulta(id_consulta)
);

-- Tabela Notificações
CREATE TABLE Notificacao (
    id_notificacao INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    id_cliente INTEGER NOT NULL,
    id_tipo_notificacao INTEGER NOT NULL,
    mensagem VARCHAR2(250),
    data_envio DATE,

    CONSTRAINT fk_notificacao_cliente FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
    CONSTRAINT fk_notificacao_tipo FOREIGN KEY (id_tipo_notificacao) REFERENCES Tipo_Notificacao(id_tipo_notificacao)
);


/*

    Tarefas para esta entrega
    1. Correções - Não houve apontamentos
    2. Empacotamento de Objetos
    3. Criação de Procedures para Relatórios(Consultas)
    4. Criação de Trigger para Auditorias
    5. Testes e Demonstração em Vídeo
    
*/


-- 2. Empacotamento de Objetos

/*

    Vamos criar um package chamado clientes, clinicas,  que conterá as principais operações do sistema.
    
    Operações a incluir no Package:
        Uma procedure para inserir um cliente.
        Uma procedure para atualizar dados do cliente.
        Uma procedure para deletar um cliente.
        Uma função para buscar um cliente pelo ID.

*/

CREATE OR REPLACE PACKAGE BODY pkg_clientes AS 

    -- 1. Procedure para inserir um cliente
    PROCEDURE inserir_cliente(p_nome IN VARCHAR2, p_sobrenome IN VARCHAR2, p_email IN VARCHAR2, p_telefone IN VARCHAR2, p_data_nascimento IN DATE, p_endereco IN VARCHAR2) IS
    BEGIN
        INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco) 
        VALUES (p_nome, p_sobrenome, p_email, p_telefone, p_data_nascimento, p_endereco);
        
        COMMIT;
    END inserir_cliente;

    -- 2. Procedure para atualizar um cliente
    PROCEDURE atualizar_cliente(p_id IN NUMBER, p_nome IN VARCHAR2, p_sobrenome IN VARCHAR2, p_email IN VARCHAR2, p_telefone IN VARCHAR2, p_data_nascimento IN DATE, p_endereco IN VARCHAR2) IS
    BEGIN
        UPDATE Cliente
        SET nome = p_nome, sobrenome = p_sobrenome, email = p_email, telefone = p_telefone, data_nasc = p_data_nascimento, endereco = p_endereco
        WHERE id_cliente = p_id;
        
        COMMIT;
    END atualizar_cliente;

    -- 3. Procedure para excluir um cliente
    PROCEDURE excluir_cliente(p_id IN NUMBER) IS
    BEGIN
        DELETE FROM Cliente WHERE id_cliente = p_id;
        
        COMMIT;
    END excluir_cliente;

    -- 4. Procedure para listar clientes (retorna um cursor para ser consumido pela aplicação)
    PROCEDURE listar_clientes(p_cursor OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN p_cursor FOR 
            SELECT id_cliente, nome, sobrenome, email, telefone, data_nasc, endereco 
            FROM Cliente;
    END listar_clientes;

END pkg_clientes;

-- Executar a procedure inserir_cliente
BEGIN
    pkg_clientes.inserir_cliente(
        p_nome => 'João',
        p_sobrenome => 'Silva',
        p_email => 'joao.silva@email.com',
        p_telefone => '1234567890',
        p_data_nascimento => TO_DATE('1990-01-01', 'YYYY-MM-DD'),
        p_endereco => 'Rua A, 123'
    );
END;

SELECT * FROM CLIENTE

-- Executar a procedure atualizar_cliente

BEGIN
    pkg_clientes.atualizar_cliente(
        p_id => 1,
        p_nome => 'João',
        p_sobrenome => 'Silva',
        p_email => 'joao.silva@novoemail.com',
        p_telefone => '9876543210',
        p_data_nascimento => TO_DATE('1990-01-01', 'YYYY-MM-DD'),
        p_endereco => 'Rua B, 456'
    );
END;

SELECT * FROM CLIENTE

-- Executar a procedure excluir_cliente

BEGIN
    pkg_clientes.excluir_cliente(p_id => 1);
END;

SELECT * FROM CLIENTE

-- Executar a procedure listar_clientes
DECLARE
    v_cursor SYS_REFCURSOR; 
    v_id_cliente NUMBER;     
    v_nome VARCHAR2(100);
    v_sobrenome VARCHAR2(100);
    v_email VARCHAR2(100);
    v_telefone VARCHAR2(15);
    v_data_nasc DATE;
    v_endereco VARCHAR2(255);
BEGIN
    -- Chama a procedure para listar os clientes e passa o cursor
    pkg_clientes.listar_clientes(p_cursor => v_cursor);

    LOOP
        FETCH v_cursor INTO v_id_cliente, v_nome, v_sobrenome, v_email, v_telefone, v_data_nasc, v_endereco;
        EXIT WHEN v_cursor%NOTFOUND;  -- Sai do loop quando o cursor não encontrar mais dados

        -- Exibe os resultados no console
        DBMS_OUTPUT.PUT_LINE('ID: ' || v_id_cliente || 
                             ', Nome: ' || v_nome || 
                             ', Sobrenome: ' || v_sobrenome || 
                             ', Email: ' || v_email || 
                             ', Telefone: ' || v_telefone || 
                             ', Data de Nascimento: ' || TO_CHAR(v_data_nasc, 'DD/MM/YYYY') || 
                             ', Endereço: ' || v_endereco);
    END LOOP;

    CLOSE v_cursor;
END;

-- Package para a tabela Clinica
CREATE OR REPLACE PACKAGE pkg_clinica AS 
    -- CRUD
    PROCEDURE inserir_clinica(p_nome IN VARCHAR2, p_endereco IN VARCHAR2, p_telefone IN VARCHAR2, p_avaliacao IN DECIMAL, p_preco_medio IN DECIMAL);
    PROCEDURE atualizar_clinica(p_id IN NUMBER, p_nome IN VARCHAR2, p_endereco IN VARCHAR2, p_telefone IN VARCHAR2, p_avaliacao IN DECIMAL, p_preco_medio IN DECIMAL);
    PROCEDURE excluir_clinica(p_id IN NUMBER);
    PROCEDURE listar_clinicas(p_cursor OUT SYS_REFCURSOR);
END pkg_clinica;

-- Package Body para a tabela Clinica
CREATE OR REPLACE PACKAGE BODY pkg_clinica AS 

    -- 1. Procedure para inserir uma clínica
    PROCEDURE inserir_clinica(p_nome IN VARCHAR2, p_endereco IN VARCHAR2, p_telefone IN VARCHAR2, p_avaliacao IN DECIMAL, p_preco_medio IN DECIMAL) IS
    BEGIN
        INSERT INTO CLINICA (nome, endereco, telefone, avaliacao, preco_medio) 
        VALUES (p_nome, p_endereco, p_telefone, p_avaliacao, p_preco_medio);
        
        COMMIT;
    END inserir_clinica;

    -- 2. Procedure para atualizar uma clínica
    PROCEDURE atualizar_clinica(p_id IN NUMBER, p_nome IN VARCHAR2, p_endereco IN VARCHAR2, p_telefone IN VARCHAR2, p_avaliacao IN DECIMAL, p_preco_medio IN DECIMAL) IS
    BEGIN
        UPDATE CLINICA
        SET nome = p_nome, endereco = p_endereco, telefone = p_telefone, avaliacao = p_avaliacao, preco_medio = p_preco_medio
        WHERE id_clinica = p_id;
        
        COMMIT;
    END atualizar_clinica;

    -- 3. Procedure para excluir uma clínica
    PROCEDURE excluir_clinica(p_id IN NUMBER) IS
    BEGIN
        DELETE FROM CLINICA WHERE id_clinica = p_id;
        
        COMMIT;
    END excluir_clinica;

    -- 4. Procedure para listar clínicas (retorna um cursor para ser consumido pela aplicação)
    PROCEDURE listar_clinicas(p_cursor OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN p_cursor FOR 
            SELECT id_clinica, nome, endereco, telefone, avaliacao, preco_medio 
            FROM CLINICA;
    END listar_clinicas;

END pkg_clinica;

-- Teste de Inserção de Clínica (CRUD: Inserir)
BEGIN
    -- Teste de Inserção
    pkg_clinica.inserir_clinica(
        p_nome => 'Clínica São João',
        p_endereco => 'Rua das Flores, 123, Centro',
        p_telefone => '123456789',
        p_avaliacao => 4.5,
        p_preco_medio => 200.00
    );
END;

SELECT * FROM CLINICA

-- Teste de Atualização de Clínica (CRUD: Atualizar)
BEGIN
    -- Teste de Atualização
    pkg_clinica.atualizar_clinica(
        p_id => 1,  -- ID da clínica a ser atualizada
        p_nome => 'Clínica São João Atualizada',
        p_endereco => 'Avenida Brasil, 456, Centro',
        p_telefone => '987654321',
        p_avaliacao => 4.7,
        p_preco_medio => 220.00
    );
END;

SELECT * FROM CLINICA

-- Teste de Exclusão de Clínica (CRUD: Excluir)
BEGIN
    -- Teste de Exclusão
    pkg_clinica.excluir_clinica(p_id => 1);  -- ID da clínica a ser excluída
END;

SELECT * FROM CLINICA

-- Teste de Listagem de Clínicas (CRUD: Listar)
DECLARE
    v_cursor SYS_REFCURSOR;
    v_id_clinica NUMBER;
    v_nome VARCHAR2(100);
    v_endereco VARCHAR2(255);
    v_telefone VARCHAR2(15);
    v_avaliacao DECIMAL(2,1);
    v_preco_medio DECIMAL(10,2);
BEGIN
    -- Teste de Listagem
    pkg_clinica.listar_clinicas(p_cursor => v_cursor);
    
    -- Processa o cursor e exibe os resultados
    LOOP
        FETCH v_cursor INTO v_id_clinica, v_nome, v_endereco, v_telefone, v_avaliacao, v_preco_medio;
        EXIT WHEN v_cursor%NOTFOUND;
        
        -- Exibe os dados das clínicas
        DBMS_OUTPUT.PUT_LINE('ID: ' || v_id_clinica || ', Nome: ' || v_nome || ', Endereço: ' || v_endereco || ', Telefone: ' || v_telefone || ', Avaliação: ' || v_avaliacao || ', Preço Médio: ' || v_preco_medio);
    END LOOP;
    
    CLOSE v_cursor;
END;

-- Criação do Pacote pkg_especialidade
CREATE OR REPLACE PACKAGE pkg_especialidade AS 
    -- CRUD
    PROCEDURE inserir_especialidade(p_nome IN VARCHAR2);
    PROCEDURE atualizar_especialidade(p_id IN NUMBER, p_nome IN VARCHAR2);
    PROCEDURE excluir_especialidade(p_id IN NUMBER);
    PROCEDURE listar_especialidades(p_cursor OUT SYS_REFCURSOR);
END pkg_especialidade;

-- Package Body para a tabela Especialidade
CREATE OR REPLACE PACKAGE BODY pkg_especialidade AS 

    -- 1. Procedure para inserir uma especialidade
    PROCEDURE inserir_especialidade(p_nome IN VARCHAR2) IS
    BEGIN
        INSERT INTO Especialidade (nome)
        VALUES (p_nome);
        
        COMMIT;
    END inserir_especialidade;

    -- 2. Procedure para atualizar uma especialidade
    PROCEDURE atualizar_especialidade(p_id IN NUMBER, p_nome IN VARCHAR2) IS
    BEGIN
        UPDATE Especialidade
        SET nome = p_nome
        WHERE id_especialidade = p_id;
        
        COMMIT;
    END atualizar_especialidade;

    -- 3. Procedure para excluir uma especialidade
    PROCEDURE excluir_especialidade(p_id IN NUMBER) IS
    BEGIN
        DELETE FROM Especialidade WHERE id_especialidade = p_id;
        
        COMMIT;
    END excluir_especialidade;

    -- 4. Procedure para listar especialidades
    PROCEDURE listar_especialidades(p_cursor OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN p_cursor FOR 
            SELECT id_especialidade, nome
            FROM Especialidade;
    END listar_especialidades;

END pkg_especialidade;

-- Teste de Inserção (CRUD: Inserir)
BEGIN
    -- Teste de Inserção
    pkg_especialidade.inserir_especialidade(p_nome => 'Ortodontia');
END;

SELECT * FROM especialidade

-- Teste de Atualização (CRUD: Atualizar)
BEGIN
    -- Teste de Atualização
    pkg_especialidade.atualizar_especialidade(
        p_id => 1,  -- ID da especialidade a ser atualizada
        p_nome => 'Endodontia'
    );
END;

SELECT * FROM especialidade

-- Teste de Exclusão (CRUD: Excluir)
BEGIN
    -- Teste de Exclusão
    pkg_especialidade.excluir_especialidade(p_id => 1);  -- ID da especialidade a ser excluída
END;

SELECT * FROM especialidade

-- Teste de Listagem (CRUD: Listar)
DECLARE
    v_cursor SYS_REFCURSOR;
    v_id_especialidade NUMBER;
    v_nome VARCHAR2(100);
BEGIN
    -- Teste de Listagem
    pkg_especialidade.listar_especialidades(p_cursor => v_cursor);
    
    -- Processa o cursor e exibe os resultados
    LOOP
        FETCH v_cursor INTO v_id_especialidade, v_nome;
        EXIT WHEN v_cursor%NOTFOUND;
        
        -- Exibe os dados das especialidades
        DBMS_OUTPUT.PUT_LINE('ID: ' || v_id_especialidade || ', Nome: ' || v_nome);
    END LOOP;
    
    CLOSE v_cursor;
END;

